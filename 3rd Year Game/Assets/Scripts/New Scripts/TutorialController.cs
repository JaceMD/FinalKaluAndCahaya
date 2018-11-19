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
	public AudioSource source;
	public AudioClip hintSFX, tutBoxSFX;
	

	// Use this for initialization
	void Start () {
		tutorialPanel.SetActive (true);
		tutPanelActive = true;
		openTutBoxText.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		controller = InputManager.ActiveDevice;
		if (tutPanelActive == true) {
			if (controller.Action1.WasPressed == true) {
				source.PlayOneShot (tutBoxSFX, 0.7f);
				tutorialPanel.SetActive (false);
				tutPanelActive = false;
				openTutBoxText.SetActive (true);
			}

			TutScenario ();
		} else if(tutPanelActive == false) {
			if (controller.Action1.WasPressed == true) {
				source.PlayOneShot (tutBoxSFX, 0.7f);
				tutorialPanel.SetActive (true);
				tutPanelActive = true;
				openTutBoxText.SetActive (false);
			}
		}
		
	}

	public void changeTutNum(int num){
		source.PlayOneShot (hintSFX, 0.4f);
		tutorialNumTracker = num;
		tutPanelActive = true;
		tutorialPanel.SetActive (true);
		openTutBoxText.SetActive (false);
	}

	void TutScenario(){
		switch (tutorialNumTracker) {
		//All lower case for descriptions please
		case 1:
			tutDescription.text = "Use the <color=\"purple\">left analogue stick <color=\"white\">to move Kalu." +
				"\n\nObjective: reach the end of the level.";


			break;
		case 2:
			tutDescription.text = "these <color=\"purple\">big totems <color=\"white\">are <color=\"purple\">checkpoints<color=\"white\">.\nif Kalu <color=\"red\">feints or falls off the map <color=\"white\">.he'll <color=\"red\">respawn<color=\"white\"> at the last visited totem.";
			tutIconObj.SetActive (false);
			break;
		case 3:
			tutDescription.text = "Dangerous creatures roam the forest.\n<color=\"red\">avoid detection<color=\"white\"> by staying out of their line of sight!";
			tutIconObj.SetActive (false);
			break;
		case 4:
			tutDescription.text = "Kalu is able to <color=\"purple\">crawl<color=\"white\"> underneath fallen tree trunks.\n<color=\"purple\">press the left analogue stick<color=\"white\"> to go prone.\npress it again to stand back up.";
			tutIconObj.SetActive (true);
			break;
		case 5:
			tutDescription.text = "The floating light creature is Cahaya. he projects light so that Kalu can see in the night." +
				"\n<color=\"purple\">press the right analogue stick<color=\"white\"> to enable Cahaya's <color=\"purple\">light projection mode<color=\"white\"> then use the right analogue stick to <color=\"purple\">rotate<color=\"white\"> Cahaya and <color=\"purple\">dynamically create shadows.";
			tutIconObj.SetActive (true);
			tutIcon.sprite = RightStick;
			break;
		case 6:
			tutDescription.text = "The <color=\"red\">enemies<color=\"white\"> have very <color=\"red\">poor eyesight<color=\"white\"> so stick to the shadows to remain undetected.\nThe <color=\"purple\">dissappearing eye icon<color=\"white\"> at the top of the screen indicates if Kalu can be seen in the light or not.";
			tutIconObj.SetActive (true);
			tutIcon.sprite = eye;
			break;
		case 7:
			tutDescription.text = "Kalu walks slower in shadows and faster in the light.\nif an enemy sees Kalu a <color=\"yellow\">yellow exclamation<color=\"white\"> will appear above their heads to indicate being <color=\"yellow\">alerted<color=\"white\">. if Kalu is seen for too long it will <color=\"red\">turn red<color=\"white\"> to indicate being <color=\"red\">spotted.";
			tutIconObj.SetActive (false);
			break;
		case 8:
			tutDescription.text = "Kalu will be <color=\"red\">spotted<color=\"white\"> if he gets too close to the enemy creatures even if he is in the shadows.";

			break;
		case 9:
			tutDescription.text = "Kalu can hide behind shorter foliage if he is <color=\"purple\">crawling.<color=\"white\">\nDon't forget you can crawl under some tree trunks.";
			break;
		case 10:
			tutDescription.text = "Nicely done! You've learnt all the basic controls. \ntry get past the final puzzle";
			break;
		case 11:
			tutDescription.text = "Hope you enjoyed this short demo. continue walking off into the forest to complete the level.";
			break;
		default:
			break;

		}
	}

}
