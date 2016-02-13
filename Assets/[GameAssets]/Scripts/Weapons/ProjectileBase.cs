using UnityEngine;
using System.Collections;

[RequireComponent (typeof (GravityBody))]
public class ProjectileBase : MonoBehaviour {

	// public vars
	public float damage;
	public float speed = 6;
	[HideInInspector]
	public float shipSpeed = 0;
	public float lifeTime = 1;

	float collisionOffsetTime = .1f;

	protected float timer = 0;

	// System vars
	protected Vector3 moveAmount;
	protected Vector3 smoothMoveVelocity;
	protected Rigidbody rigidbody;

	[HideInInspector]
	public FirstPersonController owner;

	public GameObject impactEffect;

	void Awake() {
		rigidbody = GetComponent<Rigidbody> ();
	}

	protected virtual void Update() {

		timer+=Time.deltaTime;

		if (timer >= lifeTime) Destroy(gameObject);

		Vector3 moveDir = new Vector3(0,0,1);//.normalized;
		Vector3 targetMoveAmount = moveDir * (speed+shipSpeed);
		moveAmount = targetMoveAmount;

//		RayDown ();

	}

	protected virtual void FixedUpdate() {
		// Apply movement to rigidbody
		Vector3 localMove = transform.TransformDirection(moveAmount) * Time.fixedDeltaTime;

		rigidbody.MovePosition(rigidbody.position + localMove);
	}

	protected virtual void OnCollisionEnter(Collision collision) {
		if (timer < collisionOffsetTime && collision.gameObject == owner.gameObject) return;

		if (collision.gameObject.tag == "Player") {
//			Debug.Log(collision.gameObject.name);
			collision.gameObject.GetComponent<LifeController>().TakeDamage(damage);
			SpawnImpact(collision.gameObject);
			Destroy(gameObject);
		} else if (collision.gameObject.tag == "Wall") {
			SpawnImpact();
			Destroy(gameObject);
				
		} else if (collision.gameObject.tag == "Projectile") {
			Debug.Log("OnCollisionEnter WEAPON");
			SpawnImpact();
			Destroy(gameObject);

		}
	}

	protected virtual void OnTriggerEnter(Collider other) {
		if (timer < collisionOffsetTime && other.gameObject == owner.gameObject) return;

		if (other.gameObject.tag == "Player") {
//			Debug.Log(other.gameObject.name);
			other.gameObject.GetComponent<LifeController>().TakeDamage(damage);
			SpawnImpact(other.gameObject);
			Destroy(gameObject);

		} else if (other.gameObject.tag == "Wall") {
			SpawnImpact();
			Destroy(gameObject);
//			Debug.Log("Destroy by WALL");

		} else if (other.gameObject.tag == "Projectile") {
			Debug.Log("OnTriggerEnter WEAPON");
			SpawnImpact();
			Destroy(gameObject);
		} else {
//			Debug.Log("OnTriggerEnter "+other.gameObject.name);
		}

	}

	void SpawnImpact() {
		if (impactEffect != null) Instantiate(impactEffect, transform.position, transform.rotation);
	}

	void SpawnImpact(GameObject go) {
		if (impactEffect != null){
			GameObject effect = Instantiate(impactEffect, transform.position, transform.rotation) as GameObject;
			effect.transform.parent = go.transform;
		}
	}

	void RayDown() {
		// Grounded check
		Ray ray = new Ray(transform.position, -transform.up);
		RaycastHit hit;

		if (Physics.Raycast(ray, out hit, 5f)) {
//			Debug.Log (hit.collider.gameObject.name + " : "+hit.distance);
			if (hit.distance > 1.7) down = .2f;
			else down = 0;
		}

	}

	float down = 0f;
}
