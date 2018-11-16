using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour {

    public RestartLevelController LevelController;
	public GameObject Fireflies;
	private GameObject player;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player");
		Fireflies.SetActive (false);
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == LevelController.Player)
        LevelController.CheckPointLocation = this.transform.position;
    }

    // Update is called once per frame
    void Update () {
		if (Vector3.Magnitude (this.gameObject.transform.position - player.transform.position) <= 12f) {
			Fireflies.SetActive (true);
		} else {
			Fireflies.SetActive (false);
		}
	}
}
