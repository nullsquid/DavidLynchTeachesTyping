﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class Stage_9 : Stage {
    public ScrollRect scrollrect;
    public Scrollbar mainScrollBar;
    public Scrollbar gameScrollBar;
    public GameObject minigamePanel;
	public GameObject lHand;
	public GameObject rHand;
    public GameObject seperator;
    public GameObject mainText;
    public GameObject portrait;
    public int points = 0;
    public GameObject JPrefab;
    public GameObject FPrefab;
    public GameObject playSpace;
	public GameObject minigameWindow;
	public Animator animator;
    public List<string> sequence = new List<string>();
    public List<GameObject> JList = new List<GameObject>();
    public List<GameObject> FList = new List<GameObject>();
	public List<Sprite> TestSlides = new List<Sprite> ();
	int numberRight = 0;
    float t = 0;
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
        
        TextPrinter.instance.printText = GameObject.Find("MainText_9").GetComponent<TextMeshProUGUI>();
        StartCoroutine(StartMinigame());


    }

    public override void EndStage() {
        stageIsComplete = true;
    }
    IEnumerator AnimateGame(float curValue) {
        t = 0;
        while(t < curValue + 0.05f) {
            t += Time.deltaTime * 1f;
            float newValue = Mathf.Lerp(gameScrollBar.value, gameScrollBar.value += 0.05f, t);
            gameScrollBar.value = newValue;
        }
        yield return null;
    }
    void Update() {
        if(Input.GetKeyDown(KeyCode.F) && sequence[0] == "F") {
			switch (numberRight) {
			case 0:
				minigameWindow.GetComponent<Image> ().sprite = TestSlides [1];
				numberRight += 1;
				break;
			case 1:
				minigameWindow.GetComponent<Image> ().sprite = TestSlides [2];
				numberRight += 1;
				break;
			case 2:
				minigameWindow.GetComponent<Image> ().sprite = TestSlides [3];
				numberRight += 1;
				break;
			case 5:
				minigameWindow.GetComponent<Image> ().sprite = TestSlides [6];
				numberRight += 1;
				break;


			}
            //gameScrollBar.value = gameScrollBar.value + 0.115f;
            sequence.RemoveAt(0);
        }
        if (Input.GetKeyDown(KeyCode.J) && sequence[0] == "J") {
            //gameScrollBar.value = gameScrollBar.value + 0.115f;
			switch (numberRight) {
			case 3:
				minigameWindow.GetComponent<Image> ().sprite = TestSlides [4];
				numberRight += 1;
				break;
			case 4:
				minigameWindow.GetComponent<Image> ().sprite = TestSlides [5];
				numberRight += 1;
				break;
			case 6:
				minigameWindow.GetComponent<Image> ().sprite = TestSlides [7];
				numberRight += 1;
				break;
			case 7:
				minigameWindow.GetComponent<Image> ().sprite = TestSlides [8];
				numberRight += 1;
				break;
			case 8:
				minigameWindow.GetComponent<Image> ().sprite = TestSlides [9];
				numberRight += 1;
				break;
				

				
			}
            sequence.RemoveAt(0);
        }
        if(sequence.Count == 0) {
            minigamePanel.SetActive(false);
            TextPrinter.instance.ClearText();
            StartCoroutine(Evaluation());
        }
        //remove once done
        
        
    }

    IEnumerator StartMinigame() {
		animator.SetBool ("IsTalking", true);
        TextPrinter.instance.InvokePrint("<Okay, ;0.06>{1.5}<We are now going ;0.06>{.6}<to assess your speed. ;0.06>{.6}<Are you ready? ;0.06>", 0.08f);
        GameObject.FindObjectOfType<DialogueAudioHandler>().InvokeSoundEffect("STAGE_9");
        yield return new WaitForSeconds(8f);
        animator.SetBool("IsTalking", false);
        yield return new WaitForSeconds(2f);
        mainText.SetActive(false);
        seperator.SetActive(true);
        minigamePanel.SetActive(true);

    }


    void AnimatorPause() {

		animator.SetBool("IsTalking", false);
	}

	void AnimatorUnpause() {
		animator.SetBool("IsTalking", true);
	}

    
    IEnumerator Evaluation() {
        seperator.SetActive(false);
        mainText.SetActive(true);
        portrait.SetActive(true);
        mainScrollBar.value = 0;
        //TextPrinter.instance.ClearText();
        yield return new WaitForSeconds(2f);
        StageManager.instance.StartStage(10);
    }
}
