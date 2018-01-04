using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Video;
public class Stage_12 : Stage {
    public GameObject videoPlayer;
    public Camera mainCamera;
	//might need to fix this up some
	//public Animator animator;
	public void OnEnable() {
		if (TextPrinter.instance != null) {
			TextPrinter.instance.onPrintComplete += EndStage;
			TextPrinter.instance.onAnimPause += AnimatorPause;
			TextPrinter.instance.onAnimUnpause += AnimatorUnpause;
		}
	}

	public void OnDisable() {
		TextPrinter.instance.onPrintComplete -= EndStage;
		TextPrinter.instance.onAnimPause -= AnimatorPause;
		TextPrinter.instance.onAnimUnpause -= AnimatorUnpause;
	}

    public override void StartStage() {
        videoPlayer.SetActive(false);
        mainCamera.GetComponent<CameraGlitch>().enabled = false;
        TextPrinter.instance.printText = GameObject.Find("MainText_12").GetComponent<TextMeshProUGUI>();
        
        //GameObject.FindObjectOfType<DialogueAudioHandler>().InvokeSoundEffect("STAGE_12");
        StartCoroutine(InvokeThankYou());
    }

    public override void EndStage() {
        stageIsComplete = true;

    }

    void Update() {
        
    }

	void AnimatorPause() {

		//animator.SetBool("IsTalking", false);
	}

	void AnimatorUnpause() {
		//animator.SetBool("IsTalking", true);
	}

    IEnumerator InvokeThankYou() {
        //animator.SetBool("IsTalking", true);
		GameObject.FindObjectOfType<DialogueAudioHandler>().InvokeAmbientAudio("INTRO_2");
        TextPrinter.instance.InvokePrint("<\nThank you for letting me teach you how to type. ;0.08>{1}<To unlock the full version, go into the nearest bathtub and make smacking noises with your hands until someone can assist you.;0.08>", 0.08f);
        GameObject.FindObjectOfType<DialogueAudioHandler>().InvokeSoundEffect("STAGE_12");
        yield return new WaitForSeconds(17.5f);
		GameObject.FindObjectOfType<DialogueAudioHandler>().StopAudio("INTRO_2");
        yield return new WaitForSeconds(2.0f);
        Application.Quit();
        //StageManager.instance.StartStage(0);
    }




}
