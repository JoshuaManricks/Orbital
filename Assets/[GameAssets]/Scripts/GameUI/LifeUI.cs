using UnityEngine;
using System.Collections;

public class LifeUI : MonoBehaviour {


	GameObject heart;
	GameObject skull;

	// Use this for initialization
	void Start () {
		heart = transform.GetChild (0).gameObject;
		skull = transform.GetChild (1).gameObject;
		skull.SetActive (false);
	}

	public void Die() {
		heart.SetActive (false);
		skull.SetActive (true);
	}

	public void Reset() {
		heart.SetActive (true);
		skull.SetActive (false);
	}
}
