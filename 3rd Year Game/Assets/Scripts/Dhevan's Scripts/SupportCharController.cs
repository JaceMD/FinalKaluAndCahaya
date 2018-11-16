using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class SupportCharController : MonoBehaviour {

	private InputDevice controller;
	public GameObject player;
	public Transform playerT;
	public float xPosMaxOffset, yPosMaxOffset, zPosMaxOffset;
	public float suppCharFollowSpeed = 4f;
	private  bool followMode = true;


	private float xInput, yInput, zInput;
	private bool controlsInverted = false;
	private bool controlsDisabled = false;
	public Transform moonObj;


	public float moveSpeed = 10f;

	private Ray[] lightDetectionRays = new Ray[9];
	public GameObject[] pRCTargets = new GameObject[9];

	private bool buttonPressDelay = false;
	private float startButtonPressDelay; //Since Incontrol doesnt have a getDown method, this is used to check for single button presses.

	// Use this for initialization
	void Start () {
		controller = InputManager.ActiveDevice;
		followMode = true;
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time >= startButtonPressDelay + 0.5f && buttonPressDelay == true) { //User has to wait 0.5 seconds to repress a button
			buttonPressDelay = false;
		}
		/*
		if (controller.RightStickButton && buttonPressDelay == false) {
			buttonPressDelay = true;
			startButtonPressDelay = Time.time;
			followMode = false;
		} 
		*/
		if (controller.LeftTrigger) {
			followMode = false;
		} else {
			followMode = true;
		}

		if (followMode == false) {
			Vector3 newPosition = new Vector3 (moonObj.position.x, moonObj.position.y, moonObj.position.z);
			this.transform.position = Vector3.Slerp (transform.position, newPosition, 6 * Time.deltaTime);
			if (controlsDisabled == false) {
				checkMovement ();
			}
			if (controller.Action4) {
				followMode = true;
			}
		} else {
			checkFollowDistance ();
		}

	}

	void FixedUpdate(){
			RaycastHit hit;
			for (int loop = 0; loop < 9; loop++) {
				lightDetectionRays [loop] = new Ray (transform.position, pRCTargets [loop].transform.position - transform.position);
				Debug.DrawLine (transform.position,  pRCTargets [loop].transform.position, Color.red);

				if (Physics.Raycast (lightDetectionRays [loop], out hit, 8f) && ((hit.transform.gameObject.tag == "PRCTarget") || hit.transform.gameObject.tag == "Player")) {
					player.GetComponent<DetectionController> ().setPRCTargetVisible (loop, true);
					Debug.DrawLine (hit.point, hit.point + Vector3.up * 2f, Color.green);
					//Debug.Log ("Hit");
				} else {
					player.GetComponent<DetectionController> ().setPRCTargetVisible (loop, false);
				}
			}

	}

	void DisableControls(){
		controlsDisabled = true;
		Debug.Log ("Supp controls disabled");
	}
	void EnableControls(){
		controlsDisabled = false;
		Debug.Log ("Supp controls enabled");
	}

	void checkMovement(){
		xInput = controller.LeftStick.X;  
		zInput = controller.LeftStick.Y;

		//Invert Controsl for when camera flips
		if (controlsInverted) {
			xInput *= -1f;
			zInput *= -1f;
		}
		Vector3 Movement = Vector3.zero;
		Movement = new Vector3 (xInput, 0f, zInput) * Time.deltaTime * moveSpeed;
		
		this.transform.Translate(Movement, Space.World); 
	}

	void checkFollowDistance(){

		if (Mathf.Abs (this.transform.position.x - playerT.position.x) > xPosMaxOffset) {
			Vector3 newPosition = new Vector3 (playerT.position.x, transform.position.y, transform.position.z);
			this.transform.position = Vector3.Slerp (transform.position, newPosition, suppCharFollowSpeed * Time.deltaTime);
		}

		if (Mathf.Abs (this.transform.position.y - playerT.position.y) != yPosMaxOffset) {
			Vector3 newPosition = new Vector3 (transform.position.x, playerT.position.y + yPosMaxOffset, transform.position.z);
			this.transform.position = Vector3.Slerp (transform.position, newPosition, suppCharFollowSpeed * Time.deltaTime);
		}

		if (Mathf.Abs (this.transform.position.z - playerT.position.z) > zPosMaxOffset) {
			Vector3 newPosition = new Vector3 (transform.position.x, transform.position.y, playerT.position.z);
			this.transform.position = Vector3.Slerp (transform.position, newPosition, suppCharFollowSpeed * Time.deltaTime);
		}
	}


}
