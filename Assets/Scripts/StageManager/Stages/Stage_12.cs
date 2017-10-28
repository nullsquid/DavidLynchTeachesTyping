using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Video;
public class Stage_12 : Stage {
    public GameObject videoPlayer;
    public Camera mainCamera;
    public void OnEnable() {
        TextPrinter.instance.onPrintComplete += EndStage;
    }

    public void OnDisable() {
        TextPrinter.instance.onPrintComplete -= EndStage;
    }

    public override void StartStage() {
        videoPlayer.SetActive(false);
        mainCamera.GetComponent<CameraGlitch>().enabled = false;
        TextPrinter.instance.printText = GameObject.Find("MainText_12").GetComponent<TextMeshProUGUI>();
        
        GameObject.FindObjectOfType<DialogueAudioHandler>().InvokeSoundEffect("STAGE_12");
        StartCoroutine(InvokeThankYou());
    }

    public override void EndStage() {
        stageIsComplete = true;

    }

    void Update() {
        
    }

    IEnumerator InvokeThankYou() {
        TextPrinter.instance.InvokePrint("Trial Version Over, Thank you for typing.", 0.08f);
        yield return new WaitForSeconds(1.5f);
        TextPrinter.instance.InvokePrint("Thank you for letting me teach you how to type. To purchase to full version, go into the nearest bathtub and make smacking noises with your hands until someone can assist you.", 0.08f);
        GameObject.FindObjectOfType<DialogueAudioHandler>().InvokeSoundEffect("STAGE_12");
        yield return new WaitForSeconds(1.5f);
        StageManager.instance.StartStage(0);
    }




}
