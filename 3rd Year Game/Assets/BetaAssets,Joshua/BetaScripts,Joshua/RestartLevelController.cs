using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartLevelController : MonoBehaviour {

    public GameObject Player;
	public GameObject SuppChar;
	public GameObject MainCamera;
    public Vector3 CheckPointLocation;
    public FadeEffectController ActivateFade;
    public bool Fallen = false;

	// Use this for initialization
	void Start () {
		CheckPointLocation = Player.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        if (Fallen == true)
        {
			if (ActivateFade.fadeInTimer > 0f )
            {
                Player.transform.position = CheckPointLocation;
				SuppChar.transform.position = CheckPointLocation;
				MainCamera.transform.position = new Vector3(CheckPointLocation.x, CheckPointLocation.y, MainCamera.transform.position.z);
                Fallen = false;
            }
        }
    }

	void OnTriggerEnter(Collider other){
		if (other.gameObject.tag == "Player") {
            ActivateFade.Activate = true;
            Fallen = true;
		}
	}
}
