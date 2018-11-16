using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bobbing : MonoBehaviour {
	
	public Transform suppParent;
	public float bobDistance = 0.5f;
	public float bobDuration = 2f;
	private float startTime;

	// Use this for initialization
	void Start () {
		startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		float t = (Time.time - startTime) / bobDuration;
		this.transform.position = new Vector3 (this.transform.position.x, suppParent.position.y +(Mathf.Sin (t)) * bobDistance, this.transform.position.z);
	}
}
