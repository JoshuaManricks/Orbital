using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {


	public GameObject[] players;

	public Transform[] spawnPoints;

	public GameObject Player1;
	public GameObject Player2;
	public GameObject Player3;
	public GameObject Player4;

	public float spawnDelay = 5f;

	// Use this for initialization
	void Start () {
		spawnPoints = gameObject.GetComponentsInChildren<Transform>();
		players = GameObject.FindGameObjectsWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	public void SpawnPlayer(PlayerID id) {

		if (id == PlayerID.P1) StartCoroutine("SpawnShip", Player1);
		if (id == PlayerID.P2) StartCoroutine("SpawnShip", Player2);
		if (id == PlayerID.P3) StartCoroutine("SpawnShip", Player3);
		if (id == PlayerID.P4) StartCoroutine("SpawnShip", Player4);



	}

	IEnumerator SpawnShip(GameObject ship) {
		yield return new WaitForSeconds(spawnDelay);
		Instantiate(ship,spawnPoints[Random.Range(1, spawnPoints.Length-1)].position, Quaternion.identity);

		players = null;
		players = GameObject.FindGameObjectsWithTag("Player");
	}


}
