using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DamageArea : MonoBehaviour {

	public List<LifeController> damageTargets;

	public float damage = 0.1f;

	// Use this for initialization
	void Start () {
		
	}

	void OnTriggerStay(Collider other) {
		
		if (other.gameObject.CompareTag ("SpawnPoint")) return;
//		Debug.Log("DamageArea "+other.gameObject.name);
		other.gameObject.GetComponent<LifeController>().TakeDamage(damage);
	}
}
