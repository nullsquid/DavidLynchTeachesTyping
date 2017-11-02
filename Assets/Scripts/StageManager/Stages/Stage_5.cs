using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Stage_5 : Stage {
	public Animator animator;
    public void OnEnable() {
        if (TextPrinter.instance != null)
            TextPrinter.instance.onPrintComplete += EndStage;
    }

    public void OnDisable() {
        TextPrinter.instance.onPrintComplete -= EndStage;
    }

    public override void StartStage() {
        TextPrinter.instance.printText = GameObject.Find("MainText_5").GetComponent<TextMeshProUGUI>();
		animator.SetBool ("IsTalking", true);
        TextPrinter.instance.InvokePrint("Alright kiddo...using your 'right Index Finger’, push down on the 'j' key.", 0.1f);
        GameObject.FindObjectOfType<DialogueAudioHandler>().InvokeSoundEffect("STAGE_5");

    }

    public override void EndStage() {
		animator.SetBool ("IsTalking", false);
		TextPrinter.instance.printText.text += "\n\n<color=yellow>press 'J' continue</color>";

        stageIsComplete = true;
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
