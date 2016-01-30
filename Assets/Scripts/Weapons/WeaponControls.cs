﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WeaponControls : MonoBehaviour {

	public WeaponName startPrimaryWeapon;
	public WeaponName startSecondaryWeapon;

	public float primaryInterval = 0.1f;
	public float secondaryInterval = 2f;

	public WeaponComponent[] weapons;

	public WeaponComponent primaryWeapon;
	public WeaponComponent secondaryWeapon;

	public FirstPersonController player;
	PowerUpSpawner powerUpSpawner;

	public SpecialWeaponsBar weaponsBar;

	// Use this for initialization
	void Start () {
		PowerUpSpawner.PowerUpCollected += powerUpCollected;

		weapons = GetComponentsInChildren<WeaponComponent>();

		DisableAllWeapons();

		player  = transform.parent.gameObject.GetComponent<FirstPersonController>();
		weaponsBar  = player.GetComponentInChildren<SpecialWeaponsBar>();

		ChangeWeapon(startPrimaryWeapon);
		ChangeWeapon(startSecondaryWeapon, 5);

	}

	void powerUpCollected(PowerUpEventData data) {
		if (data.playerId == player.playerID) {
			ChangeWeapon (data.type);
		} else {

		}
	}

	// Update is called once per frame
	void Update () {
		if (player.dummy) return;

		// Fire bullet code
		if (player.shipType == ShipType.Plane) UpdatePlaneControls();
		else if (player.shipType == ShipType.Tank) UpdateTankControls();
		else if (player.shipType == ShipType.Strafe) UpdateStrafeControls();
	}

	public float turretSpeed = 10f;
	public bool tankFiring = false;
	void UpdateTankControls() {
		float inputY = Input.GetAxisRaw ("Vertical_" + player.playerID) * -1f;
		inputY = Mathf.Clamp (inputY, 0, 1);


		if (inputY > 0 && !tankFiring) {
			tankFiring = true;
			InvokeRepeating ("FirePrimary", float.Epsilon, primaryInterval);
		}
		if (inputY == 0) {
			tankFiring = false;
			CancelInvoke ("FirePrimary");
		}

		if (Input.GetKeyDown (KeyCode.Joystick1Button12))
			InvokeRepeating ("FireSecondary", float.Epsilon, secondaryInterval);
		if (Input.GetKeyUp (KeyCode.Joystick1Button12))
			CancelInvoke ("FireSecondary");

		//rotate turret
		float x = Input.GetAxisRaw("TurretX_" + player.playerID);
		float y = Input.GetAxisRaw("TurretY_" + player.playerID);
		if (x != 0.0f || y != 0.0f) {
			float angle = Mathf.Atan2(y, x) * Mathf.Rad2Deg;
			angle += 90f;
			transform.localRotation = Quaternion.Slerp (transform.localRotation, Quaternion.AngleAxis (angle, Vector3.up), .1f);
		}
	}

	void UpdateStrafeControls() {
			float inputY = Input.GetAxisRaw ("Vertical_" + player.playerID) * -1f;
			inputY = Mathf.Clamp (inputY, 0, 1);

			if (inputY > 0 && !tankFiring) {
				tankFiring = true;
				InvokeRepeating ("FirePrimary", float.Epsilon, primaryInterval);
			}
			if (inputY == 0) {
				tankFiring = false;
				CancelInvoke ("FirePrimary");
			}

			if (Input.GetKeyDown (KeyCode.Joystick1Button12))
				InvokeRepeating ("FireSecondary", float.Epsilon, secondaryInterval);
			if (Input.GetKeyUp (KeyCode.Joystick1Button12))
				CancelInvoke ("FireSecondary");
			
	}

	void UpdatePlaneControls() {
		if (player.playerID == PlayerID.P2) {
			if (Input.GetKeyDown (KeyCode.Joystick1Button16))
				InvokeRepeating ("FirePrimary", float.Epsilon, primaryInterval);
			if (Input.GetKeyUp (KeyCode.Joystick1Button16))
				CancelInvoke ("FirePrimary");

			if (Input.GetKeyDown (KeyCode.Joystick1Button18))
				InvokeRepeating ("FireSecondary", float.Epsilon, secondaryInterval);
			if (Input.GetKeyUp (KeyCode.Joystick1Button18))
				CancelInvoke ("FireSecondary");

		} else if (player.playerID == PlayerID.P1) {
			if (Input.GetKeyDown (KeyCode.Q))
				InvokeRepeating ("FirePrimary", float.Epsilon, primaryInterval);
			if (Input.GetKeyUp (KeyCode.Q))
				CancelInvoke ("FirePrimary");

			if (Input.GetKeyDown (KeyCode.E))
				InvokeRepeating ("FireSecondary", float.Epsilon, secondaryInterval);
			if (Input.GetKeyUp (KeyCode.E))
				CancelInvoke ("FireSecondary");
		}
	}

	void FirePrimary()
	{
		primaryWeapon.Fire((player.walkSpeed+player.boost)*player.inputY, player);
	}

	void FireSecondary() {
		if (secondaryWeapon != null
			&& weaponsBar.currentShots > 0
			&& secondaryWeapon.canFire) {

			secondaryWeapon.Fire (player);
			weaponsBar.UseShot ();

			//check for remaining ammo
			if (weaponsBar.currentShots == 0) {
				secondaryWeapon.gameObject.SetActive(false);
			}
		}
	}

	public void DisableAllWeapons() {
		foreach (WeaponComponent weapon in weapons) {
			weapon.gameObject.SetActive(false);
		}
	}

	public void ChangeWeapon(WeaponName newWeapon, int amountOfAmmo) {
		//find weaopn
		foreach (WeaponComponent weapon in weapons) {
			if (weapon.name == newWeapon) {

				if (weapon.grade == WeaponGrade.Primary) {
					if (primaryWeapon) primaryWeapon.gameObject.SetActive(false);
					primaryWeapon = weapon;
					primaryWeapon.gameObject.SetActive(true);
				}

				if (weapon.grade == WeaponGrade.Secondary) {
					if (secondaryWeapon) secondaryWeapon.gameObject.SetActive(false);
					secondaryWeapon = weapon;
					secondaryWeapon.gameObject.SetActive(true);

					weaponsBar.SetShots (amountOfAmmo);
				}

				return;
			}
		}
	}

	public void ChangeWeapon(WeaponName newWeapon) {
		//find weaopn
		foreach (WeaponComponent weapon in weapons) {
			if (weapon.name == newWeapon) {

				if (weapon.grade == WeaponGrade.Primary) {
					if (primaryWeapon) primaryWeapon.gameObject.SetActive(false);
					primaryWeapon = weapon;
					primaryWeapon.gameObject.SetActive(true);
				}

				if (weapon.grade == WeaponGrade.Secondary) {
					if (secondaryWeapon) secondaryWeapon.gameObject.SetActive(false);
					secondaryWeapon = weapon;
					secondaryWeapon.gameObject.SetActive(true);
				}
				return;
			}
		}
	}
}

public enum WeaponName  {
	SingleShot,
	LongShot,
	Missile,
	SeekingMissile,
	ProxyMine,
	SeekingMissileCluster,
	None
}

public enum WeaponGrade  {
	Primary,
	Secondary
}

public enum ShipType  {
	Plane,
	Tank,
	Strafe
}