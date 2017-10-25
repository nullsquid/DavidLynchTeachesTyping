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

    List<GameObject> JList = new List<GameObject>();
    List<GameObject> FList = new List<GameObject>();
    public void OnEnable() {
        if (TextPrinter.instance != null)
            TextPrinter.instance.onPrintComplete += EndStage;
    }

    public void OnDisable() {
        TextPrinter.instance.onPrintComplete -= EndStage;
    }

    public override void StartStage() {

        TextPrinter.instance.printText = GameObject.Find("MainText_9").GetComponent<TextMeshProUGUI>();
        StartCoroutine(StartMinigame());


    }

    public override void EndStage() {
        stageIsComplete = true;
    }

    void Update() {
        if(FList.Count > 0 && Input.GetKeyDown(KeyCode.F)) {
            GameObject curF = FList[0];
            Destroy(curF);
            FList.Remove(curF);
            
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
        int randNum;
        
        while (points <= 5) {
            yield return new WaitForSeconds(Random.Range(.5f, 2));
            randNum = Random.Range(0, 100);
            if(randNum < 50) {
                GameObject newJ = Instantiate(JPrefab);
                newJ.transform.SetParent(playSpace.transform);
                JList.Add(newJ);

            }
            else {
                GameObject newF = Instantiate(FPrefab);
                FList.Add(newF);
            }
        }
        Debug.Log("win?");
        yield return null;
    }
    
}
