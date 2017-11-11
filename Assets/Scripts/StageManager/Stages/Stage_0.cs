using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage_0 : Stage {
    public bool hasBeenCompleted = false;
	public GameObject background;
	public GameObject lynch;
	public GameObject startText;
	public GameObject title;
	public float unloadWaitTime;
	public void OnEnable(){
		if(TextPrinter.instance != null)
		TextPrinter.instance.onPrintComplete += EndStage;
	}

	public void OnDisable(){
		TextPrinter.instance.onPrintComplete -= EndStage;
	}

	public override void StartStage(){
        TextPrinter.instance.ClearStartText();
        if(hasBeenCompleted == false)
		    TextPrinter.instance.InvokeStartPrint ("press any key to begin", 0.05f);
        else if(hasBeenCompleted == true)
            TextPrinter.instance.InvokeStartPrint("press any key to begin\n\nto play in mirror mode, press 'm'", 0.05f);

    }

	public override void EndStage(){
		stageIsComplete = true;
	}

	void Update(){
		if (stageIsComplete == true && Input.anyKeyDown) {
			//StageManager.instance.StartStage (1);
			StartCoroutine(StartFakeLoadIn());
		}
	}

	IEnumerator StartFakeLoadIn(){
		FindObjectOfType<BackgroundScrollController> ().speed = 0;
		yield return new WaitForSeconds (unloadWaitTime);
		background.SetActive (false);
		yield return new WaitForSeconds (unloadWaitTime);
		startText.SetActive (false);
		yield return new WaitForSeconds (unloadWaitTime);
		lynch.SetActive (false);
		yield return new WaitForSeconds (unloadWaitTime);
		title.SetActive (false);
		yield return new WaitForSeconds (unloadWaitTime);
		StageManager.instance.StartStage (1);

	}
    /*IEnumerator StartGame() {
        
    }*/
}
