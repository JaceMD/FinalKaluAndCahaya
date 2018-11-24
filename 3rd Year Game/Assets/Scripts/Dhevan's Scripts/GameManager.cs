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
	public AudioSource source;
	public AudioClip pauseMenuSFX, backSFX;


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
				source.PlayOneShot (pauseMenuSFX, 0.5f);
				gamePaused = true;
				//show pause menu and pause game
				pausePanel.SetActive(true);
				onScreenPauseText.SetActive (false);
				Time.timeScale = 0f;
				this.gameObject.GetComponent<TutorialController> ().disableTutMenu ();
			} else {
				//Unpause game and hide pause menu.
				source.PlayOneShot (pauseMenuSFX, 0.5f);
				gamePaused = false;
				pausePanel.SetActive (false);
				onScreenPauseText.SetActive (true);
				Time.timeScale = 1;
				this.gameObject.GetComponent<TutorialController> ().enableTutMenu ();
			}
		}

		if (gamePaused == true && (Input.GetKeyDown (KeyCode.Escape) || controller.Action4.WasPressed)) {
			source.PlayOneShot (backSFX, 0.7f);
			Time.timeScale = 1;
			if (SceneManager.GetActiveScene ().name == "Final Demo Scene" || SceneManager.GetActiveScene ().name == "Controls Scene") {
				SceneManager.LoadScene ("Main Menu Scene");
			} else {
				//Application.Quit ();
			}
		}

	}



}
