using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using InControl;
using UnityEngine.UI;

public class TutorialController : MonoBehaviour {

	private InputDevice controller;

	[SerializeField]
	private int tutorialNumTracker = 0;

	public GameObject tutorialPanel;
	private bool tutPanelActive = true;

	public TMP_Text tutDescription;
	public Image tutIcon;
	

	// Use this for initialization
	void Start () {
		controller = InputManager.ActiveDevice;
		tutorialPanel.SetActive (true);
		tutPanelActive = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (tutPanelActive == true) {
			if (controller.Action1.WasPressed == true) {
				tutorialPanel.SetActive (false);
			}
		}
		
	}
}
