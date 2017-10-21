using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Stage_3 : Stage {

	public void OnEnable(){
		if(TextPrinter.instance != null)
		TextPrinter.instance.onPrintComplete += EndStage;
	}

	public void OnDisable(){
		TextPrinter.instance.onPrintComplete -= EndStage;
	}

	public override void StartStage(){
		TextPrinter.instance.printText = GameObject.Find ("MainText_3").GetComponent<TextMeshProUGUI>();
		TextPrinter.instance.InvokePrint ("Now... Using your left index finger, push down on the F Key\n\npress 'F' continue", 0.05f);
	}

	public override void EndStage(){
		stageIsComplete = true;
	}

	void Update(){
		if (stageIsComplete == true && Input.GetKeyDown(KeyCode.F)) {
			TextPrinter.instance.onPrintComplete -= EndStage;
			//play animation?
			StageManager.instance.StartStage(4);
		}
	}
}
