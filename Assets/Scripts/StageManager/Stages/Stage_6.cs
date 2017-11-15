using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Stage_6 : Stage {
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
        TextPrinter.instance.printText = GameObject.Find("MainText_6").GetComponent<TextMeshProUGUI>();
        StartCoroutine(ShowThumbsUp());

    }

    public override void EndStage() {
        stageIsComplete = true;
		StartCoroutine (TextBlink ());
		//TextPrinter.instance.printText.text += "\n\n<color=yellow>press any key to continue</color>";
		animator.SetBool ("IsTalking", false);
    }

    void Update() {
        if (stageIsComplete == true && Input.anyKeyDown) {
            TextPrinter.instance.onPrintComplete -= EndStage;
            //play animation?
            StageManager.instance.StartStage(7);
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
			TextPrinter.instance.printText.text += "<color=yellow>(press any key to continue)</color>";
			yield return new WaitForSeconds (0.5f);
			//} else {
			TextPrinter.instance.printText.text = TextPrinter.instance.printText.text.Replace ("<color=yellow>(press any key to continue)</color>", string.Empty);
			yield return new WaitForSeconds (0.5f);
			//}

		}
		TextPrinter.instance.printText.text = TextPrinter.instance.printText.text.Replace ("<color=yellow>(press any key to continue)</color>", string.Empty);

	}

    IEnumerator ShowThumbsUp() {
        yield return new WaitForSeconds(1);
		animator.SetBool ("IsTalking", true);
        TextPrinter.instance.InvokePrint("Wow! Amazing! Beautiful. We are doing Beautiful work here. That was just great! Let's move on.\n\n", 0.08f);
        GameObject.FindObjectOfType<DialogueAudioHandler>().InvokeSoundEffect("STAGE_6");

    }
}
