using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WeaponControls : MonoBehaviour {
	
	public float primaryInterval = 0.1f;
	public float secondaryInterval = 2f;

	public Transform firePosition;

	public WeaponComponent[] weapons;

	public WeaponComponent primaryWeapon;
	public WeaponComponent secondaryWeapon;

	FirstPersonController player;

	// Use this for initialization
	void Start () {

		firePosition = transform.GetChild(0);

		weapons = GetComponentsInChildren<WeaponComponent>();

		DisableAllWeapons();

		ChangeWeapon(WeaponName.SingleShot);
		ChangeWeapon(WeaponName.None);

		player  = transform.parent.gameObject.GetComponent<FirstPersonController>();

	}

	// Update is called once per frame
	void Update () {
		if (player.dummy) return;

		// Fire bullet code

		if (player.playerID == PlayerID.P2) {
			if (Input.GetKeyDown (KeyCode.Joystick1Button16))
				InvokeRepeating ("FirePrimary", float.Epsilon, primaryInterval);
			if (Input.GetKeyUp (KeyCode.Joystick1Button16))
				CancelInvoke ("FirePrimary");

			if (Input.GetKeyDown (KeyCode.Joystick1Button17))
				InvokeRepeating ("FireSecondary", float.Epsilon, secondaryInterval);
			if (Input.GetKeyUp (KeyCode.Joystick1Button17))
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
		primaryWeapon.Fire();
	}

	void FireSecondary()
	{
		secondaryWeapon.Fire();
	}

	public void DisableAllWeapons() {
		
		foreach (WeaponComponent weapon in weapons) {

			weapon.gameObject.SetActive(false);

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