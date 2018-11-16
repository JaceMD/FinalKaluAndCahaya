using System.Collections.Generic;
using UnityEngine;
using InControl;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

	private InputDevice controller;

	public float moveSpeed = 10f;
	public float crouchSpeed = 5f;
	private float actualSpeed;

	private bool dashing = false;
	private float startDashTime;
	public float dashSpeedFactor = 30f;
	public float DashTime = 0.1f;
	public float delayDashTime = 1f;

	private bool crouching = false;
	private float startCrouchSquishTime;
	private float squishDuration = 0.25f;

	private float xInput, yInput, zInput;
	private bool controlsInverted = false;
	private bool controlsDisabled = false;

	public float jumpForce = 3.5f;
	public float fallMultiplier = 2.5f;
	private bool jumpRequest = false;
	private bool canJump = false;


	private bool buttonPressDelay = false;
	private float startButtonPressDelay;
	//Since Incontrol doesnt have a getDown method, this is used to check for single button presses.

	private Rigidbody playerRB;

	// Use this for initialization
	void Start ()
	{
		controller = InputManager.ActiveDevice;
		playerRB = this.GetComponent<Rigidbody> ();
		delayDashTime = 3f;
		actualSpeed = moveSpeed;
	}

	// Update is called once per frame
	void Update ()
	{
		if (controlsDisabled == false) {
			checkMovement ();
			checkActionButtons ();
		}
		checkTriggerButtons ();
		checkStickButtons ();
		playerRB.rotation = Quaternion.identity; //prevent player object from rotating

		if (Time.time >= startDashTime + DashTime && dashing == true) {
			dashing = false;
			playerRB.velocity = Vector3.zero;
		}
		delayDashTime -= Time.deltaTime;

		if (Time.time >= startButtonPressDelay + 0.5f && buttonPressDelay == true) { //User has to wait 0.5 seconds to repress a button
			buttonPressDelay = false;
		}

		if (crouching == true) {
			RescaleSize (new Vector3 (2.1f, 0.75f, 2.1f));
		} else {
			RescaleSize (new Vector3 (1.5f, 1.5f, 1.5f));
		}
			

	}

	void FixedUpdate(){
		if (jumpRequest == true) {
			playerRB.AddForce (Vector3.up * jumpForce, ForceMode.Impulse);
			jumpRequest = false;
		}

		if (playerRB.velocity.y < 0f) {
			playerRB.useGravity = false;
			Vector3 gravityForce = Vector3.up * -9.81f * (fallMultiplier);
			playerRB.AddForce (gravityForce, ForceMode.Acceleration);

		} else if (playerRB.velocity.y > 0f) {
			playerRB.useGravity = true;
		} else if (playerRB.velocity.y == 0f) {
			playerRB.useGravity = true;
		}
	}

	void checkMovement ()
	{
		xInput = controller.LeftStick.X;  
		zInput = controller.LeftStick.Y;

		//Invert Controsl for when camera flips
		if (controlsInverted) {
			xInput *= -1f;
			zInput *= -1f;
		}
		Vector3 Movement = Vector3.zero;
		if (crouching == false) {
			Movement = new Vector3 (xInput, 0f, zInput) * Time.deltaTime * actualSpeed;
		} else if (crouching == true) {
			Movement = new Vector3 (xInput, 0f, zInput) * Time.deltaTime * crouchSpeed;
		}
		this.transform.Translate (Movement, Space.World); 
	}

	void checkActionButtons ()
	{
		//Check Jump Action
		if ((controller.Action1 || Input.GetKey (KeyCode.Space)) && canJump == true) {
			jumpRequest = true;
			canJump = false;
		} 

			
	}

	void checkTriggerButtons ()
	{
		if (controller.RightTrigger == true && dashing == false && buttonPressDelay == false  && delayDashTime <= 0f) {
			playerRB.velocity = new Vector3 (xInput, 0f, zInput) * dashSpeedFactor;
			dashing = true;
			startDashTime = Time.time;
			Debug.Log ("Dashed");
			buttonPressDelay = true;
			delayDashTime = 1f;
		}
	}

	void checkStickButtons ()
	{
		if (controller.LeftStickButton == true && buttonPressDelay == false && controlsDisabled == false) {
			if (crouching == false) {
				startCrouchSquishTime = Time.time;
				crouching = true;
			} else if (crouching == true) {
				crouching = false;
				startCrouchSquishTime = Time.time;
			}
			buttonPressDelay = true;
			startButtonPressDelay = Time.time;
		} 


	}

	void InvertControls ()
	{
		if (controlsInverted == true) {
			controlsInverted = false;
		} else {
			controlsInverted = true;
		}
	}

	void DisableControls ()
	{
		controlsDisabled = true;
		Debug.Log ("Player controls disabled");
	}

	void EnableControls ()
	{
		controlsDisabled = false;
		Debug.Log ("Player controls enabled");
	}

	void RescaleSize (Vector3 newScale)
	{

		float t = (Time.time - startCrouchSquishTime) / squishDuration;

		this.transform.localScale = Vector3.Lerp (this.transform.localScale, newScale, t);
	}

	void OnCollisionEnter (Collision other)
	{
		if (other.gameObject.tag == "Platform") {
			canJump = true;
			playerRB.velocity = Vector3.zero;
			actualSpeed = moveSpeed;
		} else if (other.gameObject.tag == "Enemy") {
			GameObject.Find ("SceneController").GetComponent<SceneController> ().RestartLevel ();
		}
	}

	void OnCollisionExit(Collision other){
		if (other.gameObject.tag == "Platform") {
			canJump = false;
		} 
	}

	void OnCollisionStay(Collision other){
		if (other.gameObject.tag == "Platform") {
			actualSpeed = moveSpeed;
		} 
	}
	
}