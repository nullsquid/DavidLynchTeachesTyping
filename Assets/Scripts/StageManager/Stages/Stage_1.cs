using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Video;
public class Stage_1 : Stage {
    public Animator animator;
    float talkLength;
	public float loadInTime = 0.3f;
	public GameObject lynch;
	public GameObject textBox;
    public GameObject blackBox;
	public void Start(){
		TextPrinter.instance.onPrintComplete += EndStage;
	}

	public void OnDisable(){
		TextPrinter.instance.onPrintComplete -= EndStage;
	}

	public override void StartStage(){
		TextPrinter.instance.printText = GameObject.Find ("MainText_1").GetComponent<TextMeshProUGUI>();
		lynch.SetActive (false);
		textBox.SetActive (false);
        blackBox.SetActive(true);
        animator.SetBool("IsTalking", true);
		StartCoroutine(FakeLoadIn());

        

    }
    void StopTalking() {
        animator.SetBool("IsTalking", false);
    }
    public override void EndStage(){
		stageIsComplete = true;
		TextPrinter.instance.printText.text += "\n\n";
		StartCoroutine (TextBlink ());
        animator.SetBool("IsTalking", false);
	}
    public Image blackSolid;
	public GameObject videoPlayer;
    float t = 0;
    Color temp;
	void Update(){
		//Debug.Log(temp.a);
        if (stageIsComplete == true && Input.anyKeyDown) {
            GameObject.FindObjectOfType<DialogueAudioHandler>().InvokeSoundEffect("ROCK");
            StartCoroutine(WaitForRock());
		}
        //test code for the fade out
        /*
        if (Input.GetKey(KeyCode.A)) {
            t += Time.deltaTime / 3;
            temp.a = Mathf.Lerp(0, 1, t);
            blackSolid.color = temp;


        }
		if (temp.a == 1) {
			StartCoroutine (StartVideo ());
		}
		else if (Input.GetKeyUp(KeyCode.A) && t < 1) {
            //t -= Time.deltaTime / 3;
            t = 0;
            temp.a = 0;
            blackSolid.color = temp;
        }
		*/
        
	}
	/*
	IEnumerator StartVideo() {
		Camera.main.GetComponent<CameraGlitch>().enabled = true;
		yield return new WaitForSeconds(2.0f);

		videoPlayer.SetActive(true);
		float length = (float)videoPlayer.GetComponent<VideoPlayer>().clip.length;
		yield return new WaitForSeconds(length);
		StageManager.instance.StartStage(12);
	}
	*/
	IEnumerator TextBlink(){
		while (true) {
			//if (!TextPrinter.instance.printText.text.Contains ("<color=yellow>(press any key to continue)</color>")) {
			TextPrinter.instance.printText.text += "<color=yellow>(press any key to continue)</color>";
			yield return new WaitForSeconds (0.5f);
			//} else {
			TextPrinter.instance.printText.text = TextPrinter.instance.printText.text.Replace ("<color=yellow>(press any key to continue)</color>", string.Empty);
			yield return new WaitForSeconds (0.5f);
			//}

		}
	}

    IEnumerator WaitForRock() {
        stageIsComplete = false;
        yield return new WaitForSeconds(1.8f);
        StageManager.instance.StartStage(2);
    }

	IEnumerator FakeLoadIn(){
        yield return new WaitForSeconds(loadInTime);
        blackBox.SetActive(false);
		yield return new WaitForSeconds (loadInTime);
		lynch.SetActive (true);
		yield return new WaitForSeconds(loadInTime);
		textBox.SetActive(true);
		yield return new WaitForSeconds(loadInTime);
		TextPrinter.instance.InvokePrint ("Hello,\nthis is film maker David Lynch....< ;0.3>I'm going <to;0.01> be taking you< ;0.1> through the magical\nworld of typing.< ;0.15> By the time you've finished this computer program.... you'll be a typing wizard!", 0.08f);
		GameObject.FindObjectOfType<DialogueAudioHandler>().InvokeSoundEffect("STAGE_1");

	}
    
}
