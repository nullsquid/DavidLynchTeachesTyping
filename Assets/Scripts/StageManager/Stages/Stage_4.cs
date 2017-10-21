using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Stage_4 : Stage {

	public void OnEnable(){
		if(TextPrinter.instance != null)
			TextPrinter.instance.onPrintComplete += EndStage;
	}

	public void OnDisable(){
		TextPrinter.instance.onPrintComplete -= EndStage;
	}

	public override void StartStage(){
		TextPrinter.instance.printText = GameObject.Find ("MainText_4").GetComponent<TextMeshProUGUI>();
		StartCoroutine (ShowThumbsUp ());
	}

	public override void EndStage(){
		stageIsComplete = true;
	}

	void Update(){
		if (stageIsComplete == true && Input.GetKeyDown(KeyCode.F)) {
			TextPrinter.instance.onPrintComplete -= EndStage;
			//play animation?
			StageManager.instance.StartStage(5);
		}
	}

	IEnumerator ShowThumbsUp(){
		yield return new WaitForSeconds (1);
		TextPrinter.instance.InvokePrint ("Wow! Excellent! Well Done! Let's move on to the next key.\n\npress 'F' continue", 0.05f);
	}
}
