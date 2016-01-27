using UnityEngine;
using System.Collections;

public class ProximityMine : ProjectileBase {

	public float proximity = 5f;
	public float explositonDelay = 0.5f;//delay the explosion once a ship is in range
	public float activationDelay = 2f;//delay the proximity sensor
	public float explosionRadius = 6f;

	GameController gameController;

	// Use this for initialization
	void Start () {
		gameController = FindObjectOfType<GameController>();
	}
	
	// Update is called once per frame

	protected override void Update() {
		timer+=Time.deltaTime;

		if (timer > activationDelay) CheckForProximty();
	}

	protected override void FixedUpdate() {
		// DO NOTHING PLEASE
	}

	void CheckForProximty() {
		int inRangeCount = 0;

		foreach (GameObject go in gameController.players) {

			if (Vector3.Distance(go.transform.position, transform.position) <= proximity) {
				inRangeCount++;
				Debug.DrawLine(go.transform.position, transform.position, Color.red);
			}
		}

		if (inRangeCount > 0 && !triggered) TriggerExplosion(explositonDelay);
	}

	bool triggered = false;
	void TriggerExplosion(float delay) {
		Debug.Log("DelayExplosion ");

		if (triggered) return;

		triggered = true;
		StartCoroutine("Explode", delay);
	}

	IEnumerator Explode(float delay) {
//		triggered = true;
		yield return new WaitForSeconds(delay);

		Debug.Log("Explode "+delay);
		GetComponent<SphereCollider>().isTrigger = true;
		GetComponent<SphereCollider>().radius = explosionRadius;

		yield return new WaitForEndOfFrame();

		Destroy(gameObject);
	}

	protected override void OnCollisionEnter(Collision collision) {

		Debug.Log("Proxy OnCollisionEnter "+collision.gameObject.name);

		if (collision.gameObject.tag == "Player" ||
			collision.gameObject.tag == "Projectile") TriggerExplosion(0);
			
	}

	//when the mine is shot at
	protected override void OnTriggerEnter(Collider other) {
		Debug.Log("Proxy OnTriggerEnter "+other.gameObject.name);
		TriggerExplosion(0);

		if (other.gameObject.tag == "Player") {
			other.gameObject.GetComponent<LifeController>().TakeDamage(damage);
		}
	}
}
