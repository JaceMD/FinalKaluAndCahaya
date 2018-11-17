using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StealthManager : MonoBehaviour
{
	[SerializeField]
	public bool[] pRCTargetsVisible = new bool[9];



	public Image detectionUIImage;

	private bool playerInLight;

	private float alphaPercentage;

	// Use this for initialization
	void Start ()
	{
		for (int i = 0; i < 9; i++) {
			pRCTargetsVisible [i] = false;
		}
	}

	// Update is called once per frame
	void Update ()
	{
		updateAlphaUI ();
	}

	public void setPRCTargetVisible (int elem, bool visibility)
	{
		pRCTargetsVisible [elem] = visibility;
	}

	void updateAlphaUI ()
	{
		playerInLight = isPlayerInLight ();
		alphaPercentage = detectionUIImage.color.a;

		//Fade Alpha over time smoothly
		if (playerInLight == true) {
			
			alphaPercentage += Time.deltaTime/4f;
			if (alphaPercentage > 1f) {
				alphaPercentage = 1f;

			}
			//detectionUIImage.color = new Color(detectionUIImage.color.r, detectionUIImage.color.g, detectionUIImage.color.b, 1f);
		} else if (playerInLight == false) {

			alphaPercentage -= Time.deltaTime*2;
			if (alphaPercentage < 0f) {
				alphaPercentage = 0f;
			}

			//detectionUIImage.color = new Color(detectionUIImage.color.r, detectionUIImage.color.g, detectionUIImage.color.b, 0f);
		} 
		detectionUIImage.color = new Color (detectionUIImage.color.r, detectionUIImage.color.g, detectionUIImage.color.b, alphaPercentage);
	}



	public bool isPlayerInLight ()
	{
		int numPRCTargetsVisible = 0;

		for (int i = 0; i < 9; i++) {
			if (pRCTargetsVisible [i] == true) {
				numPRCTargetsVisible++;
			}
		}
		if (numPRCTargetsVisible > 2) {
			return true;
		} else if (numPRCTargetsVisible <= 2) {
			return false;
		} else {
			return false;
		}
	}

	public void playerDetected ()
	{
		detectionUIImage.color = new Color (1f, 0f, 0f, 1f);
	}

	public void playerUndetected ()
	{
		detectionUIImage.color = new Color (1f, 1f, 1f, alphaPercentage);
	}

}
