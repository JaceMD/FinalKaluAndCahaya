using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameTriggerController : MonoBehaviour {

    public FadeEffectController FadeOut;
    public float SetTimerBeforeCreditsPlay;
    private float getTimer;
    public bool Triggered = false;

	// Use this for initialization
	void Start () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            if (Triggered == false)
            {
                FadeOut.fadeOutTimer = SetTimerBeforeCreditsPlay;
                getTimer = SetTimerBeforeCreditsPlay;
                Triggered = true;
            }
        }
    }

    // Update is called once per frame
    void Update () {
		if(getTimer > 0f)
        {
            getTimer -= Time.deltaTime;
            if(getTimer <= 0f)
            {
                //loadcreditsscene
                SceneManager.LoadScene("CreditsScene");
            }
        }
	}
}
