using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Video;
using UnityEngine.UI;

public class Stage_11C : Stage {
    public ScrollRect scrollrect;
    public GameObject videoPlayer;
    public Camera mainCamera;
    public Image blackSolid;
    public Animator keyboard;
    public Animator aKey;
    public Animator pinkyGlow;
    bool pressed = false;
    float t = 0;
    bool blink = true;
	public Animator animator;
    int timesPressed = 0;
    Color temp;
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

    IEnumerator AKeyHighlight() {
        yield return new WaitForSeconds(8.7f);
        //keyboard.SetTrigger("FadeIn");
        aKey.SetTrigger("FadeIn");
    }

    IEnumerator PinkyGlow() {
        yield return new WaitForSeconds(4.0f);
        pinkyGlow.SetBool("StartGlow", true);
        yield return new WaitForSeconds(3.5f);
        pinkyGlow.SetBool("StartGlow", false);
    }

    public override void StartStage() {
        blackSolid.color = new Color(blackSolid.color.r, blackSolid.color.g, blackSolid.color.b, 0);
        StartCoroutine(PinkyGlow());
        StartCoroutine(AKeyHighlight());
        TextPrinter.instance.printText = GameObject.Find("MainText_11C").GetComponent<TextMeshProUGUI>();
        animator.SetBool("IsTalking", true);
        TextPrinter.instance.InvokePrint("{1}<Okay ;0.07>{1.1}<using your ;0.07>{.2}<'Left ;0.07>{.4}<Pinky Finger', ;0.07>{.4}<hold down ;0.07>{.5}<the ;0.07>{.2}<'A' ;0.07>{.2}<key\n\n;0.07>", 0.08f);
        GameObject.FindObjectOfType<DialogueAudioHandler>().InvokeSoundEffect("STAGE_11_REDUX_REV");

    }

    public override void EndStage() {
        StartCoroutine(TextBlink());
		//TextPrinter.instance.printText.text += "\n\n<color=yellow>hold down 'A' key to continue</color>";
        animator.SetBool("IsTalking", false);
        stageIsComplete = true;

    }
    IEnumerator GlitchSwitch() {
        mainCamera.GetComponent<CameraGlitch>().enabled = true;
        yield return new WaitForSeconds(0.6f);
        mainCamera.GetComponent<CameraGlitch>().enabled = false;
        StageManager.instance.StartStage(12);
    }
    IEnumerator TextBlink() {
        while (blink == true) {
            //if (!TextPrinter.instance.printText.text.Contains ("<color=yellow>(press any key to continue)</color>")) {
            TextPrinter.instance.printText.text += "<color=yellow>(hold down 'A' key to continue)</color>";
            yield return new WaitForSeconds(0.5f);
            //} else {
            scrollrect.enabled = false;
            TextPrinter.instance.printText.text = TextPrinter.instance.printText.text.Replace("<color=yellow>(hold down 'A' key to continue)</color>", string.Empty);
            yield return new WaitForSeconds(0.5f);
            //}

        }
        TextPrinter.instance.printText.text = TextPrinter.instance.printText.text.Replace("<color=yellow>(hold down 'A' key to continue)</color>", string.Empty);

    }
    IEnumerator KeyWasPressed() {
        
        if (timesPressed == 0 && pressed == true) {
            temp.a = 0;
            blackSolid.color = new Color(blackSolid.color.r, blackSolid.color.g, blackSolid.color.b, 0);

            StageManager.instance.StartStage(11);
            //Invoke("KeyWasPressed", 0);
            yield return new WaitForSeconds(1);
            timesPressed++;
            pressed = false;
        }
        else if (timesPressed == 1 && pressed == true) {
            temp.a = 0;
            blackSolid.color = new Color(blackSolid.color.r, blackSolid.color.g, blackSolid.color.b, 0);

            StageManager.instance.StartStage(11);
            yield return new WaitForSeconds(1);
            timesPressed++;
            //Invoke("KeyWasPressed", 0);
            pressed = false;
        }
        else if (timesPressed == 2 && temp.a == 1) {
            //StageManager.instance.StartStage(12);
            //StartCoroutine(StartVideo());
        }
        yield return null;
    }
    void InvokeNextPress() {
        StartCoroutine(KeyWasPressed());
    }
    void Update() {
        if (stageIsComplete == true && Input.GetKey(KeyCode.A)) {
            //StageManager.instance.StartStage (2);
			mainCamera.GetComponent<postVHSPro>().enabled = true;
            TextPrinter.instance.onPrintComplete -= EndStage;
            //StageManager.instance.StartStage(3);
            /*t += Time.deltaTime / 3;
            temp.a = Mathf.Lerp(0, 1, t);            blackSolid.color = temp;
			//mainCamera.GetComponent<postVHSPro>().signalNoiseAmount = temp;
			if (temp.a == 1) {
                Debug.Log(timesPressed);
                StartCoroutine(StartVideo());


            }
            */
            //StartCoroutine(StartVideo());
            StartCoroutine(GlitchSwitch());

        }
        


        else if (stageIsComplete == true && Input.GetKeyUp(KeyCode.A) && t < 1) {
			mainCamera.GetComponent<postVHSPro> ().enabled = false;
            blink = false;
            //t -= Time.deltaTime / 3;
            t = 0;
            temp.a = 0;
            blackSolid.color = temp;
            Invoke("SetBlink", GameObject.FindObjectOfType<DialogueAudioHandler>().soundEffects["STAGE_11_REDUX_REV"].length);
            TextPrinter.instance.InvokePrint("\n\nOkay now using your left pinky finger, hold down the 'A' key\n\n", 0.08f);


        }
        

    }
    void SetBlink() {
        blink = true;
        StartCoroutine(TextBlink());

    }
	void AnimatorPause() {

		animator.SetBool("IsTalking", false);
	}

	void AnimatorUnpause() {
		animator.SetBool("IsTalking", true);
	}


    


}
