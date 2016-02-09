using UnityEngine;
using System.Collections;

public class PowerUpSpawnPoint : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
//		isAvailable = true;
	}

	public bool isAvailable = true;

//	bool isAvailable = true;

	void OnTriggerEnter(Collider other) {
		Debug.Log ("OnTriggerEnter "+other.gameObject.name);
		isAvailable = false;
	}

	void OnTriggerExit(Collider other) {
		Debug.Log ("OnTriggerExit "+other.gameObject.name);
		isAvailable = true;
		//		Destroy(other.gameObject);
	}
		
	void OnTriggerStay(Collider other) {
		isAvailable = false;
	}

}