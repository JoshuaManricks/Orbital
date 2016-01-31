using UnityEngine;
using System.Collections;

public class PowerUp : MonoBehaviour {

	public PowerUpType type;

	// Use this for initialization
	void Start () {
	
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