using UnityEngine;
using System.Collections;

public class PowerUpSpawner : MonoBehaviour {

//	public delegate void PowerUpEvent(PowerUpEventData data);
//	public static event PowerUpEvent PowerUpCollected;

	public GameObject[] powerups;

	public float spawnTime = 20f;
	public GameObject[] spawnPoints;

	// Use this for initialization
	void Start () {
		
	}

	public void StartGame() {
		InvokeRepeating("SpawnPowerup", 1f, spawnTime);
	}

	void SpawnPowerup () {
//		Debug.Log("SpawnPowerup");
		PowerUpSpawnPoint spawnPoint = spawnPoints[Random.Range(0,spawnPoints.Length)].GetComponent<PowerUpSpawnPoint>();

		if (spawnPoint.isAvailable) {
			GameObject powerup = Instantiate (powerups [Random.Range (0, powerups.Length - 1)]);

			powerup.transform.position = spawnPoint.gameObject.transform.position;
			powerup.transform.LookAt (Vector3.zero);
		}
	}

	// Update is called once per frame
	void Update () {
	
	}
}

public class PowerUpEventData {

	public WeaponName type;
	public float amount;
	public PlayerID playerId;

}