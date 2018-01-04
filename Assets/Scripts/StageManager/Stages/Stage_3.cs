using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Stage_3 : Stage {
	public Animator animator;
	public ScrollRect scrollrect;
    public Animator fKeyGlow;
    public Animator fingerGlow;
    public Animator rHandAnimator;
    public Animator keyboard;
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

    IEnumerator FadeAndStartStage() {
        rHandAnimator.SetTrigger("FadeOut");
        yield return new WaitForSeconds(2.0f);
        StartCoroutine(FadeEvents());
        TextPrinter.instance.printText = GameObject.Find("MainText_3").GetComponent<TextMeshProUGUI>();
        animator.SetBool("IsTalking", true);
        TextPrinter.instance.InvokePrint("<Using your ;0.08>{.2}<'Left ;0.075>{.2}<Index Finger', ;0.08>{.6} <push down ;0.09> {.6} <on the ;0.06> <'F' ;0.06> {.4}<Key\n\n;0.06>{1}", 0.08f);
        GameObject.FindObjectOfType<DialogueAudioHandler>().InvokeSoundEffect("STAGE_3");
    }

    public override void StartStage(){
        StartCoroutine(FadeAndStartStage());

    }

    IEnumerator FadeEvents() {
        yield return new WaitForSeconds(1.0f);
        fingerGlow.SetBool("StartGlow", true);
        
        yield return new WaitForSeconds(4.65f);
        //keyboard.SetTrigger("FadeIn");
        fingerGlow.SetBool("StartGlow", false);
        fKeyGlow.SetTrigger("FadeIn");
        
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
