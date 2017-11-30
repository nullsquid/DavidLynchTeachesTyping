using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Stage_3 : Stage {
	public Animator animator;
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

    public override void StartStage(){
		TextPrinter.instance.printText = GameObject.Find ("MainText_3").GetComponent<TextMeshProUGUI>();
		animator.SetBool ("IsTalking", true);
		TextPrinter.instance.InvokePrint ("Now... Using your left index finger, push down on the F Key\n\n", 0.08f);
        GameObject.FindObjectOfType<DialogueAudioHandler>().InvokeSoundEffect("STAGE_3");

    }

    public override void EndStage(){
		animator.SetBool ("IsTalking", false);
		StartCoroutine (TextBlink ());
		//TextPrinter.instance.printText.text += "\n\n<color=yellow>press 'F' to continue</color>";
		stageIsComplete = true;
	}
	IEnumerator TextBlink(){
		while (blink == true) {
			//if (!TextPrinter.instance.printText.text.Contains ("<color=yellow>(press any key to continue)</color>")) {
			TextPrinter.instance.printText.text += "<color=yellow>(press 'F' to continue)</color>";
			yield return new WaitForSeconds (0.5f);
			//} else {
			scrollrect.enabled = false;
			TextPrinter.instance.printText.text = TextPrinter.instance.printText.text.Replace ("<color=yellow>(press 'F' to continue)</color>", string.Empty);
			yield return new WaitForSeconds (0.5f);
			//}

		}
		TextPrinter.instance.printText.text = TextPrinter.instance.printText.text.Replace ("<color=yellow>(press 'F' to continue)</color>", string.Empty);

	}
    void AnimatorPause() {

        animator.SetBool("IsTalking", false);
    }

    void AnimatorUnpause() {
        animator.SetBool("IsTalking", true);
    }
    void Update(){
		if (stageIsComplete == true && Input.GetKeyDown(KeyCode.F)) {
			TextPrinter.instance.onPrintComplete -= EndStage;
            GameObject.FindObjectOfType<DialogueAudioHandler>().InvokeSoundEffect("BLOOP_GOOD_1");
            //play animation?
            StageManager.instance.StartStage(4);
		}
	}
}
