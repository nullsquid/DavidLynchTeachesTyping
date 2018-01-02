using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Stage_5 : Stage {
    public ScrollRect scrollrect;
	public Animator animator;
    public Animator handAnimator;
    public Animator keyboardAnimator;
    public Animator fingerAnimator;
    public Animator jKey;
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

    IEnumerator FadeKeyboardIn() {
        yield return new WaitForSeconds(8.0f);
        //keyboardAnimator.SetTrigger("FadeIn");
        jKey.SetTrigger("FadeIn");
    }
    IEnumerator FingerGlow() {
        yield return new WaitForSeconds(4.5f);
        fingerAnimator.SetBool("StartGlow", true);
        yield return new WaitForSeconds(1.5f);
        fingerAnimator.SetBool("StartGlow", false);

    }

    IEnumerator FadeHandAndStart() {
        StartCoroutine(FingerGlow());
        StartCoroutine(FadeKeyboardIn());
        handAnimator.SetTrigger("FadeOut");
        yield return new WaitForSeconds(1.5f);
        TextPrinter.instance.printText = GameObject.Find("MainText_5").GetComponent<TextMeshProUGUI>();
        animator.SetBool("IsTalking", true);
        
        TextPrinter.instance.InvokePrint("<Alright ;0.09>{.4}<kiddo. ;0.09>{.6}<using your 'right ;0.07>{.2}<Index Finger’ ;0.07>,{.2}<push down ;0.09>{.3}<on the ;0.09><'j' key.\n\n;0.1>", 0.1f);
        GameObject.FindObjectOfType<DialogueAudioHandler>().InvokeSoundEffect("STAGE_5");
    }

    public override void StartStage() {
        StartCoroutine(FadeHandAndStart());

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
            scrollrect.enabled = false;
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
