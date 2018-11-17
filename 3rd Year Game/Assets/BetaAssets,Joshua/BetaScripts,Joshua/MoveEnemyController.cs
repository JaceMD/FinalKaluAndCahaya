using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEnemyController : MonoBehaviour {

    public bool NightTriggered = false;
    public Transform MoveLocation;
    public float MoveSpeed;

	public Animator animChar;

	// Use this for initialization
	void Start () {
		//Idle animation
		animChar.SetInteger ("State", 0);
	}
	
	// Update is called once per frame
	void Update () {
		if(NightTriggered == true)
        {
			animChar.SetInteger ("State", 2);
            this.transform.position = Vector3.MoveTowards(this.transform.position, MoveLocation.position, MoveSpeed * Time.deltaTime);
        }
	}
}
