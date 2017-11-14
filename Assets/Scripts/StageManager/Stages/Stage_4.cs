using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Stage_4 : Stage {
	public Animator animator;
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

    public override void StartStage(){
		TextPrinter.instance.printText = GameObject.Find ("MainText_4").GetComponent<TextMeshProUGUI>();
		StartCoroutine (ShowThumbsUp ());

    }

    public override void EndStage(){
		stageIsComplete = true;
		TextPrinter.instance.printText.text += "\n\n<color=yellow>press 'F' to continue</color>";
		animator.SetBool ("IsTalking", false);
	}

	void Update(){
		if (stageIsComplete == true && Input.GetKeyDown(KeyCode.F)) {
			TextPrinter.instance.onPrintComplete -= EndStage;
			//play animation?
			StageManager.instance.StartStage(5);
		}
	}
    void AnimatorPause() {

        animator.SetBool("IsTalking", false);
    }

    void AnimatorUnpause() {
        animator.SetBool("IsTalking", true);
    }

    IEnumerator ShowThumbsUp(){
		yield return new WaitForSeconds (1);
		animator.SetBool ("IsTalking", true);
		TextPrinter.instance.InvokePrint ("Wow! {.5}Excellent!{.5} Well Done!{.5} Let's move on to the next key.", 0.08f);
        GameObject.FindObjectOfType<DialogueAudioHandler>().InvokeSoundEffect("STAGE_4");

    }
}
