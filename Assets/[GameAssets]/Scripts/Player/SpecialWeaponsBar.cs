using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpecialWeaponsBar : MonoBehaviour {

	public int totalShots;
	public int currentShots;

	public GameObject[] nodes;

	// Use this for initialization
	void Start () {
//		SetShots (0);
	}
	
	public void SetShots (int amount) {
//		Debug.Log ("SetShots "+amount);

		totalShots = currentShots = amount;
		for (int x = 0; x < nodes.Length; x++) {
			if (x >= amount) nodes [x].SetActive(false);
			else nodes [x].SetActive(true);
		}
	}
		
	public void UseShot () {
		currentShots --;
		nodes [currentShots].SetActive(false);
	}

	void Update () {

	}

}