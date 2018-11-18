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
	public Sprite RightStick, eye; 
	

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
		openTutBoxText.SetActive (false);
	}

	void TutScenario(){
		switch (tutorialNumTracker) {
		//All lower case for descriptions please
		case 1:
			tutDescription.text = "Use the left analogue stick to move Kalu." +
				"\n\nObjective: reach the end of the level.";


			break;
		case 2:
			tutDescription.text = "these big totems are checkpoints.\nif Kalu feints or falls off the map he'll respawn at the last visited totem.";
			tutIconObj.SetActive (false);
			break;
		case 3:
			tutDescription.text = "Dangerous creatures roam around.\navoid detection by staying in the shadows or out of their line of sight !";
			tutIconObj.SetActive (false);
			break;
		case 4:
			tutDescription.text = "Kalu is able to crawl underneath fallen tree trunks.\npress the left analogue stick to go prone.\npress it again to stand back up.";
			tutIconObj.SetActive (true);
			break;
		case 5:
			tutDescription.text = "The floating light creature following Kalu is Cahaya. he projects light so that Kalu can see in the night." +
				"\npress the right analogue button to enable Cahaya's light projection mode then use the right stick to rotate Cahaya and dynamically create shadows.";
			tutIconObj.SetActive (true);
			tutIcon.sprite = RightStick;
			break;
		case 6:
			tutDescription.text = "The enemies have very poor eyesight so stick to the shadows.\nThe dissappearing white eye icon at the top of the screen tells you if Kalu can be seen in the light or not.";
			tutIconObj.SetActive (true);
			tutIcon.sprite = eye;
			break;
		case 7:
			tutDescription.text = "Kalu walks slower in shadows and faster in the light.\nif an enemy sees Kalu an alerted yellow exclamation will appear above their heads. It will turn red to indicate being spotted if Kalu is seen for too long.";
			tutIconObj.SetActive (false);
			break;
		case 8:
			tutDescription.text = "Don't let Kalu get too close to the enemy creatures even if he is in the shadows.";

			break;
		case 9:
			tutDescription.text = "Kalu can hide behind shorter foliage if he is crawling.";
			break;
		case 10:
			tutDescription.text = "Nicely done! You've learnt all the basic controls. Try get past the final puzzle";
			break;
		case 11:
			tutDescription.text = "Hope you enjoyed this short demo. continue walking off into the forest to complete the level.";
			break;
		default:
			break;

		}
	}

}
