using UnityEngine;
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

	CountDown countDown;

	// Use this for initialization
	void Start () {
		gameController = GameObject.FindObjectOfType<GameController>();

		countDown = GetComponentInChildren<CountDown>();
	}
	
	// Update is called once per frame
	void Update () {
		if (checkForPlayers) {
			if (HaveAllPlayersJoined()) {
				checkForPlayers = false;
//				SpawnPlayers();
				StartCountDown();
			}
		}
	}

	void StartCountDown() {
		Debug.Log("StartCountDown");
		countDown.StartCountDown();
	}

	public void CancelCountDown() {
		Debug.Log("CancelCountDown");
		checkForPlayers = true;
		countDown.CancelCountDown();
	}

	public void SelectPlanet() {
		Debug.Log("SelectPlanet");
		
		player1.selectorState = SelectorState.WaitingForPlanetSelection;
		player2.selectorState = SelectorState.WaitingForPlanetSelection;
		player3.selectorState = SelectorState.WaitingForPlanetSelection;
		player4.selectorState = SelectorState.WaitingForPlanetSelection;

	}
		
	public void ChangePlanet(int direction) {

		planets[planetID].gameObject.SetActive(false);
		planetID += direction;
		planetID = Mathf.Clamp(planetID, 0, planets.Length-1);
		planets[planetID].gameObject.SetActive(true);

	}

	public void ConfirmPlanet() {
//		StartCoroutine("StartGame");
		SpawnPlayers();
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
