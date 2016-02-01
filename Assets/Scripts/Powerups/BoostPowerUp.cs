using UnityEngine;
using System.Collections;

public class BoostPowerUp : PowerUp {

	public int healAmount = 5;

	// Use this for initialization
	void Start () {

	}

	protected override void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Player") {
			Debug.Log ("adds cript");
			other.gameObject.AddComponent<InfiniteBoostModifier> ();
			Destroy(gameObject);
		}
	}

}
