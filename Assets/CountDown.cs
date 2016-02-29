using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CountDown : MonoBehaviour {

	public Text countDownText;
	MenuController menuController;

	// Use this for initialization
	void Start () {
		menuController = GameObject.FindObjectOfType<MenuController>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void CancelCountDown() {
		StopCoroutine("Count");
		countDownText.text = "";
		count = 3;
	}

	public void StartCountDown() {
		count = 3;
		StartCoroutine("Count");
	}

	int count = 3;

	IEnumerator Count() {
		countDownText.text = count.ToString();

		while (count != 0) {
			yield return new WaitForSeconds(1);
			count --;
			countDownText.text = count.ToString();
		}

		menuController.SelectPlanet();
	}
}
