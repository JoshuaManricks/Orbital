using UnityEngine;
using System;
using System.Collections;

[Serializable]
public class PlanetController : MonoBehaviour {

	public PlayerSpawnPoint[] playerSpawnPoints;

	public bool randomPlayerSpawnPosition = false;
	PowerUpSpawner powerUpSpawner;
	// Use this for initialization
	void Start () {
		playerSpawnPoints = gameObject.GetComponentsInChildren<PlayerSpawnPoint>();
		powerUpSpawner = gameObject.GetComponentInChildren<PowerUpSpawner>();
	}

	public void StartGame() {
		powerUpSpawner.StartGame();
	}
}
