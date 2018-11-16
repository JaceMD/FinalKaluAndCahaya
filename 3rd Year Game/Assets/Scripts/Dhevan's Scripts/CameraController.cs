using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class CameraController : MonoBehaviour
{

	private InputDevice controller;

	public Transform playerT;
	private Transform focalObjT;

	public float followTime = 0.15f;
	public float FollowSpeed = 4f;
	public float xPosOffset = 0f, yPosOffset = 3.5f, zPosoffset = -12f;
	public float xRotOffset = 15f, yRotOffset = 10f, zRotOffset = 0f;

	public float camRotateSpeed = 6f;



	// Use this for initialization
	void Start ()
	{
		focalObjT = playerT;
		controller = InputManager.ActiveDevice;

	}
	
	// Update is called once per frame
	void Update ()
	{
		//Update Camera Position
		Vector3 newPosition = new Vector3 (focalObjT.position.x + xPosOffset, focalObjT.position.y + yPosOffset, focalObjT.position.z + zPosoffset);
		this.transform.position = Vector3.Slerp (transform.position, newPosition, FollowSpeed * Time.deltaTime);

		//Update Camera Rotation
		Vector3 newRotation = new Vector3 (xRotOffset, yRotOffset, zRotOffset);
		Quaternion rot = Quaternion.Euler (newRotation);
		this.transform.rotation = Quaternion.Slerp(this.transform.rotation, rot, Time.deltaTime * camRotateSpeed);

	}

}

