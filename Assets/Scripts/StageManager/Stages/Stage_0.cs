using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage_0 : Stage {

	public void OnEnable(){
		TextPrinter.instance.onPrintComplete += EndStage;
	}

	public void OnDisable(){
		TextPrinter.instance.onPrintComplete -= EndStage;
	}

	public override void StartStage(){
		
		TextPrinter.instance.InvokePrint ("press any key to begin", 0.05f);
	}

	public override void EndStage(){
		stageIsComplete = true;
	}

	void Update(){
		if (stageIsComplete == true && Input.anyKeyDown) {
			Debug.Log ("next stage");
		}
	}
}
