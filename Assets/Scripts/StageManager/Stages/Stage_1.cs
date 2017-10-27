using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class Stage_1 : Stage {

	public void Start(){
		TextPrinter.instance.onPrintComplete += EndStage;
	}

	public void OnDisable(){
		TextPrinter.instance.onPrintComplete -= EndStage;
	}

	public override void StartStage(){
		TextPrinter.instance.printText = GameObject.Find ("MainText_1").GetComponent<TextMeshProUGUI>();
		TextPrinter.instance.InvokePrint ("Hello, this is film maker David Lynch....I'm going to be taking you through the magical\nworld of typing. By the time you've finished this computer program.... you'll be a typing wizard. Let's get started.\n\npress any key to continue", 0.08f);
        GameObject.FindObjectOfType<DialogueAudioHandler>().InvokeSoundEffect("STAGE_1");

    }

    public override void EndStage(){
		stageIsComplete = true;
	}
    public Image blackSolid;
    float t = 0;
    Color temp;
	void Update(){
        //testing
        
        if (stageIsComplete == true && Input.anyKeyDown) {
			StageManager.instance.StartStage (2);
		}
        //test code for the fade out
        /*
        if (Input.GetKey(KeyCode.A)) {
            t += Time.deltaTime / 3;
            temp.a = Mathf.Lerp(0, 1, t);
            blackSolid.color = temp;
        }
        else if (Input.GetKeyUp(KeyCode.A)) {
            //t -= Time.deltaTime / 3;
            t = 0;
            temp.a = 0;
            blackSolid.color = temp;
        }
        */
	}
    
}
