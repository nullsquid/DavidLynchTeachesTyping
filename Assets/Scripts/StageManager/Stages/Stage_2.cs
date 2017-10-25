using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Stage_2 : Stage {

	public void OnEnable(){
		TextPrinter.instance.onPrintComplete += EndStage;
	}

	public void OnDisable(){
		TextPrinter.instance.onPrintComplete -= EndStage;
	}

	public override void StartStage(){
		TextPrinter.instance.printText = GameObject.Find ("MainText_2").GetComponent<TextMeshProUGUI>();
		TextPrinter.instance.InvokePrint ("To begin, you are going to rest your fingers on the \"Home Row\" with your left index finger on the 'f' key and the right index finger on the 'j' key\n\npress any key to continue", 0.08f);
        GameObject.FindObjectOfType<DialogueAudioHandler>().InvokeSoundEffect("STAGE_2");

    }

    public override void EndStage(){
		stageIsComplete = true;

	}

	void Update(){
		if (stageIsComplete == true && Input.anyKeyDown) {
			//StageManager.instance.StartStage (2);
			TextPrinter.instance.onPrintComplete -= EndStage;
			StageManager.instance.StartStage(3);
		}
	}
}
