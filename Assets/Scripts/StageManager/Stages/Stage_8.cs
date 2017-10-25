using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Stage_8 : Stage {
    public Camera mainCamera;
    public void OnEnable() {
        if (TextPrinter.instance != null)
            TextPrinter.instance.onPrintComplete += EndStage;
    }

    public void OnDisable() {
        TextPrinter.instance.onPrintComplete -= EndStage;
    }
    
    public override void StartStage() {
        
        TextPrinter.instance.printText = GameObject.Find("MainText_8").GetComponent<TextMeshProUGUI>();
        StartCoroutine(InvokeErrorText());


    }

    public override void EndStage() {
        stageIsComplete = true;
    }

    void Update() {
        /*if (stageIsComplete == true && Input.GetKeyDown(KeyCode.F)) {
            TextPrinter.instance.onPrintComplete -= EndStage;
            //play animation?
            StageManager.instance.StartStage(4);
        }*/
    }
    IEnumerator InvokeErrorText() {
        mainCamera.GetComponent<CameraGlitch>().enabled = true;
        TextPrinter.instance.InvokePrint("ERROR\n\nERROR\n\nERROR\n\n", 0.001f);

        yield return new WaitForSeconds(1.5f);
        mainCamera.GetComponent<CameraGlitch>().enabled = false;
        TextPrinter.instance.InvokePrint("It appears there has been a glitch in the software... We will have to move on\n\npress any key to continue", 0.08f);
        GameObject.FindObjectOfType<DialogueAudioHandler>().InvokeSoundEffect("STAGE_8");

    }
}
