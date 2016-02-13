using UnityEngine;
using System.Collections;

public class BoostPowerUp : PowerUp {

	public PowerUpSpawnPoint spawnPoint;
	public int healAmount = 5;

	// Use this for initialization
	void Start () {
		
	}

	protected override void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Player") {
			spawnPoint.isAvailable = true;

			other.gameObject.AddComponent<InfiniteBoostModifier> ();
			Destroy (gameObject);

		} else if (other.gameObject.tag == "PowerUpSpawnPoint") {

			spawnPoint = other.gameObject.GetComponent<PowerUpSpawnPoint> ();
			spawnPoint.isAvailable = false;
		}
	}

}
