﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Stage_7 : Stage {
	public Animator animator;
    public Animator keyboardAnim;
    public Animator bugAnim;
    public Animator fingerAnim;
    public ScrollRect scrollrect;
	bool blink = true;
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
        GameObject.FindObjectOfType<DialogueAudioHandler>().InvokeSoundEffect("BLOOP_START_1");
        TextPrinter.instance.printText = GameObject.Find("MainText_7").GetComponent<TextMeshProUGUI>();
        StartCoroutine(StartBugSound());
        StartCoroutine(InvokeBugText());
        

    }

    public override void EndStage() {
        stageIsComplete = true;
    }

    IEnumerator StartBugSound() {
        yield return new WaitForSeconds(4.5f);
        GameObject.FindObjectOfType<DialogueAudioHandler>().InvokeAmbientAudio("BUGGLE");
    }

    IEnumerator InvokeBugSlideIn() {
        yield return new WaitForSeconds(5.0f);
        keyboardAnim.SetTrigger("SlideOut");
        bugAnim.SetTrigger("BugSlideIn");
    }

    IEnumerator FingerGlow() {
        yield return new WaitForSeconds(2.5f);
        fingerAnim.SetBool("StartGlow", true);
        yield return new WaitForSeconds(2f);
        fingerAnim.SetBool("StartGlow", false);
        yield return new WaitForSeconds(9f);
        fingerAnim.SetBool("StartGlow", true);
        yield return new WaitForSeconds(2f);
        fingerAnim.SetBool("StartGlow", false);



    }

    IEnumerator InvokeBugText() {
        StartCoroutine(FingerGlow());
        StartCoroutine(InvokeBugSlideIn());
        //yield return new WaitForSeconds(7f);
		animator.SetBool ("IsTalking", true);
		Invoke ("StopDialogueAnim", GameObject.FindObjectOfType<DialogueAudioHandler> ().soundEffects ["STAGE_7A"].length);
        TextPrinter.instance.InvokePrint("<Now, ;0.07>{.7}<place your 'Left ;0.07>{.2}<Ring Finger' ;0.07>{.5}<in the undulating bug ;0.07>{.1}<next to your keyboard.\n\n; 0.07>", 0.08f);
        GameObject.FindObjectOfType<DialogueAudioHandler>().InvokeSoundEffect("STAGE_7A");
		//was 13f
        yield return new WaitForSeconds(19f);
		animator.SetBool ("IsTalking", true);
		Invoke ("StopDialogueAnim", GameObject.FindObjectOfType<DialogueAudioHandler> ().soundEffects ["STAGE_7B"].length);
		Invoke ("Blink", GameObject.FindObjectOfType<DialogueAudioHandler> ().soundEffects ["STAGE_7B"].length);
		blink = false;
        scrollrect.enabled = true;

        TextPrinter.instance.InvokePrint("<Place your 'Left ;0.07>{.2}<Ring Finger' ;0.07>{.5}<in the undulating bug ;0.07>\n\n", 0.08f);
        GameObject.FindObjectOfType<DialogueAudioHandler>().InvokeSoundEffect("STAGE_7B");
        yield return new WaitForSeconds(18f);
        GameObject.FindObjectOfType<DialogueAudioHandler>().StopAudio("BUGGLE");
        StageManager.instance.StartStage(8);

    }
	void Blink(){
		blink = true;
		TextPrinter.instance.printText.text += "\n\n";
		StartCoroutine (TextBlink());
	}

	IEnumerator TextBlink(){
		while (blink == true) {
			//if (!TextPrinter.instance.printText.text.Contains ("<color=yellow>(press any key to continue)</color>")) {
			TextPrinter.instance.printText.text += "<color=yellow>(press bug to continue)</color>";
			yield return new WaitForSeconds (0.5f);
            //} else {
            scrollrect.enabled = false;
            TextPrinter.instance.printText.text = TextPrinter.instance.printText.text.Replace ("<color=yellow>(press bug to continue)</color>", string.Empty);
			yield return new WaitForSeconds (0.5f);
			//}

		}
		TextPrinter.instance.printText.text = TextPrinter.instance.printText.text.Replace ("<color=yellow>(press bug to continue)</color>", string.Empty);

	}
	void AnimatorPause() {

		animator.SetBool("IsTalking", false);
	}

	void AnimatorUnpause() {
		animator.SetBool("IsTalking", true);
	}

	void StopDialogueAnim(){
		//TextPrinter.instance.printText.text += "\n\n<color=yellow>press bug to continue</color>";
		StartCoroutine (TextBlink ());
		animator.SetBool ("IsTalking", false);
	}
}
