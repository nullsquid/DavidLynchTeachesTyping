using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DialogueAudioHandler : MonoBehaviour {
	public float volume = 1;
	public List<AudioClip> soundEffectRaw = new List<AudioClip> ();
    public List<AudioClip> notes = new List<AudioClip>();
	public Dictionary<string, AudioClip> soundEffects = new Dictionary<string, AudioClip> ();
    public GameObject channelPanel;
    // Use this for initialization
	void Awake () {
		for (int i = 0; i < soundEffectRaw.Count; i++) {
			soundEffects.Add (soundEffectRaw [i].name, soundEffectRaw [i]);
		}
        //InvokeAmbientAudio("SUBSPACE");

    }

    public void FadeOutSound(string soundName, float fadeTime) {
        AudioManager.Instance.StopSound(soundName, fadeTime);
    }
    public void InvokeSoundEffect(string soundName, float vol = 1){
		AudioManager.Instance.PlaySoundAtPoint (soundEffects [soundName], gameObject, 1, volume);
	}
    public void InvokeAmbientAudio(string soundName) {
        AudioManager.Instance.PlaySoundAtPoint(soundEffects[soundName], gameObject,1,1.0f,1,true,false);
    }
    public void InvokeAudioChannel(string soundName) {
        string prevSoundName = "";
        foreach(Transform child in channelPanel.transform) {
            Destroy(child.gameObject);
        }
        AudioManager.Instance.PlayAudioMemoryAtPoint(soundEffects[soundName], channelPanel);
        
        prevSoundName = soundName;
        Debug.Log(prevSoundName);
    }

    public void StopAudio(string soundName) {
        //AudioManager.Instance.StopSound(soundName);
        foreach(Transform child in this.transform) {
            if (child.name.Contains(soundName)) {
                Destroy(child.gameObject);
            }
        }

    }



    

    public void InvokeMakeSong() {
        StartCoroutine(MakeASong());
    }

    public IEnumerator MakeASong() {
        AudioClip note1 = notes[Random.Range(0, notes.Count)];
        AudioClip note2 = notes[Random.Range(0, notes.Count)];
        AudioClip note3 = notes[Random.Range(0, notes.Count)];
        AudioManager.Instance.PlayAudioClipAtPoint(note1, gameObject);
        yield return new WaitForSeconds(1);
        AudioManager.Instance.PlayAudioClipAtPoint(note2, gameObject);
        yield return new WaitForSeconds(1);
        AudioManager.Instance.PlayAudioClipAtPoint(note3, gameObject);
        yield return new WaitForSeconds(1);

    }
}
