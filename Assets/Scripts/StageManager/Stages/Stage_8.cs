using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class Stage_8 : Stage {
	public Animator animator;
    public Camera mainCamera;
    public GameObject errorMessage;
	bool blink = true;
    public ScrollRect scrollrect;
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
        //TextPrinter.instance.printText = GameObject.Find("MainText_8").GetComponent<TextMeshProUGUI>();
        TextPrinter.instance.printText = GameObject.Find("ErrorText").GetComponent<TextMeshProUGUI>();
        StartCoroutine(InvokeErrorText());


    }

    public override void EndStage() {
		StartCoroutine (TextBlink ());
        //TextPrinter.instance.printText.text += "\n\n<color=yellow>press any key to continue</color>";
        animator.SetBool ("IsTalking", false);
        stageIsComplete = true;
    }

    void Update() {
		if (stageIsComplete == true && Input.GetKeyDown(KeyCode.Space)) {
            TextPrinter.instance.onPrintComplete -= EndStage;
			//TextPrinter.instance.InvokePrint("\n\nPlace your left ring finger inside the undulating bug next to your keyboard.", 0.08f);

            //play animation?
            StageManager.instance.StartStage(9);
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
			TextPrinter.instance.printText.text += "(press 'Space Bar' to continue)";
			yield return new WaitForSeconds (0.5f);
            //} else {
            scrollrect.enabled = false;
            TextPrinter.instance.printText.text = TextPrinter.instance.printText.text.Replace ("(press 'Space Bar' to continue)", string.Empty);
			yield return new WaitForSeconds (0.5f);
			//}

		}
		TextPrinter.instance.printText.text = TextPrinter.instance.printText.text.Replace ("(press 'Space Bar' to continue)", string.Empty);

	}

    IEnumerator InvokeErrorText() {
        mainCamera.GetComponent<CameraGlitch>().enabled = true;
        //TextPrinter.instance.InvokePrint("<color=red>ERROR\n\nERROR\n\nERROR\n\n</color>", 0.001f);

        yield return new WaitForSeconds(1.5f);
        mainCamera.GetComponent<CameraGlitch>().enabled = false;
        errorMessage.SetActive(true);
        //yield return new WaitForSeconds(1.0f);
        //errorMessage.SetActive(false);
		animator.SetBool ("IsTalking", true);
        TextPrinter.instance.InvokePrint("<It appears there has been a glitch in the software... We will have to move on\n\n;0.01>{8}", 0.01f);
        GameObject.FindObjectOfType<DialogueAudioHandler>().InvokeSoundEffect("STAGE_8");

    }
}
