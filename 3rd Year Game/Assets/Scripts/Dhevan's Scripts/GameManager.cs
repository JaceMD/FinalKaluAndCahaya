using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour {

	private InputDevice controller;
	public GameObject pausePanel;

	private bool gamePaused = false;
	public GameObject onScreenPauseText;

	void Start() 
	{
		gamePaused = false;

		pausePanel.SetActive (false);
		onScreenPauseText.SetActive (true);
		Cursor.visible = false;
	}

	void Update()
	{
		controller = InputManager.ActiveDevice;
		if (controller.Action2.WasPressed) {
			if (gamePaused == false) {
				gamePaused = true;
				//show pause menu and pause game
				pausePanel.SetActive(true);
				onScreenPauseText.SetActive (false);
				Time.timeScale = 0f;

			} else {
				//Unpause game and hide pause menu.
				gamePaused = false;
				pausePanel.SetActive (false);
				onScreenPauseText.SetActive (true);
				Time.timeScale = 1;
			}
		}

		if (gamePaused == true && (Input.GetKeyDown (KeyCode.Escape) || controller.Action4.WasPressed)) {
			Time.timeScale = 1;
			if (SceneManager.GetActiveScene ().name == "Final Demo Scene" || SceneManager.GetActiveScene ().name == "Controls Scene") {
				SceneManager.LoadScene ("Main Menu Scene");
			} else {
				//Application.Quit ();
			}
		}




	}


}
