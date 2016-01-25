using UnityEngine;
using System.Collections;

[RequireComponent (typeof (GravityBody))]
public class FirstPersonController : MonoBehaviour {
	
	// public vars
	public PlayerID playerID;
	public float mouseSensitivityX = 1;
	public float mouseSensitivityY = 1;
	public float walkSpeed = 6;
	public float jumpForce = 220;
	public LayerMask groundedMask;

	public float boost = 0f;

	public bool dummy = false;
	public bool autoMove = false;
	public float inputY;

	public bool isDead = false;
	public GameObject[] destroyOnDeath;

	//////////////////////////////////////////
	GameController gameController;
	WeaponControls weaponControls;

	// System vars
	//bool grounded;
	Vector3 moveAmount;
	Vector3 smoothMoveVelocity;
	float verticalLookRotation;
	Transform cameraTransform;
	Rigidbody rigidbody;
	
	void Awake() {
		//Cursor.lockState = CursorLockMode.Locked;
		//Cursor.visible = false;
		//cameraTransform = Camera.main.transform;
		rigidbody = GetComponent<Rigidbody> ();

		if (dummy) {
			GetComponentInChildren<Camera>().enabled =false;
		}

		weaponControls = GetComponentInChildren<WeaponControls>();
		gameController = FindObjectOfType<GameController>();
	}

	void Update() {
		
		if (isDead || dummy) return;

//		if (!autoMove) {
		// Look rotation:
//		transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * mouseSensitivityX);
			transform.Rotate(Vector3.up * Input.GetAxisRaw("Horizontal_"+playerID) * mouseSensitivityX);
//		verticalLookRotation += Input.GetAxis("Mouse Y") * mouseSensitivityY;
//		verticalLookRotation = Mathf.Clamp(verticalLookRotation,-60,60);
//		cameraTransform.localEulerAngles = Vector3.left * verticalLookRotation;
//		}
		// Calculate movement:
		//float inputX = Input.GetAxisRaw("Horizontal");
		inputY = Input.GetAxisRaw("Vertical_"+playerID);
		if (autoMove)  inputY = 1;

		Vector3 moveDir = new Vector3(0,0, inputY).normalized;
		Vector3 targetMoveAmount = moveDir * (walkSpeed+boost);
		moveAmount = Vector3.SmoothDamp(moveAmount,targetMoveAmount,ref smoothMoveVelocity,.15f);


		// Jump
		/*if (Input.GetButtonDown("Jump")) {
			if (grounded) {
				rigidbody.AddForce(transform.up * jumpForce);
			}
		}
		
		// Grounded check
		Ray ray = new Ray(transform.position, -transform.up);
		RaycastHit hit;
		
		if (Physics.Raycast(ray, out hit, 1 + .1f, groundedMask)) {
			grounded = true;
		} else {
			grounded = false;
		}*/
		
	}
	
	void FixedUpdate() {
		// Apply movement to rigidbody
		Vector3 localMove = transform.TransformDirection(moveAmount) * Time.fixedDeltaTime;
		rigidbody.MovePosition(rigidbody.position + localMove);
	}

	public void disableWeapons() {

		weaponControls.DisableAllWeapons();

	}

	public void DestroyShip() {
		isDead = true;

		//disableWeapons();

		gameController.SpawnPlayer(playerID);

		foreach (GameObject g in destroyOnDeath){
			Destroy(g);
		}

		StartCoroutine("CleanUp");
	}

	IEnumerator CleanUp() {
		yield return new WaitForSeconds(gameController.spawnDelay-0.5f);
		Destroy(gameObject);
	}
}



public enum PlayerID  {
	P1,
	P2,
	P3,
	P4
}