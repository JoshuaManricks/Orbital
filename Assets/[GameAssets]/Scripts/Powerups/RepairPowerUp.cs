using UnityEngine;
using System.Collections;

public class RepairPowerUp : PowerUp {

	public int healAmount = 5;

	// Use this for initialization
	void Start () {

	}

	protected override void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Player") {
			other.gameObject.GetComponentInChildren<LifeController>().Heal(healAmount);
			Destroy(gameObject);
		}
	}

}
