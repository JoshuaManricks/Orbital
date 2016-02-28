using UnityEngine;
using System.Collections;
using InputPlusControl;

public class ShipSelector : MonoBehaviour {

	public SelectorState selectorState = SelectorState.WaitingToJoin;

	public PlayerID playerID;
	int controllerID;
	int selectionIndex = 0;

	public ShipSelectionDisplay[] shipSelectionDisplay;
	GameController gameController;

	public GameObject joinMessage;
	public GameObject shipSelector;

	public bool useKeyboard = false;

	// Use this for initialization
	void Start () {
		
		gameController = GameObject.FindObjectOfType<GameController>();

		SetupController();

		//get all the ships
		shipSelectionDisplay = GetComponentsInChildren<ShipSelectionDisplay>();
		foreach (ShipSelectionDisplay display in shipSelectionDisplay) display.gameObject.SetActive(false);
		shipSelectionDisplay[selectionIndex].gameObject.SetActive(true);

		shipSelector.SetActive (false);
	}
	
	// Update is called once per frame
	bool canPress = true;
//	public bool selctionConfirmed = false;
//	bool joinedGame = false;

	void Update () {
		if (useKeyboard) KeyBoardControls();
		else GamePadControls();

	}

	void KeyBoardControls() {
		
		if (selectorState == SelectorState.WaitingToJoin) {
			//press A to add player
			if (Input.GetKeyDown(KeyCode.A)) {
				JoinGame();
				StartCoroutine("ButtonDelay");
			}
			return;
		}

		//game joined
//		if (selectorState == SelectorState.WaitingForShipSelection) {

			//no selection made
//			if (!selctionConfirmed) {
			if (selectorState == SelectorState.WaitingForShipSelection) {
				//press B to remove player 
				if (Input.GetKeyDown(KeyCode.D))  {
					QuitGame();

					//ship select up and down
				} else if (Input.GetKeyDown(KeyCode.UpArrow)) {
					NextSelection(1);

				} else if (Input.GetKeyDown(KeyCode.DownArrow))  {
					NextSelection(-1);
				
					//press A to confirm player selection
				} else if (Input.GetKeyDown(KeyCode.A))  {
					ConfirmSelection();
				}
				return;
			}

			//selection made
//			if (selctionConfirmed) {
			if (selectorState == SelectorState.ShipSelected) {
				//press B to cancel player selection
				if (Input.GetKeyDown(KeyCode.D))  {
					UnConfirmSelection();
				}
				return;
			}
//		}

	}


	void GamePadControls() {
		if (!canPress) return;

		if (selectorState == SelectorState.WaitingToJoin) {
		//press A to add player
			if (InputPlus.GetData (controllerID, ControllerVarEnum.FP_bottom) == 1) {
				JoinGame();
				StartCoroutine("ButtonDelay");
			}
			return;
		}

		//game joined
//		if (joinedGame) {

			//no selection made
//			if (!selctionConfirmed) {
			if (selectorState == SelectorState.WaitingForShipSelection) {
				//press B to remove player 
				if (InputPlus.GetData (controllerID, ControllerVarEnum.FP_right) == 1)  {
					QuitGame();
					StartCoroutine("ButtonDelay");

				//ship select up and down
				} else if (InputPlus.GetData (controllerID, ControllerVarEnum.dpad_down) == 1) {
					NextSelection(1);
					StartCoroutine("ButtonDelay");
				} else if (InputPlus.GetData (controllerID, ControllerVarEnum.dpad_up) == 1)  {
					NextSelection(-1);
					StartCoroutine("ButtonDelay");

				//press A to confirm player selection
				} else if (InputPlus.GetData (controllerID, ControllerVarEnum.FP_bottom) == 1)  {
					ConfirmSelection();
					StartCoroutine("ButtonDelay");
				}
				return;
			}

			//selection made
//			if (selctionConfirmed) {
			if (selectorState == SelectorState.ShipSelected) {
				//press B to cancel player selection
				if (InputPlus.GetData (controllerID, ControllerVarEnum.FP_right) == 1)  {
					UnConfirmSelection();
					StartCoroutine("ButtonDelay");
				}
				return;
			}
//		}


	}

	void JoinGame() {
		Debug.Log("JOIN GAME");
//		joinedGame = true;
		selectorState = SelectorState.WaitingForShipSelection;
		shipSelector.SetActive (true);
		joinMessage.SetActive (false);
	}

	void QuitGame() {
		Debug.Log("QUIT GAME");
//		joinedGame = false;
		selectorState = SelectorState.WaitingToJoin;
		shipSelector.SetActive (false);
		joinMessage.SetActive (true);
	}

	void UnConfirmSelection() {
		Debug.Log("Unconfiurm selection");
		selectorState = SelectorState.WaitingForShipSelection;
//		selctionConfirmed = false;

		gameController.RemovePlayerShip(playerID);
	}

	void ConfirmSelection() {
		Debug.Log("CONFIRM  selection");
//		selctionConfirmed = true;
		selectorState = SelectorState.ShipSelected;

		gameController.SetPlayerShip(playerID, shipSelectionDisplay[selectionIndex].shipType);
	}

	void NextSelection(int direction) {
		shipSelectionDisplay[selectionIndex].gameObject.SetActive(false);

		selectionIndex += direction;
		if(selectionIndex == shipSelectionDisplay.Length) selectionIndex = 0;
		if(selectionIndex < 0) selectionIndex = shipSelectionDisplay.Length-1;

		shipSelectionDisplay[selectionIndex].gameObject.SetActive(true);

	}

	IEnumerator ButtonDelay() {
		canPress = false;
		yield return new WaitForSeconds(.5f);
		canPress = true;
	}

	void SetupController() {
		switch (playerID) {
		case PlayerID.P1:
			controllerID = 1;
			return;
		case PlayerID.P2:
			controllerID = 2;
			return;
		case PlayerID.P3:
			controllerID = 3;
			return;
		case PlayerID.P4:
			controllerID = 4;
			return;
		}
	}
}


public enum SelectorState {
	WaitingToJoin,
	WaitingForShipSelection,
	ShipSelected,
	WaitingForPlanetSelection,
	PlanetSelected
}