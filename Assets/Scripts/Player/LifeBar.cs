using UnityEngine;
using System.Collections;

public class LifeBar : MonoBehaviour {

	public LifeController playerLife;
	
	public Material green, red;

//	public Direction direction;

	GameObject bar;

	// Use this for initialization
	void Start () {
		bar = transform.GetChild(0).gameObject;

//		Debug.Log("EB " +GameController.HorizontalHeightSeen);

		//if (direction == Direction.Right) transform.position = new Vector3((GameController.HorizontalHeightSeen/2) -4f, GameController.orthoSize-3f, 0f);
		//if (direction == Direction.Left) transform.position = new Vector3(-(GameController.HorizontalHeightSeen/2) + 4f, GameController.orthoSize-3f, 0f);

	}

	float lifePercentage;

	void Update () {

		lifePercentage = (playerLife.currentLife / playerLife.maxLife);

		transform.localScale = new Vector3(lifePercentage ,1f,1f);

		//TODO : optimize this not to trigger every frame
		if (lifePercentage <= .2){
			bar.GetComponent<Renderer>().material = red;
		} else {
			bar.GetComponent<Renderer>().material = green;
		}
			
	}
}
