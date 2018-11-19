using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class OverseeController : MonoBehaviour
{

	private InputDevice controller;
	public float overseeRotationSpeed = 5f;

	public GameObject overseeObj;
	private Transform overseeParentT;
	public GameObject player;

	private Ray[] lightDetectionRays = new Ray[9];
	public GameObject[] pRCTargets = new GameObject[9];

	public GameObject overseeDirLight;
	public GameObject suppPtLight;
	private float lightValue1 = 0f, lightValue2 = 20f;

	public SuppCharController suppCharScript;

	// Use this for initialization
	void Start ()
	{
		
		overseeParentT = this.transform;
		overseeParentT.position = player.transform.position;

	}
	
	// Update is called once per frame
	void Update ()
	{
		controller = InputManager.ActiveDevice;
		checkLightMode ();
		updateOverseePos ();
	}

	void updateOverseePos(){
		Vector3 newPos = new Vector3 (player.transform.position.x, player.transform.position.y + 4.5f, player.transform.position.z);
		this.transform.position = newPos;
		//Vector3.Slerp (transform.position, newPos, 2f * Time.deltaTime)
	}

	void OverseeRotationControls ()
	{
		float xInput = controller.RightStick.X;

		if (xInput > 0f) {
			overseeParentT.eulerAngles = new Vector3 (0f, overseeParentT.eulerAngles.y + overseeRotationSpeed, 0f);
		} else if (xInput < 0f) {
			overseeParentT.eulerAngles = new Vector3 (0f, overseeParentT.eulerAngles.y - overseeRotationSpeed, 0f);
		}

	}

	void checkLightMode(){
		if (suppCharScript.followMode == false) {
			OverseeRotationControls();

			lightValue1 += Time.deltaTime * 70f;
			if(lightValue1>100f){
				lightValue1 = 100f;
			}
			overseeDirLight.GetComponent<Light> ().range = lightValue1;

			lightValue2 -= Time.deltaTime * 20f;
			if(lightValue2<0f){
				lightValue2 = 0f;
			}
			suppPtLight.GetComponent<Light> ().range = lightValue2;

		} else {
			lightValue1 -= Time.deltaTime * 70f;
			if(lightValue1<=0f){
				lightValue1 = 0f;
			}
			overseeDirLight.GetComponent<Light> ().range = lightValue1;

			lightValue2 += Time.deltaTime * 20f;
			if(lightValue2>20f){
				lightValue2 = 20f;
			}
			suppPtLight.GetComponent<Light> ().range = lightValue2;

			//Reset oversee parent rotation
			//Reset after time not instantly
		}
	}

	public void ResetOverseePos(){
		overseeParentT.eulerAngles = new Vector3 (0f, 180f, 0f);
	}
}
