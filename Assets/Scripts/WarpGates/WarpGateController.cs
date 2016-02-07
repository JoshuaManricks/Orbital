using UnityEngine;
using System.Collections;

public class WarpGateController : MonoBehaviour {

	WarpGateEntry entry1;
	WarpGateEntry entry2;

	// Use this for initialization
	void Start () {
		entry1 = transform.GetChild(0).gameObject.GetComponent<WarpGateEntry>();
		entry2 = transform.GetChild(1).gameObject.GetComponent<WarpGateEntry>();

		entry1.otherGate = entry2;
		entry2.otherGate = entry1;
	}
}
