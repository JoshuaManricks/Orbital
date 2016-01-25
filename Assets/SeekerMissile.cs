using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;

public class SeekerMissile : ProjectileBase {

	public Transform target;
	Seeker seeker;

	bool canMove;

	public List<Vector3> path;
	public float TurnSpeed = 1f;

	// Use this for initialization
	void Start () {

		//target = GameObject.FindGameObjectWithTag("Target").transform;
		seeker = GetComponent<Seeker>();

		seeker.StartPath (transform.position, target.position, OnPathComplete);
	}

	public void OnPathComplete (Path p) {
		//We got our path back
		if (p.error) {
			// Nooo, a valid path couldn't be found
		} else {
			// Yay, now we can get a Vector3 representation of the path
			// from p.vectorPath
			path = p.vectorPath;
			canMove = true;

			//transform.LookAt(path[1]);
		}

		seeker.StartPath (transform.position, target.position, OnPathComplete);
	}


	// Update is called once per frame
	protected override void Update () {
		
		Debug.DrawLine(transform.position, target.position, Color.red);

		timer+=Time.deltaTime;

		if (timer >= lifeTime) Destroy(gameObject);
		if (!canMove) return;

		////////////////////////
//		public float RotationSpeed = 1f;
		Vector3 _direction = (path[1] - transform.position).normalized;
		Quaternion _lookRotation = Quaternion.LookRotation(_direction);
		transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * TurnSpeed);
		////////////////////////

		Vector3 moveDir = new Vector3(0,0,1);//.normalized;
		Vector3 targetMoveAmount = moveDir * speed;
		moveAmount = targetMoveAmount;//Vector3.SmoothDamp(moveAmount,targetMoveAmount,ref smoothMoveVelocity,.15f);

	}

	void OnTriggerEnter(Collider other) {
		Debug.Log(other.name);
		if (other.gameObject.tag == "Player") Destroy(gameObject);
	}
}