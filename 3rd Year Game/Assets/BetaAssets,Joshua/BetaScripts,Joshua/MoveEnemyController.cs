using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEnemyController : MonoBehaviour {

    public bool NightTriggered = false;
    public Transform MoveLocation;
    public float MoveSpeed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(NightTriggered == true)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, MoveLocation.position, MoveSpeed * Time.deltaTime);
        }
	}
}
