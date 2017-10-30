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
		for (int i = 0; i < stages.Count; i++) {
			if (i == 0) {
				//if(stages[i] != null)
				stages [i].gameObject.SetActive (true);
				StartStage (0);
			} else {
				if(stages[i] != null)
				stages [i].gameObject.SetActive (false);
			}
		}

	}



	public void StartStage(int stageNumber){
		switch (stageNumber) {
		case 0:
            for (int i = 0; i < stages.Count; i++) {
				if(stages[i] != null)
				stages [i].gameObject.SetActive (false);
			}
            stages [0].gameObject.SetActive (true);
			stages [0].StartStage ();
			break;
		case 1:
			for (int i = 0; i < stages.Count; i++) {
				if(stages[i] != null)
				stages [i].gameObject.SetActive (false);
			}
			stages [1].gameObject.SetActive (true);
			stages [1].StartStage ();
			break;
		case 2:
			for (int i = 0; i < stages.Count; i++) {
				if(stages[i] != null)
				stages [i].gameObject.SetActive (false);
			}
			stages [2].gameObject.SetActive (true);
			stages [2].StartStage ();	
			break;
		case 3:
			for (int i = 0; i < stages.Count; i++) {
				if(stages[i] != null)
					stages [i].gameObject.SetActive (false);
			}
			stages [3].gameObject.SetActive (true);
			stages [3].StartStage ();	
			break;
		case 4:
			for (int i = 0; i < stages.Count; i++) {
				if(stages[i] != null)
					stages [i].gameObject.SetActive (false);
			}
			stages [4].gameObject.SetActive (true);
			stages [4].StartStage ();	
			break;
            case 5:
                for (int i = 0; i < stages.Count; i++) {
                    if (stages[i] != null)
                        stages[i].gameObject.SetActive(false);
                }
                stages[5].gameObject.SetActive(true);
                stages[5].StartStage();
                break;
            case 6:
                for (int i = 0; i < stages.Count; i++) {
                    if (stages[i] != null)
                        stages[i].gameObject.SetActive(false);
                }
                stages[6].gameObject.SetActive(true);
                stages[6].StartStage();
                break;
            case 7:
                for (int i = 0; i < stages.Count; i++) {
                    if (stages[i] != null)
                        stages[i].gameObject.SetActive(false);
                }
                stages[7].gameObject.SetActive(true);
                stages[7].StartStage();
                break;
            case 8:
                for (int i = 0; i < stages.Count; i++) {
                    if (stages[i] != null)
                        stages[i].gameObject.SetActive(false);
                }
                stages[8].gameObject.SetActive(true);
                stages[8].StartStage();
                break;
            case 9:
                for (int i = 0; i < stages.Count; i++) {
                    if (stages[i] != null)
                        stages[i].gameObject.SetActive(false);
                }
                stages[9].gameObject.SetActive(true);
                stages[9].StartStage();
                break;
            case 10:
                for (int i = 0; i < stages.Count; i++) {
                    if (stages[i] != null)
                        stages[i].gameObject.SetActive(false);
                }
                stages[10].gameObject.SetActive(true);
                stages[10].StartStage();
                break;
            case 11:
                for (int i = 0; i < stages.Count; i++) {
                    if (stages[i] != null)
                        stages[i].gameObject.SetActive(false);
                }
                stages[11].gameObject.SetActive(true);
                stages[11].StartStage();
                break;
		case 12:
			for (int i = 0; i < stages.Count; i++) {
				if (stages[i] != null)
					stages[i].gameObject.SetActive(false);
			}
			stages[12].gameObject.SetActive(true);
			stages[12].StartStage();
			break;
            default:
			
			break;
		}
	}
}
