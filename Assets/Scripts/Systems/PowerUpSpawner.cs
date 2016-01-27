using UnityEngine;
using System.Collections;

public class PowerUpSpawner : MonoBehaviour {

	public delegate void PowerUpEvent(PowerUpEventData data);
	public static event PowerUpEvent PowerUpCollected;

	public GameObject[] powerups;

	public float spawnTime = 20f;
	public GameObject[] spawnPoints;

	// Use this for initialization
	void Start () {
		InvokeRepeating("SpawnPowerup", 1f, spawnTime);
	}

	void SpawnPowerup () {
//		Debug.Log("SpawnPowerup");

		GameObject powerup = Instantiate(powerups[Random.Range(0,powerups.Length-1)]);

		powerup.transform.position = spawnPoints[Random.Range(0,spawnPoints.Length-1)].transform.position;
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