using UnityEngine;
using System.Collections;

public class PowerUp : MonoBehaviour {

	public PowerUpType type;

	// Use this for initialization
	void Start () {

		transform.LookAt (Vector3.zero);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	protected virtual void OnTriggerEnter(Collider other) {
//		other.GetComponent<WeaponControls>();
		Destroy(gameObject);
	}
}

public enum PowerUpType {
	Boost,
	Weapon,
	Repair
}