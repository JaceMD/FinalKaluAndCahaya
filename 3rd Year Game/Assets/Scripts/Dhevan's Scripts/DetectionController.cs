using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DetectionController : MonoBehaviour {

	public bool[] pRCTargetsVisible = new bool[9];
	public Image detectionUIImage;


	private float alphaPercentage;

	// Use this for initialization
	void Start () {
		for (int i = 0; i < 9; i++) {
			pRCTargetsVisible [i] = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
		updateAlphaUI ();
	}

	public void setPRCTargetVisible(int elem, bool visibility){
		pRCTargetsVisible [elem] = visibility;
	}

	void updateAlphaUI(){
		float alpha = checkVisibilityAlphaRatio ();
		alphaPercentage = detectionUIImage.color.a;

		//Fade Alpha over time smoothly
		if (alphaPercentage > alpha) {
			alphaPercentage -= Time.deltaTime;
		} else if (alphaPercentage < alpha) {
			alphaPercentage += Time.deltaTime;
		} else {
			alphaPercentage = alpha;
		}

		detectionUIImage.color = new Color(detectionUIImage.color.r, detectionUIImage.color.g, detectionUIImage.color.b, alphaPercentage);


		//check distance alpha
	}
	float checkVisibilityAlphaRatio(){
		int numPRCTargetsVisible = 0;

		for (int i = 0; i < 9; i++) {
			if (pRCTargetsVisible [i] == true) {
				numPRCTargetsVisible++;
			}
		}
		alphaPercentage = numPRCTargetsVisible / 9f;

		return alphaPercentage;

	}


	public bool isPlayerInLight(){
		if (alphaPercentage > 0.5f) {
			return true;
		} else {
			return false;
		}
	}
	public void playerDetected(){
		detectionUIImage.color = new Color(1f, 0f, 0f, 1f);
	}
	public void playerUndetected(){
		detectionUIImage.color = new Color(1f, 1f, 1f, alphaPercentage);
	}






}
