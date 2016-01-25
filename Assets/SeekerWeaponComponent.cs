using UnityEngine;
using System.Collections;

public class SeekerWeaponComponent : WeaponComponent {




	public override void Fire() {
		
		if (GetComponent<WeaponLockOn>().lockedShip == null) return;

		foreach (Transform child in firePositions) {

			SeekerMissile clone = (SeekerMissile)Instantiate(projectile);
			clone.transform.position = child.position;
			clone.transform.rotation = child.rotation;
				
			Debug.Log("clone.target "+clone.target);

			clone.target = GetComponent<WeaponLockOn>().lockedShip.transform;

			Debug.Log("clone.target "+clone.target);

		}
	}


}
