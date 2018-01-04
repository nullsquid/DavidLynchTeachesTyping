using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using TOZ;
public class Stage_10 : Stage {
    public GameObject speechBubble;
    public GameObject coffeeAndCigaretteBreak;
	public Animator animator;
    public Animator rHand;
    public Animator lHand;
    public Animator blackFade;
    public GameObject mainTextArea;
    public Camera mainCamera;
    bool coffeeBreak = false;
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
        speechBubble.SetActive(false);
        mainTextArea.SetActive(true);
        TextPrinter.instance.printText = GameObject.Find("MainText_10").GetComponent<TextMeshProUGUI>();
        StartCoroutine(ShowSpeechBubble());


    }

    public override void EndStage() {
        stageIsComplete = true;
    }

    void Update() {
        if (stageIsComplete == true && Input.anyKeyDown) {
            
            //play animation?
            //StageManager.instance.StartStage(11);
        }
        if(coffeeBreak == true && Input.anyKeyDown) {
            GameObject.FindObjectOfType<DialogueAudioHandler>().StopAudio("JAZZU");
            mainCamera.GetComponent<TOZ.ImageEffects.PP_Amnesia>().enabled = false;
            StageManager.instance.StartStage(11);
        }
    }
    IEnumerator ShowSpeechBubble() {
        yield return new WaitForSeconds(1f);
        speechBubble.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        speechBubble.SetActive(false);
        StartCoroutine(WaitForSpeechBubble());

    }
	void AnimatorPause() {

		animator.SetBool("IsTalking", false);
	}

	void AnimatorUnpause() {
		animator.SetBool("IsTalking", true);
	}
    IEnumerator WaitForSpeechBubble() {
		animator.SetBool ("IsTalking", false);
		Invoke ("StopAnim", GameObject.FindObjectOfType<DialogueAudioHandler> ().soundEffects ["STAGE_10"].length);
        AnimatorUnpause();
        TextPrinter.instance.InvokePrint("<Great work ;0.07>{.4}<kiddo! ;0.07>{1}<How about ;0.07>{.2}<you reward yourself ;0.07>{.2}<with a coffee and a smoke? ;0.07>{4}", 0.08f);
        GameObject.FindObjectOfType<DialogueAudioHandler>().InvokeSoundEffect("STAGE_10");
        yield return new WaitForSeconds(9.5f);
        GameObject.FindObjectOfType<DialogueAudioHandler>().InvokeAmbientAudio("JAZZU");
        coffeeAndCigaretteBreak.SetActive(true);
        mainTextArea.SetActive(false);
        yield return new WaitForSeconds(2.0f);
        TextPrinter.instance.onPrintComplete -= EndStage;
        //have to press a button to stop smoke break
        coffeeBreak = true;
        animator.SetBool("BreakTime", true);
        blackFade.SetTrigger("FadeIn");
        //mainCamera.GetComponent<TOZ.ImageEffects.PP_Amnesia>().enabled = true;
        //mainCamera.GetComponent<TOZ.ImageEffects.PP_Amnesia>().density = 0.3f;
        //mainCamera.GetComponent<TOZ.ImageEffects.PP_Amnesia>().speed = .04f;
        lHand.SetTrigger("FadeLong");
        rHand.SetTrigger("FadeLong");



    }

    void StopAnim(){
		animator.SetBool ("IsTalking", false);
	}
}
