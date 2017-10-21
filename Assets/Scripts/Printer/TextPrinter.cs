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
			printText.text += textToPrint [i];
			yield return new WaitForSeconds (timeBtwChars);
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
