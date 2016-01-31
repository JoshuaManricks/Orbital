using UnityEngine;
using System.Collections;
using InputPlusControl;

[RequireComponent (typeof (GravityBody))]
public class FirstPersonController : MonoBehaviour {

	//////////////////////////////////////////
	GameController gameController;
	WeaponControls weaponControls;
	//////////////////////////////////////////

	// public vars
	public PlayerID playerID;
	[HideInInspector]
	public int controllerID;

	public ShipType shipType;
	public float movementSpeed = 6;
	public float rotateSpeed = 2;
	[HideInInspector]
	public float boostModifier = 0f;

	public bool dummy = false;
//	public bool autoMove = false;
	public float inputY;

	[HideInInspector]
	public bool isDead = false;
	public GameObject[] destroyOnDeath;


	//bool grounded;
	Vector3 moveAmount;
	Vector3 smoothMoveVelocity;
	float verticalLookRotation;
	Transform cameraTransform;
	Rigidbody rigidbody;


	bool initCompleted = false;

	//public float jumpForce = 220;
	//	public LayerMask groundedMask;
	void Start() {
	
	}
	
	void Awake() {
		rigidbody = GetComponent<Rigidbody> ();

//		if (dummy) {
//			GetComponentInChildren<Camera>().enabled =false;
//		}

		weaponControls = GetComponentInChildren<WeaponControls>();
		gameController = FindObjectOfType<GameController>();

	}

	public void init() {
		Debug.Log("PLAYER CREATED : " + playerID + " : "+ shipType);

		SetupController ();

		initCompleted = true;
	}

	void Update() {

		if (!initCompleted) return;

		if (isDead || dummy) return;

		if (shipType == ShipType.Plane) {
			MovePlane ();

		} else if (shipType == ShipType.Tank) {
			MoveTank ();

		} else if (shipType == ShipType.Strafe) {
			MoveStrafe ();

		}


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

	void MovePlane() {

//		transform.Rotate (Vector3.up * Input.GetAxisRaw ("Horizontal_" + playerID) * mouseSensitivityX);
		transform.Rotate (Vector3.up * InputPlus.GetData (controllerID, ControllerVarEnum.ThumbLeft_x) * rotateSpeed);

//		inputY = InputPlus.GetData (controllerID, ControllerVarEnum.ShoulderBottom_right);//Input.GetAxisRaw ("Vertical_" + playerID) * -1f;
		inputY = Mathf.Clamp (InputPlus.GetData (controllerID, ControllerVarEnum.ShoulderBottom_right), 0, 1);

		Vector3 moveDir = new Vector3 (0, 0, inputY);//.normalized;
		Vector3 targetMoveAmount = moveDir * (movementSpeed + boostModifier);
		moveAmount = Vector3.SmoothDamp (moveAmount, targetMoveAmount, ref smoothMoveVelocity, .15f);
	}

	void MoveTank() {
		transform.Rotate (Vector3.up * InputPlus.GetData (controllerID, ControllerVarEnum.ThumbLeft_x) * rotateSpeed);

		Vector3 moveDir = new Vector3 (0, 0, InputPlus.GetData (controllerID, ControllerVarEnum.ThumbLeft_y)*-1);//.normalized;
		Vector3 targetMoveAmount = moveDir * (movementSpeed + boostModifier);
		moveAmount = Vector3.SmoothDamp (moveAmount, targetMoveAmount, ref smoothMoveVelocity, .15f);
	}

	void MoveStrafe() {
		//rotate ship
		transform.Rotate (Vector3.up * InputPlus.GetData (controllerID, ControllerVarEnum.ThumbLeft_x) * rotateSpeed);

		//move direction
		Vector3 moveDir = new Vector3 (	InputPlus.GetData (controllerID, ControllerVarEnum.ThumbRight_x),
										0,
										InputPlus.GetData (controllerID, ControllerVarEnum.ThumbLeft_y) * -1);//.normalized;
		Vector3 targetMoveAmount = moveDir * (movementSpeed + boostModifier);
		moveAmount = Vector3.SmoothDamp (moveAmount, targetMoveAmount, ref smoothMoveVelocity, .15f);
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

		gameController.ReSpawnPlayer(playerID);

		foreach (GameObject g in destroyOnDeath){
			Destroy(g);
		}

		StartCoroutine("CleanUp");
	}

	IEnumerator CleanUp() {
		yield return new WaitForSeconds(gameController.spawnDelay-0.5f);
		Destroy(gameObject);
	}

	void SetupController() {
		switch (playerID) {
		case PlayerID.P1:
				controllerID = 1;
			return;
			case PlayerID.P2:
				controllerID = 2;
			return;
			case PlayerID.P3:
				controllerID = 3;
			return;
			case PlayerID.P4:
				controllerID = 4;
			return;
		}
	}
}

public enum PlayerID  {
	P1,
	P2,
	P3,
	P4,
	none
}