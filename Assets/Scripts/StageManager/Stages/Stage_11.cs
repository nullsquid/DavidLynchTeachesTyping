using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Video;
using UnityEngine.UI;

public class Stage_11 : Stage {
    public GameObject videoPlayer;
    public Camera mainCamera;
    public Image blackSolid;
    float t = 0;
    Color temp;
    public void OnEnable() {

        TextPrinter.instance.onPrintComplete += EndStage;
    }

    public void OnDisable() {
        TextPrinter.instance.onPrintComplete -= EndStage;
    }

    public override void StartStage() {
        TextPrinter.instance.printText = GameObject.Find("MainText_11").GetComponent<TextMeshProUGUI>();
        TextPrinter.instance.InvokePrint("Okay now using your left pinky finger, hold down the 'A' key", 0.08f);
        GameObject.FindObjectOfType<DialogueAudioHandler>().InvokeSoundEffect("STAGE_11");

    }

    public override void EndStage() {
		TextPrinter.instance.printText.text += "\n\n<color=yellow>hold down 'A' key to continue</color>";
        stageIsComplete = true;

    }

    void Update() {
        if (stageIsComplete == true && Input.GetKey(KeyCode.A)) {
            //StageManager.instance.StartStage (2);
			mainCamera.GetComponent<postVHSPro>().enabled = true;
            TextPrinter.instance.onPrintComplete -= EndStage;
            //StageManager.instance.StartStage(3);
            t += Time.deltaTime / 3;
            temp.a = Mathf.Lerp(0, 1, t);
            blackSolid.color = temp;
			//mainCamera.GetComponent<postVHSPro>().signalNoiseAmount = temp;
			if (temp.a == 1) {
				StartCoroutine(StartVideo());
			}

        }

        
		else if (stageIsComplete == true && Input.GetKeyUp(KeyCode.A) && t < 1) {
			mainCamera.GetComponent<postVHSPro> ().enabled = false;
            //t -= Time.deltaTime / 3;
            t = 0;
            temp.a = 0;
            blackSolid.color = temp;

            TextPrinter.instance.InvokePrint("\n\nOkay now using your left pinky finger, hold down the 'A' key", 0.08f);


        }

    }

    IEnumerator StartVideo() {
        mainCamera.GetComponent<CameraGlitch>().enabled = true;
        yield return new WaitForSeconds(2.0f);
        videoPlayer.SetActive(true);
        float length = (float)videoPlayer.GetComponent<VideoPlayer>().clip.length;
        yield return new WaitForSeconds(length);
		blackSolid.color = new Color (0, 0, 0, 0);
		videoPlayer.SetActive (false);
		mainCamera.GetComponent<CameraGlitch> ().enabled = false;
        StageManager.instance.StartStage(12);
    }


}
