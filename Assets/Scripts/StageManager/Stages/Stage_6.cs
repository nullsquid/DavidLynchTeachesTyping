using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Stage_6 : Stage {

    public void OnEnable() {
        if (TextPrinter.instance != null)
            TextPrinter.instance.onPrintComplete += EndStage;
    }

    public void OnDisable() {
        TextPrinter.instance.onPrintComplete -= EndStage;
    }

    public override void StartStage() {
        TextPrinter.instance.printText = GameObject.Find("MainText_6").GetComponent<TextMeshProUGUI>();
        StartCoroutine(ShowThumbsUp());

    }

    public override void EndStage() {
        stageIsComplete = true;
    }

    void Update() {
        if (stageIsComplete == true && Input.anyKeyDown) {
            TextPrinter.instance.onPrintComplete -= EndStage;
            //play animation?
            StageManager.instance.StartStage(7);
        }
    }

    IEnumerator ShowThumbsUp() {
        yield return new WaitForSeconds(1);
        TextPrinter.instance.InvokePrint("Wow! Amazing! Beautiful. We are doing Beautiful work here. That was just great! Let's move on to the next one.\n\npress any key to continue", 0.08f);
        GameObject.FindObjectOfType<DialogueAudioHandler>().InvokeSoundEffect("STAGE_6");

    }
}
