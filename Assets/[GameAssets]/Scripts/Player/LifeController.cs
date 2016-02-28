using UnityEngine;
using System.Collections;

public class LifeController : MonoBehaviour {

	public float maxLife;
	public float currentLife;
	FirstPersonController player;

	public bool invulnerable = false;
	// Use this for initialization
	void Start () {
		player = GetComponent<FirstPersonController>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void TakeDamage(float damage) {
//		Debug.Log(gameObject.name + " TakeDamage "+damage);


		if (invulnerable) return;
			
		currentLife = Mathf.Clamp(currentLife - damage, 0, maxLife);

		if (currentLife == 0) {
			player.DestroyShip();

			Explode();

			Destroy(this);
		}
	}


	void Explode() {
		Collider[] cols = GetComponentsInChildren<Collider>();

		foreach (Collider c in cols) {
			c.enabled = true; 
//			Debug.Log ("Add rigidbody "+c.name);
			c.gameObject.AddComponent<Rigidbody>();
		}

		GetComponent<BoxCollider>().enabled = false;
	}

	public void Heal(float heal) {

		currentLife = Mathf.Clamp(currentLife + heal, 0, maxLife);

	}
}
