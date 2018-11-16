using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class CheckGroundController : MonoBehaviour {

	public MainCharacterController control;

	void OnTriggerEnter(Collider other){
		if (other.gameObject.tag == "Ground") {
			control.SetGrounded (true);
		}
	}

	void OnTriggerExit(Collider other){
		if (other.gameObject.tag == "Ground") {	
			control.SetGrounded (false);
		}
	}
}
