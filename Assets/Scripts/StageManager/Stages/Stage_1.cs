using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Stage_1 : Stage {

	public void Start(){
		TextPrinter.instance.onPrintComplete += EndStage;
	}

	public void OnDisable(){
		TextPrinter.instance.onPrintComplete -= EndStage;
	}

	public override void StartStage(){
		TextPrinter.instance.printText = GameObject.Find ("MainText_1").GetComponent<TextMeshProUGUI>();
		TextPrinter.instance.InvokePrint ("Hello, this is film maker David Lynch.... Today. I'm going to be taking you through the magical\nworld of typing. By the time you've finished this computer program.... you'll be a typing wizard. Let's get started.\n\npress any key to continue", 0.05f);
	}

	public override void EndStage(){
		stageIsComplete = true;
	}

	void Update(){
		if (stageIsComplete == true && Input.anyKeyDown) {
			StageManager.instance.StartStage (2);
		}
	}
}
