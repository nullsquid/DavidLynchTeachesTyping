using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Stage_0 : Stage {
    public bool hasBeenCompleted = false;
	public GameObject background;
	public GameObject lynch;
	public GameObject startText;
	public GameObject title;
	public float unloadWaitTime;
    public Camera mainCamera;
    //specific to intro
    RawImage bkgImg;
    Color temp;
	public void OnEnable(){
		if(TextPrinter.instance != null)
		TextPrinter.instance.onPrintComplete += EndStage;
	}

	public void OnDisable(){
		TextPrinter.instance.onPrintComplete -= EndStage;
	}

    private void Start() {
        
    }
    public void PlayIntro() {
        startText.SetActive(false);
        lynch.SetActive(false);
        title.SetActive(false);
        GameObject.FindObjectOfType<DialogueAudioHandler>().InvokeSoundEffect("INTRO_1");
        StartCoroutine(IntroSequence());
    }

    IEnumerator IntroSequence() {
        mainCamera.GetComponents<postVHSPro>()[1].enabled = true;

        FindObjectOfType<BackgroundScrollController>().speed = 0.125f;
        bkgImg = background.GetComponent<RawImage>();
        bkgImg.color = new Color(bkgImg.color.r, bkgImg.color.g, bkgImg.color.b, 0);
        float fadeTime = 6.6f;
        float opacity = .50f;
        yield return new WaitForSeconds(1.0f);
        StartCoroutine(FadeStripesIn(fadeTime, bkgImg, opacity));
        yield return new WaitForSeconds(fadeTime);
        StartCoroutine(FadeStripesOut(fadeTime, bkgImg, opacity));
        yield return new WaitForSeconds(fadeTime);
        FindObjectOfType<BackgroundScrollController>().speed = 0.05f;
        bkgImg.color = new Color(bkgImg.color.r, bkgImg.color.g, bkgImg.color.b, 1);
        GameObject.FindObjectOfType<DialogueAudioHandler>().InvokeAmbientAudio("INTRO_2");
        startText.SetActive(true);
        lynch.SetActive(true);
        title.SetActive(true);
        mainCamera.GetComponents<postVHSPro>()[1].enabled = false;
        TextPrinter.instance.ClearStartText();
        if (hasBeenCompleted == false)
            TextPrinter.instance.InvokeStartPrint("press any key to begin", 0.05f);
        else if (hasBeenCompleted == true)
            TextPrinter.instance.InvokeStartPrint("press any key to begin\n\nto play in mirror mode, press 'm'", 0.05f);

    }

    IEnumerator FadeStripesIn(float t, RawImage i, float o) {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 0);
        while(i.color.a < o) {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a + (Time.deltaTime / t));
            yield return null;
        }
    }
    IEnumerator FadeStripesOut(float t, RawImage i, float o) {
        i.color = new Color(i.color.r, i.color.g, i.color.b, o);
        while(i.color.a > 0.0f) {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t));
            yield return null;
        }
    }

	public override void StartStage(){
        PlayIntro();
        
    }

	public override void EndStage(){
		stageIsComplete = true;
	}

	void Update(){
		if (stageIsComplete == true && Input.anyKeyDown) {
			//StageManager.instance.StartStage (1);
			StartCoroutine(StartFakeLoadIn());
		}
	}

	IEnumerator StartFakeLoadIn(){
        GameObject.FindObjectOfType<DialogueAudioHandler>().StopAudio("INTRO_2");
        FindObjectOfType<BackgroundScrollController> ().speed = 0;
		yield return new WaitForSeconds (unloadWaitTime);
		background.SetActive (false);
		yield return new WaitForSeconds (unloadWaitTime);
		startText.SetActive (false);
		yield return new WaitForSeconds (unloadWaitTime);
		lynch.SetActive (false);
		yield return new WaitForSeconds (unloadWaitTime);
		title.SetActive (false);
		yield return new WaitForSeconds (unloadWaitTime);
		StageManager.instance.StartStage (1);

	}
    /*IEnumerator StartGame() {
        
    }*/
}
