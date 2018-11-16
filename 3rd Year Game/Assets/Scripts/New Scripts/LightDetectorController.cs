using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]

public class LightDetectorController : MonoBehaviour {

	public GameObject player;

	private Ray[] lightDetectionRays = new Ray[9];
	public GameObject[] pRCTargets = new GameObject[9];

	private bool drawRayCasts = false;

	// Use this for initialization
	void Start () {
		drawRayCasts = false;
		//player = GameObject.Find ("Player");

	}

	// Update is called once per frame
	void Update () {

	}
	void FixedUpdate(){
		if (drawRayCasts == true) {
			RaycastHit hit;
			for (int loop = 0; loop < 9; loop++) {
				lightDetectionRays [loop] = new Ray (transform.position, pRCTargets [loop].transform.position - transform.position);
				Debug.DrawLine (transform.position,  pRCTargets [loop].transform.position, Color.red);

				if (Physics.Raycast (lightDetectionRays [loop], out hit, 8f) && ((hit.transform.gameObject.tag == "PRCTarget") || hit.transform.gameObject.tag == "Player")) {
					player.GetComponent<StealthManager> ().setPRCTargetVisible (loop, true);
					Debug.DrawLine (hit.point, hit.point + Vector3.up * 2f, Color.green);
					//Debug.Log ("Hit");
				} else {
					player.GetComponent<StealthManager> ().setPRCTargetVisible (loop, false);
				}
			}
		}
	}

	void OnTriggerEnter(Collider other){
		if (other.gameObject.tag == "Player") {
			drawRayCasts = true;
		}
	}

	void OnTriggerExit(Collider other){
		if (other.gameObject.tag == "Player") {
			drawRayCasts = false;
		}
	}
}

