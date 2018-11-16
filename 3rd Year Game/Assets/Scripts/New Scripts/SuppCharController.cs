using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class SuppCharController : MonoBehaviour {

	private InputDevice controller;
	public GameObject player;
	public Transform playerT;
	public float xPosMaxOffset = 2f, yPosMaxOffset = 2.5f, zPosMaxOffset = 2f;
	public float suppCharFollowSpeed = 1.8f;
	public bool followMode = true;

	public Transform overseePos;

	private Ray[] lightDetectionRays = new Ray[9];
	public GameObject[] pRCTargets = new GameObject[9];

	private bool controlsDisabled = false;



	// Use this for initialization
	void Start () {
		controller = InputManager.ActiveDevice;
		followMode = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (controlsDisabled == false) {
			if (controller.RightStickButton.WasPressed) {
				if (followMode == true) {
					followMode = false;
					GameObject.Find ("Oversee").GetComponent<OverseeController> ().ResetOverseePos ();
				} else if (followMode == false) {
					followMode = true;
				}

			}

			if (followMode == true) {
				checkFollowDistance ();
			} else {
				Vector3 newPosition = new Vector3 (overseePos.position.x, overseePos.position.y, overseePos.position.z);
				this.transform.position = Vector3.Slerp (transform.position, newPosition, 6 * Time.deltaTime);
			}
		}

	}

	void FixedUpdate(){
		RaycastHit hit;
		for (int loop = 0; loop < 9; loop++) {
			lightDetectionRays [loop] = new Ray (transform.position, pRCTargets [loop].transform.position - transform.position);
			Debug.DrawLine (transform.position,  pRCTargets [loop].transform.position, Color.red);

			if (Physics.Raycast (lightDetectionRays [loop], out hit, 40f) && ((hit.transform.gameObject.tag == "PRCTarget") || hit.transform.gameObject.tag == "Player")) {
				player.GetComponent<StealthManager> ().setPRCTargetVisible (loop, true);
				Debug.DrawLine (hit.point, hit.point + Vector3.up * 2f, Color.green);
				//Debug.Log ("Hit");
			} else {
				player.GetComponent<StealthManager> ().setPRCTargetVisible (loop, false);
			}
		}

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

	public void DisableControls(){
		controlsDisabled = true;
	}
	public void EnableControls(){
		controlsDisabled = false;
	}
}
