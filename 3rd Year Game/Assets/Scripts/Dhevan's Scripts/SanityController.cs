using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SanityController : MonoBehaviour {

	private float finalSanity;
	public Slider sanityBar;
	public float sanitySliderSpeed = 1f;
	public float sanityIncrementGain = 1f;
	public float sanityDecrementLoss = 0.1f;


	private float lightExposure;

	// Use this for initialization
	void Start () {
		finalSanity = 100f;
		sanityBar.value = finalSanity;
	}
	
	// Update is called once per frame
	void Update () {
		
		updateSanityBar ();

	}

	public void checkLightExposure(float value){
		lightExposure = value;
		Debug.Log ("light exposure ratio: " + lightExposure);
	}

	void updateSanityBar(){


		if (lightExposure > 0f) {
			finalSanity += Time.deltaTime * sanityIncrementGain * lightExposure;
		} else {
			finalSanity -= Time.deltaTime * sanityDecrementLoss;
		}

		if (finalSanity > 100f) {
			finalSanity = 100f;
		} else if (finalSanity < 0f) {
			finalSanity = 0f;
		}
		sanityBar.value = finalSanity;
	}

	public float getSanityLevel(){
		return finalSanity;
	}
}
