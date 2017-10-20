using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour {
	public List<Stage> stages = new List<Stage> ();
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
		StartStage (0);
	}



	public void StartStage(int stageNumber){
		switch (stageNumber) {
		case 0:
			stages [0].StartStage ();
			break;

		default:
			break;
		}
	}
}
