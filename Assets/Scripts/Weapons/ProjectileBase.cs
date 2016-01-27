﻿using UnityEngine;
using System.Collections;

[RequireComponent (typeof (GravityBody))]
public class ProjectileBase : MonoBehaviour {

	// public vars
	public float damage;
	public float speed = 6;
	public float lifeTime = 1;

	public float collisionOffsetTime = .1f;

	protected float timer = 0;

	// System vars
	protected Vector3 moveAmount;
	protected Vector3 smoothMoveVelocity;
	protected Rigidbody rigidbody;

	public GameObject owner;

	void Awake() {
		rigidbody = GetComponent<Rigidbody> ();
	}

	protected virtual void Update() {

		timer+=Time.deltaTime;

		if (timer >= lifeTime) Destroy(gameObject);

		Vector3 moveDir = new Vector3(0,0,1);//.normalized;
		Vector3 targetMoveAmount = moveDir * speed;
		moveAmount = targetMoveAmount;

		RayDown ();

	}

	protected virtual void FixedUpdate() {
		// Apply movement to rigidbody
		Vector3 localMove = transform.TransformDirection(moveAmount) * Time.fixedDeltaTime;

		rigidbody.MovePosition(rigidbody.position + localMove);
	}

	protected virtual void OnCollisionEnter(Collision collision) {
		if (timer < collisionOffsetTime) return;

		if (collision.gameObject.tag == "Player") {
			Debug.Log(collision.gameObject.name);
			collision.gameObject.GetComponent<LifeController>().TakeDamage(damage);

			Destroy(gameObject);
		}
	}


	void RayDown() {
		// Grounded check
		Ray ray = new Ray(transform.position, -transform.up);
		RaycastHit hit;

		if (Physics.Raycast(ray, out hit, 5f)) {
			Debug.Log (hit.collider.gameObject.name + " : "+hit.distance);

			if (hit.distance > 1.7) down = .2f;
			else down = 0;
		}

	}

	float down = 0f;
}
