using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Stage_9 : Stage {
    public void OnEnable() {
        if (TextPrinter.instance != null)
            TextPrinter.instance.onPrintComplete += EndStage;
    }

    public void OnDisable() {
        TextPrinter.instance.onPrintComplete -= EndStage;
    }

    public override void StartStage() {

        TextPrinter.instance.printText = GameObject.Find("MainText_9").GetComponent<TextMeshProUGUI>();
        Application.Quit();


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
    
}
