using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Stage_7 : Stage {
	public Animator animator;
    public void OnEnable() {
        if (TextPrinter.instance != null)
            TextPrinter.instance.onPrintComplete += EndStage;
    }

    public void OnDisable() {
        TextPrinter.instance.onPrintComplete -= EndStage;
    }

    public override void StartStage() {
        TextPrinter.instance.printText = GameObject.Find("MainText_7").GetComponent<TextMeshProUGUI>();
        StartCoroutine(InvokeBugText());
        

    }

    public override void EndStage() {
        stageIsComplete = true;
    }

    void Update() {
        /*if (stageIsComplete == true && Input.GetKeyDown(KeyCode.F)) {
            TextPrinter.instance.onPrintComplete -= EndStage;
            //play animation?
            StageManager.instance.StartStage(4);
        }*/
    }
    IEnumerator InvokeBugText() {
		animator.SetBool ("IsTalking", true);
		Invoke ("StopDialogueAnim", GameObject.FindObjectOfType<DialogueAudioHandler> ().soundEffects ["STAGE_7A"].length);
        TextPrinter.instance.InvokePrint("Now, place your left ring finger inside the undulating bug next to your keyboard.\n\npress bug to continue", 0.08f);
        GameObject.FindObjectOfType<DialogueAudioHandler>().InvokeSoundEffect("STAGE_7A");
        yield return new WaitForSeconds(13f);
		animator.SetBool ("IsTalking", true);
		Invoke ("StopDialogueAnim", GameObject.FindObjectOfType<DialogueAudioHandler> ().soundEffects ["STAGE_7B"].length);
        TextPrinter.instance.InvokePrint("\n\nPlace your left ring finger inside the undulating bug next to your keyboard.", 0.08f);
        GameObject.FindObjectOfType<DialogueAudioHandler>().InvokeSoundEffect("STAGE_7B");
        yield return new WaitForSeconds(9f);
        StageManager.instance.StartStage(8);

    }
	void StopDialogueAnim(){
		animator.SetBool ("IsTalking", false);
	}
}
