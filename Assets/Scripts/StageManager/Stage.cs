using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour {

	public bool stageIsComplete = false;
	public bool mirrorMode = false;

	int stageNumber;
	public virtual void StartStage(){
		stageIsComplete = false;

	}




	public virtual void EndStage(){
		stageIsComplete = true;
	}
}
