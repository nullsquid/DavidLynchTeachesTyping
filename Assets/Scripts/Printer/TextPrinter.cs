using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class TextPrinter : MonoBehaviour {
	public TextMeshProUGUI startText;
	public TextMeshProUGUI printText;
	public string textToPrint;

	public delegate void PrintComplete();
	public event PrintComplete onPrintComplete;

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

	IEnumerator PrintText(float timeBtwChars){

		for (int i = 0; i < textToPrint.Length; i++) {
			
			if (textToPrint [i] == '.' || textToPrint [i] == ',' || textToPrint [i] == '!') {
				if (textToPrint [i] != '<' || textToPrint [i] != '>') {
					printText.text += textToPrint [i];
					yield return new WaitForSeconds (0.085f);
				}
			} else if (textToPrint [i] == '<') {

				isProcessingTag = true;
				string waitTime = "";
				string wordToPrint = "";
				string tag = "";
				for (int j = i; j < textToPrint.Length; j++) {
                    tag += textToPrint[j];
                    //if(textToPrint[j] != ',') {
                    wordToPrint += textToPrint[j];
                    //}
                    if(textToPrint[j] == ';') {
                        for(int t = j + 1; t < textToPrint.Length; t++) {
                            waitTime += textToPrint[t];
                            if(textToPrint[t] == '>') {
                                break;
                            }
                        }
                        
                    }
                    if(textToPrint[j] == '>') {
                        wordToPrint = wordToPrint.Replace(waitTime, string.Empty).Replace("<", string.Empty).Replace(";", string.Empty).Replace(">", string.Empty);
                        break;
                    }


                }
                
				i += tag.Length - 1;
                Debug.Log(wordToPrint +"<<");
				Debug.Log (waitTime.Replace(">", string.Empty).Replace(";", string.Empty));
                
				for (int p = 0; p < wordToPrint.Length; p++) {
                    Debug.Log("running?!");
					printText.text += wordToPrint [p];
					yield return new WaitForSeconds (float.Parse (waitTime.Replace (">", string.Empty).Replace(";", string.Empty)));
				}
                
                //StartCoroutine(PrintTaggedWords(wordToPrint, float.Parse(waitTime.Replace(">", string.Empty).Replace(";", string.Empty))));
                isProcessingTag = false;
				
			}
            
            else {
				if (isProcessingTag == false) {
					//if (textToPrint [i] != '<' || textToPrint [i] != '>') {
						printText.text += textToPrint [i];
						yield return new WaitForSeconds (timeBtwChars);
					//}
				}
                else if(isProcessingTag == true) {
                    yield return null;
                }
			}
            
		}
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
