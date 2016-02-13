using UnityEngine;
using System.Collections;

public class PowerUpSpawnPoint : MonoBehaviour {
	
	// Update is called once per frame
	public bool isAvailable = true;

	void OnTriggerEnter(Collider other) {
//		Debug.Log ("PowerUpSpawnPoint OnTriggerEnter "+other.gameObject.name);
		isAvailable = false;
	}

	void OnTriggerExit(Collider other) {
//		Debug.Log ("PowerUpSpawnPoint OnTriggerExit "+other.gameObject.name);
		isAvailable = true;
	}

	/*
	void OnColliderEnter(Collision other) {
		Debug.Log ("PowerUpSpawnPoint OnColliderEnter "+other.gameObject.name);
		isAvailable = false;
	}

	void OnColliderExit(Collision other) {
		Debug.Log ("PowerUpSpawnPoint OnColliderExit "+other.gameObject.name);
		isAvailable = true;
	}


	void OnColliderStay(Collision other) {
		Debug.Log ("OnTriggerExit "+other.gameObject.name);

		isAvailable = false;
	}

	void OnTriggerStay(Collider other) {
		isAvailable = false;
	}*/

}