﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Stage_5 : Stage {
	public Animator animator;
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
        TextPrinter.instance.printText = GameObject.Find("MainText_5").GetComponent<TextMeshProUGUI>();
		animator.SetBool ("IsTalking", true);
        TextPrinter.instance.InvokePrint("Alright kiddo...using your 'right Index Finger’, push down on the 'j' key.\n\n", 0.1f);
        GameObject.FindObjectOfType<DialogueAudioHandler>().InvokeSoundEffect("STAGE_5");

    }

    public override void EndStage() {
		animator.SetBool ("IsTalking", false);
		//TextPrinter.instance.printText.text += "\n\n<color=yellow>press 'J' continue</color>";
		StartCoroutine(TextBlink());
        stageIsComplete = true;
    }

    void AnimatorPause() {

        animator.SetBool("IsTalking", false);
    }

    void AnimatorUnpause() {
        animator.SetBool("IsTalking", true);
    }

	IEnumerator TextBlink(){
		while (blink == true) {
			//if (!TextPrinter.instance.printText.text.Contains ("<color=yellow>(press any key to continue)</color>")) {
			TextPrinter.instance.printText.text += "<color=yellow>(press 'J' continue)</color>";
			yield return new WaitForSeconds (0.5f);
			//} else {
			TextPrinter.instance.printText.text = TextPrinter.instance.printText.text.Replace ("<color=yellow>(press 'J' continue)</color>", string.Empty);
			yield return new WaitForSeconds (0.5f);
			//}

		}
		TextPrinter.instance.printText.text = TextPrinter.instance.printText.text.Replace ("<color=yellow>(press 'J' continue)</color>", string.Empty);

	}
    void Update() {
        if (stageIsComplete == true && Input.GetKeyDown(KeyCode.J)) {
            GameObject.FindObjectOfType<DialogueAudioHandler>().InvokeSoundEffect("BLOOP_GOOD_1");
            TextPrinter.instance.onPrintComplete -= EndStage;
            //play animation?
            StageManager.instance.StartStage(6);
        }
    }
}
