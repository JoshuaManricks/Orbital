using UnityEngine;
using System.Collections;

public class LifeController : MonoBehaviour {

	public float maxLife;
	public float life;
	FirstPersonController player;


	// Use this for initialization
	void Start () {
		player = GetComponent<FirstPersonController>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void TakeDamage(float damage) {
		Debug.Log(gameObject.name + " TakeDamage "+damage);

		life = Mathf.Clamp(life - damage, 0, maxLife);

		if (life == 0) {
			player.DestroyShip();

			Explode();

			Destroy(this);
		}
	}


	void Explode() {
		Collider[] cols = GetComponentsInChildren<Collider>();

		foreach (Collider c in cols) {
			c.enabled = true; 
			c.gameObject.AddComponent<Rigidbody>();
		}

		GetComponent<BoxCollider>().enabled = false;
	}

	public void Heal(float heal) {

		life = Mathf.Clamp(life + heal, 0, maxLife);

	}
}
