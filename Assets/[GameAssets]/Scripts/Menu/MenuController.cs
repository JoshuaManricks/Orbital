﻿using UnityEngine;
using System.Collections;

public class MenuController : MonoBehaviour {

	GameController gameController;

	public PlanetController[] planets;
	public int planetID;

	public int minPlayers = 2;
	public int confirmedPlayers;

	public ShipSelector player1;
	public ShipSelector player2;
	public ShipSelector player3;
	public ShipSelector player4;

	bool checkForPlayers = true;

	public float rotateSpeed = .2f;

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

		transform.RotateAround (Vector3.zero, Vector3.up, rotateSpeed);
	}

	void SpawnPlayers() {
		gameController.StartMatch(planetID);
		gameObject.SetActive(false);
	}

	bool HaveAllPlayersJoined() {
		
		confirmedPlayers = 0;

		if (player1.selectorState == SelectorState.ShipSelected) confirmedPlayers++;
		if (player2.selectorState == SelectorState.ShipSelected) confirmedPlayers++;
		if (player3.selectorState == SelectorState.ShipSelected) confirmedPlayers++;
		if (player4.selectorState == SelectorState.ShipSelected) confirmedPlayers++;

		return (confirmedPlayers >= minPlayers);

	}
}
