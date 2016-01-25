using UnityEngine;
using System.Collections;

public class WeaponPowerUp : PowerUp {
	public WeaponName weaponName;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(new Vector3(2,1,2));
	}

	protected override void OnTriggerEnter(Collider other) {
		Debug.Log(other.gameObject.name);

		if (other.gameObject.tag == "Player") {
			other.gameObject.GetComponentInChildren<WeaponControls>().ChangeWeapon(weaponName);
			Destroy(gameObject);
		}
	}
}
