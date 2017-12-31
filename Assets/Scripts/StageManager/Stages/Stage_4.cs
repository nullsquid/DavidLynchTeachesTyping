using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Stage_4 : Stage {
	public Animator animator;
	bool blink = true;
    public Animator handAnimator;
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
		StartCoroutine (TextBlink ());
		//TextPrinter.instance.printText.text += "\n\n<color=yellow>press 'F' to continue</color>";
		animator.SetBool ("IsTalking", false);
	}

	void Update(){
		if (stageIsComplete == true && Input.GetKeyDown(KeyCode.Space)) {
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

	IEnumerator TextBlink(){
		while (blink == true) {
			//if (!TextPrinter.instance.printText.text.Contains ("<color=yellow>(press any key to continue)</color>")) {
			TextPrinter.instance.printText.text += "<color=yellow>(press 'Space Bar' to continue)</color>";
			yield return new WaitForSeconds (0.5f);
			//} else {
			TextPrinter.instance.printText.text = TextPrinter.instance.printText.text.Replace ("<color=yellow>(press 'Space Bar' to continue)</color>", string.Empty);
			yield return new WaitForSeconds (0.5f);
			//}

		}
		TextPrinter.instance.printText.text = TextPrinter.instance.printText.text.Replace ("<color=yellow>(press 'Space Bar' to continue)</color>", string.Empty);

	}

    IEnumerator FadeHand() {
        yield return new WaitForSeconds(2f);
        handAnimator.SetTrigger("FadeOut");
    }

    IEnumerator ShowThumbsUp(){
        StartCoroutine(FadeHand());
		yield return new WaitForSeconds (1);
		animator.SetBool ("IsTalking", true);
		TextPrinter.instance.InvokePrint ("<Wow! ;0.08>{.5}<Excellent! ;0.08>{.5}<Well Done! ;0.08>{.5}<Let's move on ;0.08>{.3}<to the next key.\n\n;0.08>{1}", 0.08f);
        GameObject.FindObjectOfType<DialogueAudioHandler>().InvokeSoundEffect("STAGE_4");

    }
}
