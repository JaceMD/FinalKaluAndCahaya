using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using InControl;
using UnityEngine.UI;

public class TutorialController : MonoBehaviour {

	private InputDevice controller;

	[SerializeField]
	private int tutorialNumTracker = 1;

	public GameObject tutorialPanel;
	private bool tutPanelActive = true;

	public GameObject openTutBoxText;
	public TMP_Text tutDescription;
	public GameObject tutIconObj;
	public Image tutIcon;
	

	// Use this for initialization
	void Start () {
		controller = InputManager.ActiveDevice;
		tutorialPanel.SetActive (true);
		tutPanelActive = true;
		openTutBoxText.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		if (tutPanelActive == true) {
			if (controller.Action1.WasPressed == true) {
				tutorialPanel.SetActive (false);
				tutPanelActive = false;
				openTutBoxText.SetActive (true);
			}

			TutScenario ();
		} else if(tutPanelActive == false) {
			if (controller.Action1.WasPressed == true) {
				tutorialPanel.SetActive (true);
				tutPanelActive = true;
				openTutBoxText.SetActive (false);
			}
		}
		
	}

	public void changeTutNum(int num){
		tutorialNumTracker = num;
		tutPanelActive = true;
		tutorialPanel.SetActive (true);

	}

	void TutScenario(){
		switch (tutorialNumTracker) {
		//All lower case for descriptions please
		case 1:
			tutDescription.text = "use the left analogue stick to move Kalu.";


			break;
		case 2:
			tutDescription.text = "these big totems are checkpoints.\nif Kalu feints or falls off the map he'll respawn here.";
			tutIconObj.SetActive (false);
			break;
		case 3:

			break;
		case 4:

			break;
		case 5:

			break;
		case 6:

			break;
		case 7:

			break;
		case 8:

			break;
		case 9:

			break;
		case 10:

			break;
		default:
			break;

		}
	}

}
