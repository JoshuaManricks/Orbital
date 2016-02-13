using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MovementModifier : MonoBehaviour {

	public List<FirstPersonController> ships;

	public float movementEffect = -0.1f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
//		foreach( FirstPersonController ship in ships) {
//			ship.boostModifier +=
//		}
	}

	void OnTriggerEnter(Collider other) {
		Debug.Log("SLOW DOWN");

		other.gameObject.GetComponent<FirstPersonController>().movementModifier = movementEffect;
	}


	void OnTriggerExit(Collider other) {
		Debug.Log("SPEED UP");

//		ships.Remove(other.gameObject.GetComponent<FirstPersonController>());
		other.gameObject.GetComponent<FirstPersonController>().movementModifier = 0;
	}
}
