using UnityEngine;
using System.Collections;
using InputPlusControl;

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
	float boostInput;
	bool renderOn = false;
	// Update is called once per frame
	void Update () {
		if (player.dummy) return;

		isBoosting = false;

		if (player.useKeyboard) boostInput = Input.GetKey(KeyCode.LeftShift) ? 1 : 0;
		else boostInput = InputPlus.GetData (player.controllerID, ControllerVarEnum.ShoulderBottom_left);
		boostInput = Mathf.Clamp (boostInput, 0, 1);

		if (boostInput > 0 && fuel > 0 && !blockBoost) isBoosting = true;

//		if (player.shipType == ShipType.Plane) {
//
//			boostInput = InputPlus.GetData (player.controllerID, ControllerVarEnum.ShoulderBottom_right);
//			boostInput = Mathf.Clamp (boostInput, 0, 1);
//
//			if (boostInput > 0 && fuel > 0 && !blockBoost) isBoosting = true;
//
//		} else if (player.shipType == ShipType.Tank) {
//			boostInput = InputPlus.GetData (player.controllerID, ControllerVarEnum.ShoulderBottom_left);
//			boostInput = Mathf.Clamp (boostInput, 0, 1);
//
//			if (boostInput > 0 && fuel > 0 && !blockBoost) isBoosting = true;
//
//		} else if (player.shipType == ShipType.Strafe) {
//
//
//		}

			
		//if (Input.GetButton("Boost_"+player.playerID) && fuel > 0 && !blockBoost) {
		if (isBoosting) {
			//deplete fuel
			fuel -= depletionRate;
			fuel = Mathf.Clamp(fuel, 0, maxFuel);
			//increase player speed
			player.boostModifier = speedIncrease;
//			trail.enabled = true;
			if (!renderOn) {
				StartCoroutine("TrailOn");
				renderOn = true;
			}

		} else {
			//regen fuel
			fuel += regenRate;
			fuel = Mathf.Clamp(fuel, 0, maxFuel);
			//set player speed back to normal
			player.boostModifier = 0;
//			trail.enabled = false;
			if (renderOn) {
				StartCoroutine("TrailOff");
				renderOn = false;
			}


			if (fuel > 3f) blockBoost = false;
			else blockBoost = true;
		}
			
	}

	float trailTime = 0.5f;
	IEnumerator TrailOn() {
		StopCoroutine("TrailOff");
		while (trail.time < trailTime) {
			trail.time += 0.02f;
			yield return new WaitForEndOfFrame();
		}

	}

	IEnumerator TrailOff() {
		StopCoroutine("TrailOn");
		while (trail.time > 0) {
			trail.time -= 0.02f;
			yield return new WaitForEndOfFrame();
		}

	}


}
