using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DamageArea : MonoBehaviour {

	public List<LifeController> damageTargets;

	public float damage = 0.1f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
		foreach( LifeController l in damageTargets) {
			l.TakeDamage(damage);
		}
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.CompareTag("Player")) damageTargets.Add(other.gameObject.GetComponent<LifeController>());
	}


	void OnTriggerExit(Collider other) {
		if (other.gameObject.CompareTag("Player")) damageTargets.Remove(other.gameObject.GetComponent<LifeController>());
	}
}
