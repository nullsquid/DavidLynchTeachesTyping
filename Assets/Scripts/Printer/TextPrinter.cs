using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
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
            else if (textToPrint[i] == '{') {
                string waitTime = "";
                Debug.Log("bloop");
                isProcessingTag = true;
                for (int j = i + 1; j < textToPrint.Length; j++) {
                    waitTime += textToPrint[j];
                    if (textToPrint[j] == '}') {
                        textToPrint = textToPrint.Remove(j - 1, 2);
                        waitTime = waitTime.Replace("}", string.Empty);
                        break;
                    }
                    else {

                    }
                }

                StartCoroutine(PauseAnim(float.Parse(waitTime)));
                //StartCoroutine(PausePrint(float.Parse(waitTime)));
                yield return new WaitForSeconds(float.Parse(waitTime));
                isProcessingTag = false;
            }
            else if (textToPrint[i] == '<') {

                isProcessingTag = true;
                string waitTime = "";
                string wordToPrint = "";
                string tag = "";
                for (int j = i; j < textToPrint.Length; j++) {
                    tag += textToPrint[j];
                    //if(textToPrint[j] != ',') {
                    wordToPrint += textToPrint[j];
                    //}
                    if (textToPrint[j] == ';') {
                        for (int t = j + 1; t < textToPrint.Length; t++) {
                            waitTime += textToPrint[t];
                            if (textToPrint[t] == '>') {
                                break;
                            }
                        }

                    }
                    if (textToPrint[j] == '>') {
                        wordToPrint = wordToPrint.Replace(waitTime, string.Empty).Replace("<", string.Empty).Replace(";", string.Empty).Replace(">", string.Empty);
                        break;
                    }


                }
                //Debug.Log(tag.Length);
                //remove this comment if doesn't work
                //i += tag.Length - 1;
                //Debug.Log(wordToPrint + "<<");
                Debug.Log(waitTime.Replace(">", string.Empty).Replace(";", string.Empty));
                //Debug.Log("TAG IS " + tag);
                for (int p = 0; p < wordToPrint.Length; p++) {
                    //Debug.Log("running?!");
                    printText.text += wordToPrint[p];
                    //textToPrint = textToPrint.Replace(tag, string.Empty);
                    yield return new WaitForSeconds(float.Parse(waitTime.Replace(">", string.Empty).Replace(";", string.Empty)));
                }
                Debug.Log("TAG IS " + tag + "<<");
                textToPrint = textToPrint.Replace(tag, string.Empty);
                //StartCoroutine(PrintTaggedWords(wordToPrint, float.Parse(waitTime.Replace(">", string.Empty).Replace(";", string.Empty))));
                isProcessingTag = false;

            }
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
