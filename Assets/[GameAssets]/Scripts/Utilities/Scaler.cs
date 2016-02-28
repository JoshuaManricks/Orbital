using UnityEngine;
using System.Collections;

public class Scaler : MonoBehaviour {

	public float speed = 1f;
	public Vector3 size;

	// Use this for initialization
	void Start () {
		
		transform.localScale = Vector3.zero;
	}
	
	// Update is called once per frame
	void Update () {
		float step = speed * Time.deltaTime;
		transform.localScale = Vector3.MoveTowards(transform.localScale, size, step);

		if (transform.localScale == size) transform.localScale = Vector3.zero;
	}
}
