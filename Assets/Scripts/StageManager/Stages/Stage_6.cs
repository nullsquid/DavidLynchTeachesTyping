using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Stage_6 : Stage {
    public ScrollRect scrollrect;
	public Animator animator;
    public Animator handAnimator;
    public Animator spaceGlow;
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
        if (stageIsComplete == true && Input.GetKeyDown(KeyCode.Space)) {
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
        spaceGlow.SetTrigger("Glow");
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

    IEnumerator HandFade() {
        yield return new WaitForSeconds(1f);
        handAnimator.SetTrigger("FadeOut");

    }

    IEnumerator ShowThumbsUp() {
        StartCoroutine(HandFade());
        yield return new WaitForSeconds(1);
		animator.SetBool ("IsTalking", true);
        TextPrinter.instance.InvokePrint("{1}<Wow! ;0.08>{.5}<Amazing! ;0.08>{.5}<Beautiful. ;0.08>{.2}<We are doing Beautiful work here. ;0.06>{.5}<That was just great! ;0.08>{.7}<Let's ;0.08>{.3}<move ;0.08>{.3}<on.\n\n;0.08>{1}", 0.08f);
        GameObject.FindObjectOfType<DialogueAudioHandler>().InvokeSoundEffect("STAGE_6");

    }
}
