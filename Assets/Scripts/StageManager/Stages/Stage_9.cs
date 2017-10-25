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

    public List<GameObject> JList = new List<GameObject>();
    public List<GameObject> FList = new List<GameObject>();
    public void OnEnable() {
        if (TextPrinter.instance != null)
            TextPrinter.instance.onPrintComplete += EndStage;
    }

    public void OnDisable() {
        TextPrinter.instance.onPrintComplete -= EndStage;
    }

    public override void StartStage() {
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
        if(Input.GetKeyDown(KeyCode.F) && ((GameObject.Find("FKey0") != null && GameObject.Find("FKey0").activeSelf == true) || (GameObject.Find("FKey1") != null && GameObject.Find("FKey1").activeSelf == true) || (GameObject.Find("FKey2") != null && GameObject.Find("FKey2").activeSelf == true))) {
            if(GameObject.Find("FKey0").activeSelf == true) {
                Destroy(GameObject.Find("FKey0"));
            }
            else if (GameObject.Find("FKey1").activeSelf == true) {
                Destroy(GameObject.Find("FKey1"));
            }
            else if (GameObject.Find("FKey2").activeSelf == true) {
                Destroy(GameObject.Find("FKey2"));
            }
        }
        if (Input.GetKeyDown(KeyCode.J) && (GameObject.Find("JKey0").activeSelf == true || GameObject.Find("JKey1").activeSelf == true || GameObject.Find("JKey2").activeSelf == true)) {
            if (GameObject.Find("JKey0").activeSelf == true) {
                Destroy(GameObject.Find("JKey0"));
                //points += 1;
            }
            else if (GameObject.Find("JKey1").activeSelf == true) {
                Destroy(GameObject.Find("JKey1"));
                //points += 1;
            }
            else if (GameObject.Find("JKey2").activeSelf == true) {
                Destroy(GameObject.Find("JKey2"));
                //points += 1;
            }
        }
    }

    IEnumerator StartMinigame() {
        TextPrinter.instance.InvokePrint("Okay, We are going to assess your typing speed. Are you ready?\n\nPress 'F' or 'J' to continue", 0.08f);
        //GameObject.FindObjectOfType<DialogueAudioHandler>().InvokeSoundEffect("STAGE_9");
        yield return new WaitForSeconds(10f);
        mainText.SetActive(false);
        seperator.SetActive(true);
        StartCoroutine(Minigame());
    }

    IEnumerator Minigame() {
        FList[0].SetActive(true);
        yield return new WaitForSeconds(2);
        JList[0].SetActive(true);
        yield return new WaitForSeconds(2);
        FList[1].SetActive(true);
        yield return new WaitForSeconds(2);
        JList[1].SetActive(true);
        yield return new WaitForSeconds(2);
        FList[2].SetActive(true);
        yield return new WaitForSeconds(2);
        JList[2].SetActive(true);
        yield return new WaitForSeconds(2);

        //yield return null;

    }
    
}
