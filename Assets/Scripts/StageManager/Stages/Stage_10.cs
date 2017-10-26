﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Stage_10 : Stage {
    public GameObject speechBubble;
    public GameObject coffeeAndCigaretteBreak;
    public void OnEnable() {
        if (TextPrinter.instance != null)
            TextPrinter.instance.onPrintComplete += EndStage;
    }

    public void OnDisable() {
        TextPrinter.instance.onPrintComplete -= EndStage;
    }

    public override void StartStage() {
        speechBubble.SetActive(false);
        TextPrinter.instance.printText = GameObject.Find("MainText_10").GetComponent<TextMeshProUGUI>();
        StartCoroutine(ShowSpeechBubble());


    }

    public override void EndStage() {
        stageIsComplete = true;
    }

    void Update() {
        if (stageIsComplete == true && Input.anyKeyDown) {
            TextPrinter.instance.onPrintComplete -= EndStage;
            //play animation?
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
    IEnumerator WaitForSpeechBubble() {
        TextPrinter.instance.InvokePrint("Great work on that typing kiddo! How about you reward yourself with a coffee and a smoke?", 0.08f);
        GameObject.FindObjectOfType<DialogueAudioHandler>().InvokeSoundEffect("STAGE_10");
        yield return new WaitForSeconds(9.5f);
        coffeeAndCigaretteBreak.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        StageManager.instance.StartStage(11);


    }
}
