using UnityEngine;
using System.Collections;

public class WarpGateEntry : MonoBehaviour {

	[HideInInspector]
	public WarpGateEntry otherGate;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	void OnTriggerEnter(Collider other) {
//		Debug.Log("WARP");

		WarpObject  WO = other.gameObject.GetComponent<WarpObject>();

		if (WO.justWarped) return;

		other.gameObject.transform.position = otherGate.transform.position;
	}

	void OnTriggerExit(Collider other) {
//		Debug.Log("WARP");

		WarpObject  WO = other.gameObject.GetComponent<WarpObject>();

		if (WO.justWarped) WO.justWarped = false;
		else WO.justWarped = true;
//			other.gameObject.transform.position = otherGate.transform.position;
	}

}
