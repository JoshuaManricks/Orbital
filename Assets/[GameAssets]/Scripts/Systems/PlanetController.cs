using UnityEngine;
using System;
using System.Collections;

[Serializable]
public class PlanetController : MonoBehaviour {

	public PlayerSpawnPoint[] playerSpawnPoints;

	public bool randomPlayerSpawnPosition = false;

	// Use this for initialization
	void Start () {
		playerSpawnPoints = gameObject.GetComponentsInChildren<PlayerSpawnPoint>();
	}
	
	// Update is called once per frame
//	void Update () {
	
//	}
}
