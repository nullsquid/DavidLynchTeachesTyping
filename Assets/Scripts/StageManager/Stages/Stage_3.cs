using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Stage_3 : Stage {
	public Animator animator;
	public void OnEnable(){
		if(TextPrinter.instance != null)
		TextPrinter.instance.onPrintComplete += EndStage;
	}

	public void OnDisable(){
		TextPrinter.instance.onPrintComplete -= EndStage;
	}

	public override void StartStage(){
		TextPrinter.instance.printText = GameObject.Find ("MainText_3").GetComponent<TextMeshProUGUI>();
		animator.SetBool ("IsTalking", true);
		TextPrinter.instance.InvokePrint ("Now... Using your left index finger, push down on the F Key\n\npress 'F' continue", 0.08f);
        GameObject.FindObjectOfType<DialogueAudioHandler>().InvokeSoundEffect("STAGE_3");

    }

    public override void EndStage(){
		animator.SetBool ("IsTalking", false);
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
