using UnityEngine;
using System.Collections;

public class InfiniteBoostModifier : MonoBehaviour {

	public float timeOfEffect = 10f;

	public BoostController boostController;

	// Use this for initialization
	void Start () {
		boostController = gameObject.GetComponent<BoostController> ();

		Destroy (this, timeOfEffect);
	}
	
	// Update is called once per frame
	void Update () {
		boostController.fuel = boostController.maxFuel;
	}
}
