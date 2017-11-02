using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Stage_6 : Stage {
	public Animator animator;
    public void OnEnable() {
        if (TextPrinter.instance != null)
            TextPrinter.instance.onPrintComplete += EndStage;
    }

    public void OnDisable() {
        TextPrinter.instance.onPrintComplete -= EndStage;
    }

    public override void StartStage() {
        TextPrinter.instance.printText = GameObject.Find("MainText_6").GetComponent<TextMeshProUGUI>();
        StartCoroutine(ShowThumbsUp());

    }

    public override void EndStage() {
        stageIsComplete = true;
		TextPrinter.instance.printText.text += "\n\n<color=yellow>press any key to continue</color>";
		animator.SetBool ("IsTalking", false);
    }

    void Update() {
        if (stageIsComplete == true && Input.anyKeyDown) {
            TextPrinter.instance.onPrintComplete -= EndStage;
            //play animation?
            StageManager.instance.StartStage(7);
        }
    }

    IEnumerator ShowThumbsUp() {
        yield return new WaitForSeconds(1);
		animator.SetBool ("IsTalking", true);
        TextPrinter.instance.InvokePrint("Wow! Amazing! Beautiful. We are doing Beautiful work here. That was just great! Let's move on.", 0.08f);
        GameObject.FindObjectOfType<DialogueAudioHandler>().InvokeSoundEffect("STAGE_6");

    }
}
