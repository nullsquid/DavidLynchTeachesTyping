﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Stage_2 : Stage {
	public Animator animator;
	public ScrollRect scrollrect;
    public Animator rHand;
    public Animator lHand;
    public Animator wipe;
    public Animator homeRow;
	public Animator keyboardWhite;
    public Animator spaceKeyGlow;
    public GameObject wipeObj;
	bool blink = true;

	public void OnEnable(){
		if(TextPrinter.instance != null)
		TextPrinter.instance.onPrintComplete += EndStage;
		TextPrinter.instance.onAnimPause += AnimatorPause;
		TextPrinter.instance.onAnimUnpause += AnimatorUnpause;
	}

	public void OnDisable(){
		TextPrinter.instance.onPrintComplete -= EndStage;
		TextPrinter.instance.onAnimPause -= AnimatorPause;
		TextPrinter.instance.onAnimUnpause -= AnimatorUnpause;
	}
    IEnumerator PixelWipeAndPlay() {
        wipeObj.SetActive(true);
        wipe.SetTrigger("Wipe");
        GameObject.FindObjectOfType<DialogueAudioHandler>().InvokeSoundEffect("PIXEL_WIPE");

        //GameObject.FindObjectOfType<DialogueAudioHandler>().InvokeSoundEffect("PIXEL_WIPE");
        yield return new WaitForSeconds(1.4f);
        wipeObj.SetActive(false);
        yield return new WaitForSeconds(3);
		//StartCoroutine (FadeHomeRow ());
        TextPrinter.instance.printText = GameObject.Find("MainText_2").GetComponent<TextMeshProUGUI>();
        animator.SetBool("IsTalking", true);
        StartCoroutine(FadeEvent());
        TextPrinter.instance.InvokePrint("<To begin, ;.07> {.6} <rest your fingers ;0.08> {.2} <on the \"Home Row\" ;0.08>{.2} <with your 'Left Index Finger' ;0.08> {.45} <on the 'F' key ;0.07>{.4} <and your 'Right Index Finger' ;0.06>{0.5} <on the 'J' key\n\n;0.1>{1}", 0.08f);
        GameObject.FindObjectOfType<DialogueAudioHandler>().InvokeSoundEffect("STAGE_2");
    }

	IEnumerator FadeHomeRow(){
		yield return new WaitForSeconds (5.5f);
		keyboardWhite.SetTrigger ("Fade");
	}

	public override void StartStage(){

        StartCoroutine(PixelWipeAndPlay());

    }

    IEnumerator GlowEvent() {
        yield return new WaitForSeconds(3f);
        homeRow.SetTrigger("FadeIn");
        //yield return new WaitForSeconds(2.5f);
        //homeRow.SetTrigger("FadeOut");
    }

    IEnumerator FadeEvent() {
        StartCoroutine(GlowEvent());
        yield return new WaitForSeconds(3.5f);
		keyboardWhite.SetTrigger ("Fade");
        rHand.SetTrigger("FadeIn");
        lHand.SetTrigger("FadeIn");

    }
    public override void EndStage(){
		animator.SetBool ("IsTalking", false);
		StartCoroutine (TextBlink ());
		//TextPrinter.instance.printText.text += "\n\n<color=yellow>press any key to continue</color>";
		stageIsComplete = true;

	}

	void AnimatorPause() {

		animator.SetBool("IsTalking", false);
	}

	void AnimatorUnpause() {
		animator.SetBool("IsTalking", true);
	}

	IEnumerator TextBlink(){
        spaceKeyGlow.SetTrigger("Glow");
		while (blink == true) {
			//if (!TextPrinter.instance.printText.text.Contains ("<color=yellow>(press any key to continue)</color>")) {
			TextPrinter.instance.printText.text += "<color=yellow>(press 'Space Bar' to continue)</color>";
			yield return new WaitForSeconds (0.5f);
			//} else {
			scrollrect.enabled = false;
			TextPrinter.instance.printText.text = TextPrinter.instance.printText.text.Replace ("<color=yellow>(press 'Space Bar' to continue)</color>", string.Empty);
			yield return new WaitForSeconds (0.5f);
			//}

		}
		TextPrinter.instance.printText.text = TextPrinter.instance.printText.text.Replace ("<color=yellow>(press 'Space Bar' to continue)</color>", string.Empty);

	}

	void Update(){
		if (stageIsComplete == true && Input.GetKeyDown(KeyCode.Space)) {
			//StageManager.instance.StartStage (2);
			TextPrinter.instance.onPrintComplete -= EndStage;
			StageManager.instance.StartStage(3);
		}
	}
}
