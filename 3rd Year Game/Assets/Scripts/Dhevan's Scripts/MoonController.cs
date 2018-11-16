using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class MoonController : MonoBehaviour {

	private InputDevice controller;

	private bool moonEnabled = false;
	public Transform moonTransform;
	public float moonRotationSpeed = 5f;

	private bool controlsInverted = false;
	public GameObject moonObj;
	public GameObject player;

	private Ray[] lightDetectionRays = new Ray[9];
	public GameObject[] pRCTargets = new GameObject[9];

	public GameObject moonDirLight;
	public GameObject suppPtLight;
	private float lightValue1 = 0f, lightValue2 = 20f;



	// Use this for initialization
	void Start () {
		controller = InputManager.ActiveDevice;
	}
	
	// Update is called once per frame
	void Update () {
		if (controller.LeftTrigger) {
			//moonRotationEnabled = true;
			MoonRotationControls();

			lightValue1 += Time.deltaTime * 70f;
			if(lightValue1>100f){
				lightValue1 = 100f;
			}
			moonDirLight.GetComponent<Light> ().range = lightValue1;

			lightValue2 -= Time.deltaTime * 20f;
			if(lightValue2<0f){
				lightValue2 = 0f;
			}
			suppPtLight.GetComponent<Light> ().range = lightValue2;

		} else {
			//moonRotationEnabled = false;
			lightValue1 -= Time.deltaTime * 70f;
			if(lightValue1<=0f){
				lightValue1 = 0f;
			}
			moonDirLight.GetComponent<Light> ().range = lightValue1;

			lightValue2 += Time.deltaTime * 20f;
			if(lightValue2>20f){
				lightValue2 = 20f;
			}
			suppPtLight.GetComponent<Light> ().range = lightValue2;
		}
			
		Vector3 newPos = new Vector3 (player.transform.position.x, 4.5f, player.transform.position.z);
		this.transform.position = Vector3.Slerp (transform.position, newPos, 2f * Time.deltaTime);
	}

	void FixedUpdate(){
		if (controller.LeftTrigger) {
			RaycastHit hit;
			for (int loop = 0; loop < 9; loop++) {
				lightDetectionRays [loop] = new Ray (moonObj.transform.position, pRCTargets [loop].transform.position - moonObj.transform.position);
				Debug.DrawLine (moonObj.transform.position, pRCTargets [loop].transform.position, Color.red);

				if (Physics.Raycast (lightDetectionRays [loop], out hit, 20f) && ((hit.transform.gameObject.tag == "PRCTarget") || hit.transform.gameObject.tag == "Player")) {
					player.GetComponent<DetectionController> ().setPRCTargetVisible (loop, true);
					Debug.DrawLine (hit.point, hit.point + Vector3.up * 2f, Color.green);
					//Debug.Log ("Hit");
				} else {
					player.GetComponent<DetectionController> ().setPRCTargetVisible (loop, false);
				}
			}
		}

	}

	void MoonRotationControls(){
		float xInput = controller.RightStick.X;
		if (controlsInverted == false) {
			if (xInput > 0f) {
				moonTransform.eulerAngles = new Vector3 (0f, moonTransform.eulerAngles.y + moonRotationSpeed, 0f);
			} else if (xInput < 0f) {
				moonTransform.eulerAngles = new Vector3 (0f, moonTransform.eulerAngles.y - moonRotationSpeed, 0f);
			}
		} else {
			if (xInput > 0f) {
				moonTransform.eulerAngles = new Vector3 (0f, moonTransform.eulerAngles.y - moonRotationSpeed, 0f);
			} else if (xInput < 0f) {
				moonTransform.eulerAngles = new Vector3 (0f, moonTransform.eulerAngles.y + moonRotationSpeed, 0f);
			}
		}
	}

	void InvertControls (){
		if (controlsInverted == true) {
			controlsInverted = false;
		} else {
			controlsInverted = true;
		}
	}

	public void enableMoonlight(){
		moonEnabled = true;
	}
	public void disableMoonlight(){
		moonEnabled = false;
	}
}
