using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage_0 : Stage {

	public void Start(){
		TextPrinter.instance.onPrintComplete += EndStage;
	}

	public void OnDisable(){
		TextPrinter.instance.onPrintComplete -= EndStage;
	}

	public override void StartStage(){
		
		TextPrinter.instance.InvokeStartPrint ("press any key to begin", 0.05f);
	}

	public override void EndStage(){
		stageIsComplete = true;
	}

	void Update(){
		if (stageIsComplete == true && Input.anyKeyDown) {
			StageManager.instance.StartStage (1);
		}
	}
}
