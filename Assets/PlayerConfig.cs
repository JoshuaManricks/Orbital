using UnityEngine;
using System;
using System.Collections;

public class PlayerConfig : MonoBehaviour {

	public FirstPersonController playerController;

	Camera camera;

	// Use this for initialization
	void Start () {
		playerController = GetComponent<FirstPersonController>();
		camera = GetComponentInChildren<Camera>();
	}

	// Update is called once per frame
	public void Configure (PlayerID data, int playerCount) {
		if (playerController == null) playerController = GetComponent<FirstPersonController>();
		if (camera == null) camera = GetComponentInChildren<Camera>();

		playerController.playerID = data;

		playerController.init();

		ConfigureCamera(playerCount);

	}

	void ConfigureCamera(int playerCount) {

		if (playerCount > 2 ) {
			if (playerController.playerID == PlayerID.P1) camera.rect = new Rect(0.0f, 0.5f, 0.5f, 0.5f);
			if (playerController.playerID == PlayerID.P2) camera.rect = new Rect(0.5f, 0.5f, 0.5f, 0.5f);
			if (playerController.playerID == PlayerID.P3) camera.rect = new Rect(0.0f, 0.0f, 0.5f, 0.5f);
			if (playerController.playerID == PlayerID.P4) camera.rect = new Rect(0.5f, 0.0f, 0.5f, 0.5f);

		} else {
			if (playerController.playerID == PlayerID.P1) camera.rect = new Rect(0.0f, 0.0f, 0.5f, 1.0f);
			if (playerController.playerID == PlayerID.P2) camera.rect = new Rect(0.5f, 0.0f, 0.5f, 1.0f);
		}

	}

}

[Serializable]
public class ShipConfigData {
	public PlayerID playerID = PlayerID.none;
	public ShipType ship;
//	public ShipType shipType;

}