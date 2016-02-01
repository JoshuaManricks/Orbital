using UnityEngine;
using System.Collections;

public class MenuController : MonoBehaviour {

	GameController gameController;


	public int minPlayers = 2;
	public int confirmedPlayers;

	public ShipSelector player1;
	public ShipSelector player2;
	public ShipSelector player3;
	public ShipSelector player4;

	bool checkForPlayers = true;

	// Use this for initialization
	void Start () {
		gameController = GameObject.FindObjectOfType<GameController>();
	}
	
	// Update is called once per frame
	void Update () {
		if (checkForPlayers) {
			if (HaveAllPlayersJoined()) {
				checkForPlayers = false;
				SpawnPlayers();
			}
		}
	}

	void SpawnPlayers() {
		gameController.StartMatch();
		gameObject.SetActive(false);
	}

	bool HaveAllPlayersJoined() {
		confirmedPlayers = 0;

		if (player1.selctionConfirmed) confirmedPlayers++;
		if (player2.selctionConfirmed) confirmedPlayers++;
		if (player3.selctionConfirmed) confirmedPlayers++;
		if (player4.selctionConfirmed) confirmedPlayers++;

		return (confirmedPlayers >= minPlayers);
	}
}
