using UnityEngine;
using System.Collections;

public class WeaponPowerUp : PowerUp {
	public WeaponName weaponName;

	// Use this for initialization
	void Start () {
	
	}


	protected override void OnTriggerEnter(Collider other) {
//		Debug.Log(other.gameObject.name);

		if (other.gameObject.tag == "Player") {
			other.gameObject.GetComponentInChildren<WeaponControls>().ChangeWeapon(weaponName);
			Destroy(gameObject);
		}
	}
}
