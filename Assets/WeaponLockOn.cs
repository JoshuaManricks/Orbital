using UnityEngine;
using System.Collections;

public class WeaponLockOn : MonoBehaviour {

	public GameObject lockedShip;
	public GameObject myPlayer;

	public GameObject marker;

	public float lockDistance = 5f;

	GameController gameController;
	// Use this for initialization
	void Start () {
		
		gameController = FindObjectOfType<GameController>();
		myPlayer = transform.parent.parent.gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		ScanForPlayers();

		UpdateLockMarker();
	}

	void UpdateLockMarker() {
		if (lockedShip != null) {

			if (lockedShip.GetComponent<FirstPersonController>().isDead) {
				lockedShip = null;
				marker.SetActive(false);
				return;
			}
				
			marker.SetActive(true);
			marker.transform.position = lockedShip.transform.position;


		}else {
			marker.SetActive(false);
		}

	}

	void ScanForPlayers() {


		if (lockedShip != null) {

			if (Vector3.Distance(gameObject.transform.position, lockedShip.transform.position) <= lockDistance) return;
			else lockedShip = null;
			Debug.Log("Lost Lock");		
		}

		Debug.Log("looksing for ship");

		foreach (GameObject ship in gameController.players) {

			if (ship != myPlayer && ship != null) {
				
//				Debug.Log(Vector3.Distance(gameObject.transform.position, ship.transform.position));
				if (Vector3.Distance(gameObject.transform.position, ship.transform.position) <= lockDistance) {

					if (!ship.GetComponent<FirstPersonController>().isDead) lockedShip = ship;
				}
			}
		}

	}

}
