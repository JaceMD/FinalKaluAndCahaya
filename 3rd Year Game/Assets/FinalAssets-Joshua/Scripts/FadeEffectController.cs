using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeEffectController : MonoBehaviour {

    public bool Activate = false;

    public float SetTimer;
    public float fadeOutTimer = 0;
    public float fadeInTimer = 0;

    public float FadeMultiplier;

    public Material FadePanel;
    private Color FadeAlpha;

    public MeshRenderer FadeMesh;

	// Use this for initialization
	void Start () {
        FadeAlpha = FadePanel.color;
        FadeAlpha.a = 0;
        FadePanel.color = FadeAlpha;

        fadeOutTimer = 0;
        fadeInTimer = 0;
    }
	
	// Update is called once per frame
	void Update () {
        if(FadePanel.color.a <= 0f)
        {
            FadeMesh.enabled = false;
        }
        else
        {
            FadeMesh.enabled = true;
        }

		if (Activate == true)
        {
            fadeOutTimer = SetTimer;
            Activate = false;
        }

		if (fadeOutTimer > 0f) {
			fadeOutTimer -= Time.deltaTime;
			FadeAlpha.a += FadeMultiplier * Time.deltaTime;
			if (FadeAlpha.a > 1f) {
				FadeAlpha.a = 1f;
			}
			FadePanel.color = FadeAlpha;
			if (fadeOutTimer <= 0f) {
				fadeInTimer = SetTimer;
				FadeAlpha.a = 1f;
				FadePanel.color = FadeAlpha;
			}
		} 
		if (fadeInTimer > 0f) {
			fadeInTimer -= Time.deltaTime;
			FadeAlpha.a -= FadeMultiplier * Time.deltaTime;
			if (FadeAlpha.a < 0f) {
				FadeAlpha.a = 0f;
			}
			FadePanel.color = FadeAlpha;
			if (fadeInTimer <= 0f) {
				FadeAlpha.a = 0f;
				FadePanel.color = FadeAlpha;
			}
		} 
    }
}
