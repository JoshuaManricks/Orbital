using UnityEngine;
using System.Collections;

public class ProximityMine : ProjectileBase {

	public float proximity = 5f;
	public float explositonDelay = 0.5f;//delay the explosion once a ship is in range
	public float activationDelay = 2f;//delay the proximity sensor
	public float explosionRadius = 6f;

	public GameObject[] players;
	public GameObject myPlayer;

	// Use this for initialization
	void Start () {
		players = GameObject.FindGameObjectsWithTag("Player");

	}
	
	// Update is called once per frame

	protected override void Update() {
		timer+=Time.deltaTime;


		//if (!triggered  && timer > activationDelay) CheckForProximty();
		if (timer > activationDelay) CheckForProximty();
	}

	protected override void FixedUpdate() {
		// DO NOTHING PLEASE
	}

	void CheckForProximty() {
		int inRangeCount = 0;

		foreach (GameObject go in players) {

			if (Vector3.Distance(go.transform.position, transform.position) <= proximity) {
				inRangeCount++;
				Debug.DrawLine(go.transform.position, transform.position, Color.red);
			}
		
		}

		if (inRangeCount > 0 && !triggered) DelayExplosion();
	}

	bool triggered = false;
	void DelayExplosion() {
		Debug.Log("DelayExplosion ");

		triggered = true;
		StartCoroutine("Explode");
	}

	IEnumerator Explode() {

		yield return new WaitForSeconds(explositonDelay);
		Debug.Log("Explode ");
		GetComponent<SphereCollider>().radius = explosionRadius;
		GetComponent<SphereCollider>().isTrigger = true;

		yield return new WaitForEndOfFrame();

		Destroy(gameObject);
	}

	protected override void OnCollisionEnter(Collision collision) {
		return;

		if (collision.gameObject.tag == "Player") {
			if (!triggered) GetComponent<SphereCollider>().radius = explosionRadius;

			collision.gameObject.GetComponent<LifeController>().TakeDamage(damage);

			Destroy(gameObject);
		}
	}

	void OnTriggerEnter(Collider collision) {

		if (collision.gameObject.tag == "Player") {
			//if (!triggered) GetComponent<SphereCollider>().radius = explosionRadius;

			collision.gameObject.GetComponent<LifeController>().TakeDamage(damage);

		}

	}
}
