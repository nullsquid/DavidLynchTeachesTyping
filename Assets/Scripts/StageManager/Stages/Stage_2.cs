using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Stage_2 : Stage {
	public Animator animator;
	public ScrollRect scrollrect;
    public Animator rHand;
    public Animator lHand;

	bool blink = true;

	public void OnEnable(){
		if(TextPrinter.instance != null)
		TextPrinter.instance.onPrintComplete += EndStage;
		TextPrinter.instance.onAnimPause += AnimatorPause;
		TextPrinter.instance.onAnimUnpause += AnimatorUnpause;
	}

	public void OnDisable(){
		TextPrinter.instance.onPrintComplete -= EndStage;
		TextPrinter.instance.onAnimPause -= AnimatorPause;
		TextPrinter.instance.onAnimUnpause -= AnimatorUnpause;
	}

	public override void StartStage(){
		TextPrinter.instance.printText = GameObject.Find ("MainText_2").GetComponent<TextMeshProUGUI>();
		animator.SetBool ("IsTalking", true);
        StartCoroutine(FadeEvent());
		TextPrinter.instance.InvokePrint ("To begin, you are going to rest your fingers on the \"Home Row\" with your left index finger on the 'f' key and the right index finger on the 'j' key\n\n", 0.08f);
        GameObject.FindObjectOfType<DialogueAudioHandler>().InvokeSoundEffect("STAGE_2");

    }


    IEnumerator FadeEvent() {
        yield return new WaitForSeconds(3.5f);
        rHand.SetTrigger("FadeIn");
        lHand.SetTrigger("FadeIn");

    }
    public override void EndStage(){
		animator.SetBool ("IsTalking", false);
		StartCoroutine (TextBlink ());
		//TextPrinter.instance.printText.text += "\n\n<color=yellow>press any key to continue</color>";
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
			TextPrinter.instance.printText.text += "<color=yellow>(press space to continue)</color>";
			yield return new WaitForSeconds (0.5f);
			//} else {
			scrollrect.enabled = false;
			TextPrinter.instance.printText.text = TextPrinter.instance.printText.text.Replace ("<color=yellow>(press space to continue)</color>", string.Empty);
			yield return new WaitForSeconds (0.5f);
			//}

		}
		TextPrinter.instance.printText.text = TextPrinter.instance.printText.text.Replace ("<color=yellow>(press space to continue)</color>", string.Empty);

	}

	void Update(){
		if (stageIsComplete == true && Input.GetKeyDown(KeyCode.Space)) {
			//StageManager.instance.StartStage (2);
			TextPrinter.instance.onPrintComplete -= EndStage;
			StageManager.instance.StartStage(3);
		}
	}
}
