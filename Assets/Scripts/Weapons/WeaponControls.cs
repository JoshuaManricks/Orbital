using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using InputPlusControl;

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

//		Debug.Log ("BUTTON " +InputPlus.GetData (1, ControllerVarEnum.ThumbRight));

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
	[HideInInspector]
	public bool isPrimaryFiring = false;

	public bool isSecondaryFiring = false;
	void UpdateTankControls() {
		float inputY = InputPlus.GetData (player.controllerID, ControllerVarEnum.ShoulderBottom_right);
		inputY = Mathf.Clamp (inputY, 0, 1);

		if (inputY > 0 && !isPrimaryFiring) {
			isPrimaryFiring = true;
			InvokeRepeating ("FirePrimary", float.Epsilon, primaryInterval);

		} else if (inputY == 0 && isPrimaryFiring) {
			isPrimaryFiring = false;
			CancelInvoke ("FirePrimary");
		}

		if (InputPlus.GetData (player.controllerID, ControllerVarEnum.ThumbRight) == 1f && !isSecondaryFiring) {
			InvokeRepeating ("FireSecondary", float.Epsilon, secondaryInterval);
			isSecondaryFiring = true;
		} else if (InputPlus.GetData (player.controllerID, ControllerVarEnum.ThumbRight) == 0f && isSecondaryFiring) {
			CancelInvoke ("FireSecondary");
			isSecondaryFiring = false;
		}

		//rotate turret
		float x = InputPlus.GetData (player.controllerID, ControllerVarEnum.ThumbRight_x);
		float y = InputPlus.GetData (player.controllerID, ControllerVarEnum.ThumbRight_y);
		if (x != 0.0f || y != 0.0f) {
			float angle = Mathf.Atan2(y, x) * Mathf.Rad2Deg;
			angle += 90f;
			transform.localRotation = Quaternion.Slerp (transform.localRotation, Quaternion.AngleAxis (angle, Vector3.up), .1f);
		}
	}

	void UpdateStrafeControls() {
		float inputY = InputPlus.GetData (player.controllerID, ControllerVarEnum.ShoulderBottom_right);
		inputY = Mathf.Clamp (inputY, 0, 1);

		if (inputY > 0 && !isPrimaryFiring) {
			isPrimaryFiring = true;
			InvokeRepeating ("FirePrimary", float.Epsilon, primaryInterval);
		} else if (inputY == 0 && isPrimaryFiring) {
			isPrimaryFiring = false;
			CancelInvoke ("FirePrimary");
		}

		if (InputPlus.GetData (player.controllerID, ControllerVarEnum.ThumbRight) == 1f && !isSecondaryFiring) {
			InvokeRepeating ("FireSecondary", float.Epsilon, secondaryInterval);
			isSecondaryFiring = true;
		} else if (InputPlus.GetData (player.controllerID, ControllerVarEnum.ThumbRight) == 0f && isSecondaryFiring) {
			CancelInvoke ("FireSecondary");
			isSecondaryFiring = false;
		}
			
	}

	void UpdatePlaneControls() {

		if (InputPlus.GetData (player.controllerID, ControllerVarEnum.FP_bottom) == 1f && !isPrimaryFiring) {
			InvokeRepeating ("FirePrimary", float.Epsilon, primaryInterval);
			isPrimaryFiring = true;
		} else if (InputPlus.GetData (player.controllerID, ControllerVarEnum.FP_bottom) == 0f && isPrimaryFiring) {
			CancelInvoke ("FirePrimary");
			isPrimaryFiring = false;
		}

		if (InputPlus.GetData (player.controllerID, ControllerVarEnum.FP_left) == 1f && !isSecondaryFiring) {
			InvokeRepeating ("FireSecondary", float.Epsilon, secondaryInterval);
			isSecondaryFiring = true;
		} else if (InputPlus.GetData (player.controllerID, ControllerVarEnum.FP_left) == 0f && !isSecondaryFiring) {
			CancelInvoke ("FireSecondary");
			isSecondaryFiring = false;
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