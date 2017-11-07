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
					tag += textToPrint [j];
					if (textToPrint [j] != ',') {
						wordToPrint += textToPrint [j];
					} else if (textToPrint [j] == ',') {
						wordToPrint.Remove (0, 1);
						for (int t = j; t < textToPrint.Length; t++) {
							waitTime += textToPrint [t];

						}
					}
					if (textToPrint [i] == '>') {
						isProcessingTag = false;
						break;
					}
					 
				}

				i += tag.Length - 1;
				Debug.Log (waitTime);
				for (int k = 0; k < wordToPrint.Length; k++) {
					printText.text += wordToPrint [k];
					yield return new WaitForSeconds (float.Parse (waitTime.Replace (">", string.Empty).Replace(",", string.Empty)));
				}
				/*
				string waitTime = "";
				isProcessingTag = true;
				for (int j = i; j < textToPrint.Length; j++) {
					if (textToPrint [j] != '<' || textToPrint [j] != '>') {
						waitTime += textToPrint [j];
					}
					if (textToPrint [j] == '>') {
						isProcessingTag = false;
						break;
					}
				}
				i += waitTime.Length - 1;
				//Debug.Log (waitTime);
				yield return new WaitForSeconds (float.Parse (waitTime.Replace("<",string.Empty).Replace(">", string.Empty)));

				*/
			} else {
				if (isProcessingTag == false) {
					//if (textToPrint [i] != '<' || textToPrint [i] != '>') {
						printText.text += textToPrint [i];
						yield return new WaitForSeconds (timeBtwChars);
					//}
				}
			}
            
		}
		if (onPrintComplete != null) {
			Debug.Log ("print complete");
			onPrintComplete ();
		}
	}

	public void ClearText(){
		printText.text = "";
	}

	public void ClearStartText(){
		startText.text = "";
	}

}
