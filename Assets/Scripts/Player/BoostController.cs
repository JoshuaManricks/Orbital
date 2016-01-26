using UnityEngine;
using System.Collections;

public class BoostController : MonoBehaviour {

	public float regenRate = .1f;
	public float depletionRate = .2f;

	public float maxFuel = 10f;
	public float fuel = 10f;

	public float speedIncrease = 10f;

	public bool blockBoost = false;

	FirstPersonController player;
	TrailRenderer trail;
	// Use this for initialization
	void Start () {
		player = GetComponent<FirstPersonController>();
		trail = GetComponent<TrailRenderer>();
	}

	bool isBoosting = false;
	// Update is called once per frame
	void Update () {
		isBoosting = false;

		if (player.playerID == PlayerID.P2) {
			Debug.Log ("b " + Input.GetAxisRaw ("Boost_" + player.playerID));
			if (Input.GetAxisRaw ("Boost_" + player.playerID)>0 && fuel > 0 && !blockBoost) isBoosting = true;

		} else {
			if (Input.GetButton ("Boost_" + player.playerID) && fuel > 0 && !blockBoost)  isBoosting = true;

		}


		
			
		//if (Input.GetButton("Boost_"+player.playerID) && fuel > 0 && !blockBoost) {
		if (isBoosting) {
			//deplete fuel
			fuel -= depletionRate;
			fuel = Mathf.Clamp(fuel, 0, maxFuel);
			//increase player speed
			player.boost = speedIncrease;
			trail.enabled = true;

		} else {
			//regen fuel
			fuel += regenRate;
			fuel = Mathf.Clamp(fuel, 0, maxFuel);
			//set player speed back to normal
			player.boost = 0;
			trail.enabled = false;

			if (fuel > 3f) blockBoost = false;
			else blockBoost = true;
		}


	}


}
