using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour {

    public RestartLevelController LevelController;
	public GameObject Fireflies;
	private GameObject player;
	private float playerDist;
	private bool checkPointEnabled = false;
	private bool visited = false;

	public int tutorialNum;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player");
		Fireflies.SetActive (false);
	}

    private void OnTriggerEnter(Collider other)
    {
		if (other.gameObject == LevelController.Player) {


			LevelController.CheckPointLocation = this.transform.position;
			Fireflies.SetActive (true);
			checkPointEnabled = true;
			if (visited == false) {
				GameObject.Find ("Game Manager").GetComponent<TutorialController> ().changeTutNum (tutorialNum);
				visited = true;
			}
		}
    }

    // Update is called once per frame
    void Update () {

		playerDist = (Vector3.Magnitude (this.gameObject.transform.position - player.transform.position));

			if (playerDist <= 12f && checkPointEnabled == true) {
			Fireflies.SetActive (true);
		} else {
			Fireflies.SetActive (false);
		}
	}


}
