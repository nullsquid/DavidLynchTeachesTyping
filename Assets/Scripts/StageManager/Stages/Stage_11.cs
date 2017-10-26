using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Video;
public class Stage_11 : Stage {
    public GameObject videoPlayer;
    public Camera mainCamera;
    public void OnEnable() {
        TextPrinter.instance.onPrintComplete += EndStage;
    }

    public void OnDisable() {
        TextPrinter.instance.onPrintComplete -= EndStage;
    }

    public override void StartStage() {
        TextPrinter.instance.printText = GameObject.Find("MainText_11").GetComponent<TextMeshProUGUI>();
        TextPrinter.instance.InvokePrint("Okay now using your left pinky finger, hold down the 'A' key\n\nHold the 'A' key...", 0.08f);
        GameObject.FindObjectOfType<DialogueAudioHandler>().InvokeSoundEffect("STAGE_11");

    }

    public override void EndStage() {
        stageIsComplete = true;

    }

    void Update() {
        if (stageIsComplete == true && Input.GetKeyDown(KeyCode.A)) {
            //StageManager.instance.StartStage (2);
            TextPrinter.instance.onPrintComplete -= EndStage;
            //StageManager.instance.StartStage(3);
            StartCoroutine(StartVideo());
        }
    }

    IEnumerator StartVideo() {
        mainCamera.GetComponent<CameraGlitch>().enabled = true;
        yield return new WaitForSeconds(2.0f);
        videoPlayer.SetActive(true);
        float length = (float)videoPlayer.GetComponent<VideoPlayer>().clip.length;
        yield return new WaitForSeconds(length);
        StageManager.instance.StartStage(12);
    }


}
