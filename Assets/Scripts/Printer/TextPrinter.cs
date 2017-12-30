using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
public class TextPrinter : MonoBehaviour {
	public TextMeshProUGUI startText;
	public TextMeshProUGUI printText;
	public string textToPrint;
	public delegate void AnimPause();
	public delegate void AnimUnpause();
	public delegate void PrintComplete();

	public event AnimPause onAnimPause;
	public event AnimUnpause onAnimUnpause;
	public event PrintComplete onPrintComplete;

    bool isPrinting = true;
	bool isProcessingTag;
	#region Singleton
	public static TextPrinter instance;
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

	public void InvokeStartPrint(string text, float time){
		textToPrint = text;
		StartCoroutine (PrintStartText (time));
	}

	IEnumerator PrintStartText(float timeBtwChars){
		
		for (int i = 0; i < textToPrint.Length; i++) {
			
			startText.text += textToPrint [i];
			yield return new WaitForSeconds (timeBtwChars);
		}
		if (onPrintComplete != null) {
			onPrintComplete ();
		}
	}
	public void InvokePrint(string text, float time){
		textToPrint = text;
		StartCoroutine (PrintText (time));
	}

	IEnumerator PauseAnim(float timeToPause){
		onAnimPause ();
		yield return new WaitForSeconds (timeToPause);
		onAnimUnpause ();
	}
    IEnumerator PausePrint(float timeToPause) {
        isPrinting = false;
        yield return new WaitForSeconds(timeToPause);
        isPrinting = true;
    }

    IEnumerator PrintText(float timeBtwChars) {
		List<string> newStringSpeedComponents = textToPrint.Split ('<', '>').Where((item,index) => index % 2 != 0).ToList();
		Debug.Log (newStringSpeedComponents[0]);
        //while(isPrinting == true)
        //{ 
        for (int i = 0; i < textToPrint.Length; i++) {

            //Debug.Log(textToPrint[i]);
            if (/*textToPrint[i] == '.' ||*/ textToPrint[i] == ',' || textToPrint[i] == '!') {
                if (textToPrint[i] != '<' || textToPrint[i] != '>') {
                    printText.text += textToPrint[i];
                    yield return new WaitForSeconds(0.085f);
                }
            }
			//if (textToPrint [i] == '<') {
				
			
            else {
                if (isProcessingTag == false) {
                    //if (textToPrint [i] != '<' || textToPrint [i] != '>') {
                    printText.text += textToPrint[i];
                    yield return new WaitForSeconds(timeBtwChars);
                    //}
                }
                else if (isProcessingTag == true) {
                    yield return null;
                }
            }

        }
        //}
		if (onPrintComplete != null) {
			Debug.Log ("print complete");
			onPrintComplete ();
		}
	}
    IEnumerator PrintTaggedWords(string word, float time) {
        Debug.Log("rrrr");
        for(int i = 0; i < word.Length; i++) {
            printText.text += word[i];
            yield return new WaitForSeconds(time);
        }
        isProcessingTag = false;
    }
	public void ClearText(){
		printText.text = "";
	}

	public void ClearStartText(){
		startText.text = "";
	}

}
