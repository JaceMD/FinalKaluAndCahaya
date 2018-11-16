using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	private InputDevice controller;
	public GameObject pausePanel;

	private bool gamePaused = false;

	void Start() 
	{
		gamePaused = false;
		controller = InputManager.ActiveDevice;
		pausePanel.SetActive (false);
	}

	void Update()
	{
		if (controller.Action2.WasPressed) {
			if (gamePaused == false) {
				gamePaused = true;
				//show pause menu and pause game
				pausePanel.SetActive(true);
				Time.timeScale = 0f;

				if (Input.GetKeyDown (KeyCode.Escape) || controller.Action4.WasPressed) {
					if (SceneManager.GetActiveScene ().name == "Final Demo Scene" || SceneManager.GetActiveScene ().name == "Controls Scene") {
						SceneManager.LoadScene ("Main Menu Scene");
					} else {
						Application.Quit ();
					}
				}

			} else {
				//Unpause game and hide pause menu.
				gamePaused = false;
				pausePanel.SetActive (false);
				Time.timeScale = 1;
			}
		}


	}

	void pauseGame(){

	}

	void unpauseGame(){

	}
}
