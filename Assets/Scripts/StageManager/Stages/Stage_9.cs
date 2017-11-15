using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Stage_9 : Stage {
    public GameObject seperator;
    public GameObject mainText;
    public GameObject portrait;
    public int points = 0;
    public GameObject JPrefab;
    public GameObject FPrefab;
    public GameObject playSpace;
	public Animator animator;
    public List<GameObject> JList = new List<GameObject>();
    public List<GameObject> FList = new List<GameObject>();
	public void OnEnable() {
		if (TextPrinter.instance != null) {
			TextPrinter.instance.onPrintComplete += EndStage;
			TextPrinter.instance.onAnimPause += AnimatorPause;
			TextPrinter.instance.onAnimUnpause += AnimatorUnpause;
		}
	}

	public void OnDisable() {
		TextPrinter.instance.onPrintComplete -= EndStage;
		TextPrinter.instance.onAnimPause -= AnimatorPause;
		TextPrinter.instance.onAnimUnpause -= AnimatorUnpause;
	}

    public override void StartStage() {
        seperator.SetActive(false);
        for(int i = 0; i < 3; i++) {
            FList[i].SetActive(false);
            JList[i].SetActive(false);
        }
        TextPrinter.instance.printText = GameObject.Find("MainText_9").GetComponent<TextMeshProUGUI>();
        StartCoroutine(StartMinigame());


    }

    public override void EndStage() {
        stageIsComplete = true;
    }

    void Update() {
        if(Input.GetKeyDown(KeyCode.F) && ((GameObject.Find("FKey0") != null && GameObject.Find("FKey0").activeSelf == true) || (GameObject.Find("FKey1") != null && GameObject.Find("FKey1").activeSelf == true) || (GameObject.Find("FKey2") != null && GameObject.Find("FKey2").activeSelf == true) || (GameObject.Find("FKey3") != null && GameObject.Find("FKey3").activeSelf == true) || (GameObject.Find("FKey4") != null && GameObject.Find("FKey4").activeSelf == true) || (GameObject.Find("FKey5") != null && GameObject.Find("FKey5").activeSelf == true) || (GameObject.Find("FKey6") != null && GameObject.Find("FKey6").activeSelf == true) || (GameObject.Find("FKey7") != null && GameObject.Find("FKey7").activeSelf == true) || (GameObject.Find("FKey8") != null && GameObject.Find("FKey8").activeSelf == true) || (GameObject.Find("FKey9") != null && GameObject.Find("FKey9").activeSelf == true) || (GameObject.Find("FKey10") != null && GameObject.Find("FKey10").activeSelf == true) || (GameObject.Find("FKey11") != null && GameObject.Find("FKey11").activeSelf == true))) {
            if(GameObject.Find("FKey0") != null && GameObject.Find("FKey0").activeSelf == true) {
                Destroy(GameObject.Find("FKey0"));
            }
            else if (GameObject.Find("FKey1") != null && GameObject.Find("FKey1").activeSelf == true) {
                Destroy(GameObject.Find("FKey1"));
            }
            else if (GameObject.Find("FKey2") != null && GameObject.Find("FKey2").activeSelf == true) {
                Destroy(GameObject.Find("FKey2"));
            }
            else if (GameObject.Find("FKey3") != null && GameObject.Find("FKey3").activeSelf == true) {
                Destroy(GameObject.Find("FKey3"));
            }
            else if (GameObject.Find("FKey4") != null && GameObject.Find("FKey4").activeSelf == true) {
                Destroy(GameObject.Find("FKey4"));
            }
            else if (GameObject.Find("FKey5") != null && GameObject.Find("FKey5").activeSelf == true) {
                Destroy(GameObject.Find("FKey5"));
            }
            else if (GameObject.Find("FKey6") != null && GameObject.Find("FKey6").activeSelf == true) {
                Destroy(GameObject.Find("FKey6"));
            }
            else if (GameObject.Find("FKey7") != null && GameObject.Find("FKey7").activeSelf == true) {
                Destroy(GameObject.Find("FKey7"));
            }
            else if (GameObject.Find("FKey8") != null && GameObject.Find("FKey8").activeSelf == true) {
                Destroy(GameObject.Find("FKey8"));
            }
            else if (GameObject.Find("FKey9") != null && GameObject.Find("FKey9").activeSelf == true) {
                Destroy(GameObject.Find("FKey9"));
            }
            else if (GameObject.Find("FKey10") != null && GameObject.Find("FKey10").activeSelf == true) {
                Destroy(GameObject.Find("FKey10"));
            }
            else if (GameObject.Find("FKey11") != null && GameObject.Find("FKey11").activeSelf == true) {
                Destroy(GameObject.Find("FKey11"));
            }
        }
        if (Input.GetKeyDown(KeyCode.J) && ((GameObject.Find("JKey0") != null && GameObject.Find("JKey0").activeSelf == true) || (GameObject.Find("JKey1") != null && GameObject.Find("JKey1").activeSelf == true) || (GameObject.Find("JKey2") != null && GameObject.Find("JKey2").activeSelf == true) || (GameObject.Find("JKey3") != null && GameObject.Find("JKey3").activeSelf == true) || (GameObject.Find("JKey4") != null && GameObject.Find("JKey4").activeSelf == true) || (GameObject.Find("JKey5") != null && GameObject.Find("JKey5").activeSelf == true) || (GameObject.Find("JKey6") != null && GameObject.Find("JKey6").activeSelf == true) || (GameObject.Find("JKey7") != null && GameObject.Find("JKey7").activeSelf == true) || (GameObject.Find("JKey8") != null && GameObject.Find("JKey8").activeSelf == true) || (GameObject.Find("JKey9") != null && GameObject.Find("JKey9").activeSelf == true) || (GameObject.Find("JKey10") != null && GameObject.Find("JKey10").activeSelf == true) || (GameObject.Find("JKey11") != null && GameObject.Find("JKey11").activeSelf == true))) {
            if (GameObject.Find("JKey0") != null && GameObject.Find("JKey0").activeSelf == true) {
                Destroy(GameObject.Find("JKey0"));
                //points += 1;
            }
            else if (GameObject.Find("JKey1") != null && GameObject.Find("JKey1").activeSelf == true) {
                Destroy(GameObject.Find("JKey1"));
                //points += 1;
            }
            else if (GameObject.Find("JKey2") != null && GameObject.Find("JKey2").activeSelf == true) {
                Destroy(GameObject.Find("JKey2"));
                //points += 1;
            }
            else if (GameObject.Find("JKey3") != null && GameObject.Find("JKey3").activeSelf == true) {
                Destroy(GameObject.Find("JKey3"));
                //points += 1;
            }
            else if (GameObject.Find("JKey4") != null && GameObject.Find("JKey4").activeSelf == true) {
                Destroy(GameObject.Find("JKey4"));
                //points += 1;
            }
            else if (GameObject.Find("JKey5") != null && GameObject.Find("JKey5").activeSelf == true) {
                Destroy(GameObject.Find("JKey5"));
                //points += 1;
            }
            else if (GameObject.Find("JKey6") != null && GameObject.Find("JKey6").activeSelf == true) {
                Destroy(GameObject.Find("JKey6"));
                //points += 1;
            }
            else if (GameObject.Find("JKey7") != null && GameObject.Find("JKey7").activeSelf == true) {
                Destroy(GameObject.Find("JKey7"));
                //points += 1;
            }
            else if (GameObject.Find("JKey7") != null && GameObject.Find("JKey7").activeSelf == true) {
                Destroy(GameObject.Find("JKey7"));
                //points += 1;
            }
            else if (GameObject.Find("JKey8") != null && GameObject.Find("JKey8").activeSelf == true) {
                Destroy(GameObject.Find("JKey8"));
                //points += 1;
            }
            else if (GameObject.Find("JKey9") != null && GameObject.Find("JKey9").activeSelf == true) {
                Destroy(GameObject.Find("JKey9"));
                //points += 1;
            }
            else if (GameObject.Find("JKey10") != null && GameObject.Find("JKey10").activeSelf == true) {
                Destroy(GameObject.Find("JKey10"));
                //points += 1;
            }
            else if (GameObject.Find("JKey11") != null && GameObject.Find("JKey11").activeSelf == true) {
                Destroy(GameObject.Find("JKey11"));
                //points += 1;
            }
        }
    }

    IEnumerator StartMinigame() {
		animator.SetBool ("IsTalking", true);
        TextPrinter.instance.InvokePrint("Okay, We are going to assess your typing speed. Are you ready?", 0.08f);
        GameObject.FindObjectOfType<DialogueAudioHandler>().InvokeSoundEffect("STAGE_9");
        yield return new WaitForSeconds(10f);
        mainText.SetActive(false);
        seperator.SetActive(true);
        StartCoroutine(Minigame());
    }


	void AnimatorPause() {

		animator.SetBool("IsTalking", false);
	}

	void AnimatorUnpause() {
		animator.SetBool("IsTalking", true);
	}

    IEnumerator Minigame() {
		animator.SetBool ("IsTalking", false);
        FList[0].SetActive(true);
        yield return new WaitForSeconds(Random.Range(.5f, 2f));
        JList[0].SetActive(true);
        yield return new WaitForSeconds(Random.Range(.5f, 2f));
        FList[1].SetActive(true);
        yield return new WaitForSeconds(Random.Range(.5f, 2f));
        JList[1].SetActive(true);
        yield return new WaitForSeconds(Random.Range(.5f, 2f));
        FList[2].SetActive(true);
        yield return new WaitForSeconds(Random.Range(.5f, 2f));
        JList[2].SetActive(true);
        yield return new WaitForSeconds(Random.Range(.5f, 2f));
        ////////////////////////
        FList[3].SetActive(true);
        yield return new WaitForSeconds(Random.Range(.5f, 2f));
        JList[3].SetActive(true);
        yield return new WaitForSeconds(Random.Range(.5f, 2f));
        FList[4].SetActive(true);
        yield return new WaitForSeconds(Random.Range(.5f, 2f));
        JList[4].SetActive(true);
        yield return new WaitForSeconds(Random.Range(.5f, 2f));
        FList[5].SetActive(true);
        yield return new WaitForSeconds(Random.Range(.5f, 2f));
        JList[5].SetActive(true);
        yield return new WaitForSeconds(Random.Range(.5f, 2f));
        ////////////////////////
        FList[6].SetActive(true);
        yield return new WaitForSeconds(Random.Range(.5f, 2f));
        JList[6].SetActive(true);
        yield return new WaitForSeconds(Random.Range(.5f, 2f));
        FList[7].SetActive(true);
        yield return new WaitForSeconds(Random.Range(.5f, 2f));
        JList[7].SetActive(true);
        yield return new WaitForSeconds(Random.Range(.5f, 2f));
        FList[8].SetActive(true);
        yield return new WaitForSeconds(Random.Range(.5f, 2f));
        JList[8].SetActive(true);
        yield return new WaitForSeconds(Random.Range(.5f, 2f));
        /////////////////////////
        FList[9].SetActive(true);
        yield return new WaitForSeconds(Random.Range(.5f, 2f));
        JList[9].SetActive(true);
        yield return new WaitForSeconds(Random.Range(.5f, 2f));
        FList[10].SetActive(true);
        yield return new WaitForSeconds(Random.Range(.5f, 2f));
        JList[10].SetActive(true);
        yield return new WaitForSeconds(Random.Range(.5f, 2f));
        FList[11].SetActive(true);
        yield return new WaitForSeconds(Random.Range(.5f, 2f));
        JList[11].SetActive(true);
        yield return new WaitForSeconds(Random.Range(.5f, 2f));
        StartCoroutine(Evaluation());
    }
    IEnumerator Evaluation() {
        seperator.SetActive(false);
        mainText.SetActive(true);
        portrait.SetActive(true);
        TextPrinter.instance.InvokePrint("\n\nEvaluating...", 0.1f);
        yield return new WaitForSeconds(1.5f);
        StageManager.instance.StartStage(10);
    }
}
