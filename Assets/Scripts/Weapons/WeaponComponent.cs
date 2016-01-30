using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WeaponComponent : MonoBehaviour {

	public bool canFire = true;
	public WeaponGrade grade;
	public WeaponName name;
	public ProjectileBase projectile;

	[HideInInspector]
	public List<Transform> firePositions;

	// Use this for initialization
	void Start () {
		//get all the firing positions
		for (int x = 0; x <transform.childCount; x++){
			Transform child = transform.GetChild(x);
			if (child.tag == "FirePosition") firePositions.Add(child);
		}
	}

	public virtual void Fire(float additionSpeed, FirstPersonController player) {

		foreach (Transform child in firePositions) {
			ProjectileBase clone = (ProjectileBase)Instantiate(projectile);
			clone.transform.position = child.position;
			clone.transform.rotation = child.rotation;
			clone.shipSpeed = additionSpeed;
			clone.owner = player;
		}
	}

	public virtual void Fire(FirstPersonController player) {

		foreach (Transform child in firePositions) {
			ProjectileBase clone = (ProjectileBase)Instantiate(projectile);
			clone.transform.position = child.position;
			clone.transform.rotation = child.rotation;
			clone.owner = player;
		}
	}
}
