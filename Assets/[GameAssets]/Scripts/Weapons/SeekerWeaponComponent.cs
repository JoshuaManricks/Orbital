using UnityEngine;
using System.Collections;

public class SeekerWeaponComponent : WeaponComponent {

	void Update() {

		if (GetComponent<WeaponLockOn> ().lockedShip == null)
			canFire = false;
		else
			canFire = true;

	}

	public override void Fire(FirstPersonController player) {
		foreach (Transform child in firePositions) {
			SeekerMissile clone = (SeekerMissile)Instantiate(projectile);
			clone.transform.position = child.position;
			clone.transform.rotation = child.rotation;
			clone.target = GetComponent<WeaponLockOn>().lockedShip.transform;
			clone.owner = player;
		}
	}

}
