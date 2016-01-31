using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public GameObject[] players;

	int spawnID; 
	public Transform[] spawnPoints;

//	public GameObject player1;
//	public GameObject player2;
//	public GameObject player3;
//	public GameObject player4;

	public ShipConfigData player1Config;
	public ShipConfigData player2Config;
	public ShipConfigData player3Config;
	public ShipConfigData player4Config;

	public GameObject planeShip;
	public GameObject tankShip;
	public GameObject strafeShip;

	[HideInInspector]
	public float spawnDelay = 5f;

	// Use this for initialization
	void Start () {

		DontDestroyOnLoad(gameObject);

		spawnPoints = gameObject.GetComponentsInChildren<Transform>();

//		players = GameObject.FindGameObjectsWithTag("Player");

		spawnID = Random.Range(0, spawnPoints.Length-1);

	}
	
	// Update is called once per frame
	void Update () {
	
	}


	public void StartMatch() {

		Debug.Log("+++++ START MATCH +++++");
	
		//create all the players
		if (player1Config.playerID != PlayerID.none) SpawnShip(player1Config);
		if (player2Config.playerID != PlayerID.none) SpawnShip(player2Config);
		if (player3Config.playerID != PlayerID.none) SpawnShip(player3Config);
		if (player4Config.playerID != PlayerID.none) SpawnShip(player4Config);

	}

	public void SetPlayerShip(PlayerID player, ShipType shipType) {
		totalPlayers++;

		ShipConfigData config = new ShipConfigData();
		config.playerID = player;
		config.ship = shipType;

		switch (player) {
		case PlayerID.P1:
			player1Config = config;
			return;
		case PlayerID.P2:
			player2Config = config;
			return;
		case PlayerID.P3:
			player3Config = config;
			return;
		case PlayerID.P4:
			player4Config = config;
			return;
		}

	}
	public int totalPlayers = 0;
	public void RemovePlayerShip(PlayerID player) {
		totalPlayers--;

		ShipConfigData config = new ShipConfigData();
		config.playerID = PlayerID.none;
//		config.ship = shipType;

		switch (player) {
		case PlayerID.P1:
			player1Config = config;
			return;
		case PlayerID.P2:
			player2Config = config;
			return;
		case PlayerID.P3:
			player3Config = config;
			return;
		case PlayerID.P4:
			player4Config = config;
			return;
		}
	}
		
	GameObject GetShipPrefab(ShipType ship) {

		if (ship == ShipType.Plane) return planeShip;
		else if (ship == ShipType.Tank) return tankShip;
		else if (ship == ShipType.Strafe) return strafeShip;
		else return planeShip;
	}

	public void ReSpawnPlayer(PlayerID id) {
		if (id == PlayerID.P1) StartCoroutine("SpawnDelay", player1Config);
		if (id == PlayerID.P2) StartCoroutine("SpawnDelay", player2Config);
		if (id == PlayerID.P3) StartCoroutine("SpawnDelay", player3Config);
		if (id == PlayerID.P4) StartCoroutine("SpawnDelay", player4Config);
	}

	IEnumerator SpawnDelay(ShipConfigData config) {
		yield return new WaitForSeconds(spawnDelay);

		SpawnShip(config);
	}

	void SpawnShip(ShipConfigData config) {
		
//		PlayerConfig playerConfig = Instantiate(GetShipPrefab(config.ship),spawnPoints[spawnID].position, Quaternion.identity) as PlayerConfig;
		GameObject go = Instantiate(GetShipPrefab(config.ship),spawnPoints[spawnID].position, Quaternion.identity) as GameObject;

		go.GetComponent<PlayerConfig>().Configure(config.playerID, totalPlayers);

		MoveSpawnPoint();

		//regenerate the list of players
		players = null;
		players = GameObject.FindGameObjectsWithTag("Player");
	}

	void MoveSpawnPoint() {
		spawnID++;
		if (spawnID == spawnPoints.Length) spawnID = 0;
	}

}
