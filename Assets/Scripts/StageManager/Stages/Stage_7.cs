using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Stage_7 : Stage {
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
        TextPrinter.instance.InvokePrint("Now, place your left ring finger inside the undulating bug next to your keyboard.\n\n", 0.08f);
        GameObject.FindObjectOfType<DialogueAudioHandler>().InvokeSoundEffect("STAGE_7A");
        yield return new WaitForSeconds(13f);
		animator.SetBool ("IsTalking", true);
		Invoke ("StopDialogueAnim", GameObject.FindObjectOfType<DialogueAudioHandler> ().soundEffects ["STAGE_7B"].length);
		blink = false;
        TextPrinter.instance.InvokePrint("Place your left ring finger inside the undulating bug", 0.08f);
        GameObject.FindObjectOfType<DialogueAudioHandler>().InvokeSoundEffect("STAGE_7B");
        yield return new WaitForSeconds(9f);
        StageManager.instance.StartStage(8);

    }
	IEnumerator TextBlink(){
		while (blink == true) {
			//if (!TextPrinter.instance.printText.text.Contains ("<color=yellow>(press any key to continue)</color>")) {
			TextPrinter.instance.printText.text += "<color=yellow>(press bug to continue)</color>";
			yield return new WaitForSeconds (0.5f);
			//} else {
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
