using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour {

	#region Singleton
	public static StageManager instance;
	void Awake() {

		if (instance == null) {
			instance = this;
		}
		else if (instance != this) {
			Destroy(gameObject);
		}
		DontDestroyOnLoad(gameObject);
	}
	#endregion

	void Start(){
		TextPrinter.instance.InvokePrint ("press any key to begin", 0.05f);
	}

	public void StartStage(int stageNumber){
		switch (stageNumber) {
		case 1:
			break;

		default:
			break;
		}
	}
}
