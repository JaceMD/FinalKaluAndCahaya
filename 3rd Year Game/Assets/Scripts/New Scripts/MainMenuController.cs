﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using InControl;

public class MainMenuController : MonoBehaviour {

	private int buttonCounter = 1;
	//1 = Play
	//2 = Credits
	//3 = Quit

	public GameObject playMenuUI, creditsMenuUI, quitMenuUI;
	private InputDevice controller;
	private AudioSource source;
	public AudioClip scrollSFX, selectSFX;

	private bool selectedOption = false;
	private float startSelectionTime;

	// Use this for initialization
	void Start () {
		buttonCounter = 1;
		source = this.gameObject.GetComponent<AudioSource> ();
		Cursor.visible = false;
		selectedOption = false;
	}
	
	// Update is called once per frame
	void Update () {
		controller = InputManager.ActiveDevice;

		if (Input.GetKeyDown (KeyCode.Escape)) {
			Application.Quit ();
		}

		if (selectedOption == true && Time.time >= startSelectionTime + 0.3f) {
			switch(buttonCounter){
			case 1:
				this.gameObject.GetComponent<SceneController> ().onClickDemoScene ();
				break;
			case 2:
				this.gameObject.GetComponent<SceneController> ().onClickCreditsScene ();
				break;
			case 3:
				this.gameObject.GetComponent<SceneController> ().onClickQuitGame ();
				break;
			default:
				break;

			}
		}else {
			if (controller.DPadUp.WasPressed) {
				buttonCounter--;
				source.PlayOneShot (scrollSFX, 0.7f);
				if (buttonCounter < 1) {
					buttonCounter = 1;
				}
			} else if(controller.DPadDown.WasPressed){
				buttonCounter++;
				source.PlayOneShot (scrollSFX, 0.7f);
				if (buttonCounter > 3) {
					buttonCounter = 3;
				}

			}
		}

		switch (buttonCounter) {
		case 1:
			playMenuUI.GetComponent<Animator> ().enabled = true;
			playMenuUI.GetComponent<Image> ().color = Color.red;

			creditsMenuUI.GetComponent<Animator> ().enabled = false;
			creditsMenuUI.GetComponent<Image> ().color = Color.white;
			quitMenuUI.GetComponent<Animator> ().enabled = false;
			quitMenuUI.GetComponent<Image> ().color = Color.white;
			break;
		case 2:
			creditsMenuUI.GetComponent<Animator> ().enabled = true;
			creditsMenuUI.GetComponent<Image> ().color = Color.red;

			playMenuUI.GetComponent<Animator> ().enabled = false;
			playMenuUI.GetComponent<Image> ().color = Color.white;
			quitMenuUI.GetComponent<Animator> ().enabled = false;
			quitMenuUI.GetComponent<Image> ().color = Color.white;
			break;
		case 3:
			quitMenuUI.GetComponent<Animator> ().enabled = true;
			quitMenuUI.GetComponent<Image> ().color = Color.red;

			playMenuUI.GetComponent<Animator> ().enabled = false;
			playMenuUI.GetComponent<Image> ().color = Color.white;
			creditsMenuUI.GetComponent<Animator> ().enabled = false;
			creditsMenuUI.GetComponent<Image> ().color = Color.white;
			break;
		default:
			break;
		}

		if (controller.Action1.WasPressed) {
			source.PlayOneShot (selectSFX, 1f);
			selectedOption = true;
			startSelectionTime = Time.time;
		}

	}
}
