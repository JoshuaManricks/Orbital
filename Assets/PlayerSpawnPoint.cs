using UnityEngine;
using System.Collections;

public class PlayerSpawnPoint : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public bool isAvailable = true;

	void OnTriggerEnter(Collider other) {
		Debug.Log ("PlayerSpawnPoint OnTriggerEnter "+other.gameObject.name);
		isAvailable = false;
	}

	void OnTriggerExit(Collider other) {
		Debug.Log ("PlayerSpawnPoint OnTriggerExit "+other.gameObject.name);
		isAvailable = true;
	}
}
