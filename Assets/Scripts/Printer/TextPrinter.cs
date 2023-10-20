using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using System.Text.RegularExpressions;
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

    bool isPrinting = false;
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
        StartCoroutine(ParseText());


	}

    //string PrintAtSpeed()
    IEnumerator ParseText() {
        List<string> newPauseStringComponents = textToPrint.Split('{', '}').Where((item, index) => index % 2 != 0).ToList();
        List<string> newStringSpeedComponents = textToPrint.Split('<', '>').Where((item, index) => index % 2 != 0).ToList();

        int speedTag = -1;
        int pauseTag = -1;
        for (int i = 0; i < textToPrint.Length; i++) {
            //Debug.Log(isPrinting);

            if (isPrinting == false) {
                if (textToPrint[i] == '<') {
                    speedTag += 1;
                    string[] curChunk = newStringSpeedComponents[speedTag].Split(';');
                    StartCoroutine(PrintText(curChunk[0], float.Parse(curChunk[1])));
                }
                if(textToPrint[i] == '{') {
                    isPrinting = true;
                    pauseTag += 1;
                    float timeToPause = float.Parse(newPauseStringComponents[pauseTag]);
                    StartCoroutine(PauseAnim(timeToPause));
                    yield return new WaitForSeconds(timeToPause);
                    isPrinting = false;
                }
                


            }
            else {
                yield return new WaitUntil(() => isPrinting == false);
            }
            
        }
        onPrintComplete();
    }
    IEnumerator PrintText(string text, float time) {
        isPrinting = true;
        //Debug.Log("hi");
        for (int i = 0; i < text.Length; i++) {
            printText.text += text[i];
            yield return new WaitForSeconds(time);

        }
        isPrinting = false;        
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
