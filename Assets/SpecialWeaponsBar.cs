using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpecialWeaponsBar : MonoBehaviour {

	public int totalShots;
	public int currentShots;

	public GameObject[] nodes;

	// Use this for initialization
	void Start () {
		
	}
	
	public void SetShots (int amount) {
		totalShots = currentShots = amount;
		for (int x = 0; x < totalShots; x++) {
			nodes [x].SetActive(true);
		}
	}
		
	public void UseShot () {
		currentShots --;
		nodes [currentShots].SetActive(false);

		Debug.Log ("currentShots "+currentShots);
	}

	void Update () {

	}

}