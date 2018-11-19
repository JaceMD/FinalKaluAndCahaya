﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour {

	private InputDevice controller;

	void Start(){
		
		Cursor.visible = false;
	}

	// Update is called once per frame
	void Update () {
		controller = InputManager.ActiveDevice;

		if ((controller.Action4.WasPressed == true || Input.GetKeyDown(KeyCode.Escape))){
			Scene thisScene = SceneManager.GetActiveScene ();
			if (thisScene.name == "CreditsScene") {
				Debug.Log ("Triangle pressed");
				onClickMainMenu ();
			}
		}
	}

	public void onClickMainMenu(){
		SceneManager.LoadScene ("Main Menu Scene");
	}

	public void onClickDemoScene(){
		SceneManager.LoadScene ("Final Demo Scene");
	}

	public void onClickCreditsScene(){
		SceneManager.LoadScene ("CreditsScene");
	}

	public void onClickQuitGame(){
		Application.Quit ();
	}
	

	public void RestartLevel(){
		Scene thisScene = SceneManager.GetActiveScene ();
		SceneManager.LoadScene (thisScene.name);
	}
}
