using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {
    public Dictionary<string, AudioSource> clipsPlaying = new Dictionary<string, AudioSource>();
    public delegate void OnAudioClipComplete();
    public event OnAudioClipComplete audioClipComplete;
    #region Singleton
    private static AudioManager _instance;
    public static AudioManager Instance
    {
        get
        {
            if(_instance == null)
            {
                GameObject tam = new GameObject("TinyAudioManager");
                tam.AddComponent<AudioManager>();
                
            }
            return _instance;
        }

    }
    void Awake()
    {
        _instance = this;
        
    }
    #endregion

    public void PlayAudioMemoryAtPoint(AudioClip clip, GameObject objectToPlayOn) {

        //StartCoroutine(PlayClipAtPoint(clip, objectToPlayOn));
        GameObject go = new GameObject("AudioSource");
        float randomPitch = Random.Range(-0.1f, 0.2f);
        go.AddComponent<AudioSource>();
        AudioSource newAudioSource = go.GetComponent<AudioSource>();
        newAudioSource.name = clip.name + '_' + objectToPlayOn.name;
        newAudioSource.transform.position = objectToPlayOn.transform.position;
        newAudioSource.gameObject.transform.parent = objectToPlayOn.gameObject.transform;

        newAudioSource.clip = clip;
        newAudioSource.Play();
        Destroy(newAudioSource.gameObject, newAudioSource.clip.length);
    }
        //for audio logs
        public void PlayAudioClipAtPoint(AudioClip clip, GameObject objectToPlayOn) {

        //StartCoroutine(PlayClipAtPoint(clip, objectToPlayOn));
		GameObject go = new GameObject("AudioSource");
		float randomPitch = Random.Range(-0.1f, 0.2f);
		go.AddComponent<AudioSource>();
		AudioSource newAudioSource = go.GetComponent<AudioSource>();
		newAudioSource.name = clip.name + '_' + objectToPlayOn.name;
		newAudioSource.transform.position = objectToPlayOn.transform.position;
		newAudioSource.gameObject.transform.parent = objectToPlayOn.gameObject.transform;

		newAudioSource.clip = clip;
		newAudioSource.Play();
		Invoke ("AudioClipCompleteCleanup", newAudioSource.clip.length);
		Destroy (newAudioSource.gameObject, newAudioSource.clip.length);
    }

    /*IEnumerator PlayClipAtPoint(AudioClip clip, GameObject objectToPlayOn) {
        GameObject go = new GameObject("AudioSource");
        float randomPitch = Random.Range(-0.1f, 0.2f);
        go.AddComponent<AudioSource>();
        AudioSource newAudioSource = go.GetComponent<AudioSource>();
        newAudioSource.name = clip.name + '_' + objectToPlayOn.name;
        newAudioSource.transform.position = objectToPlayOn.transform.position;
        newAudioSource.gameObject.transform.parent = objectToPlayOn.gameObject.transform;
        
        newAudioSource.clip = clip;
        newAudioSource.Play();
        while(newAudioSource.time <= newAudioSource.clip.length) {
            yield return null;
        }
        audioClipComplete();
        Destroy(newAudioSource.gameObject);
        
    }*/

    public void PlaySoundAtPoint(AudioClip clip, GameObject objectToPlayOn) {
        AudioSource.PlayClipAtPoint(clip, objectToPlayOn.transform.position);
    }
    
    public void PlaySoundAtPoint(AudioClip clip, GameObject objectToPlayOn, float pitch, float volume, float spatialBlend = 1f, bool loop = false, bool randPitch = false)
    {
        GameObject go = new GameObject("AudioSource");
        float randomPitch = Random.Range(-0.1f, 0.2f);
        go.AddComponent<AudioSource>();
        AudioSource newAudioSource = go.GetComponent<AudioSource>();
        newAudioSource.name = clip.name + '_' + objectToPlayOn.name;
        newAudioSource.transform.position = objectToPlayOn.transform.position;
        newAudioSource.gameObject.transform.parent = objectToPlayOn.gameObject.transform;
        newAudioSource.loop = loop;
        if (randPitch == true)
        {
            newAudioSource.pitch = pitch + randomPitch;
        }
        else
        {
            newAudioSource.pitch = pitch;
        }
        
        newAudioSource.volume = volume;
        newAudioSource.spatialBlend = spatialBlend;
        newAudioSource.clip = clip;
        newAudioSource.Play();

        if (loop == false)
            Destroy(newAudioSource.gameObject, newAudioSource.clip.length);
    }

    public void PlayAudioAmbient(AudioClip clip, float volume, bool loop = true, float fadeInTime = 0, bool crossfade = false, string nameOfSoundToFade = "")
    {
        GameObject go = new GameObject("AmbientAudioSource");
        go.AddComponent<AudioSource>();
        AudioSource newAudioSource = go.GetComponent<AudioSource>();
        newAudioSource.spatialBlend = 0;
        newAudioSource.name = clip.name + "_AmbientAudio";
        clipsPlaying.Add(newAudioSource.name, newAudioSource);

        newAudioSource.volume = 0;
        newAudioSource.clip = clip;
        newAudioSource.transform.position = gameObject.transform.position;
        newAudioSource.gameObject.transform.parent = gameObject.transform;
        newAudioSource.Play();
        if (crossfade == false)
        {
            StartCoroutine(FadeIn(newAudioSource, fadeInTime, volume));
        }
        else if (crossfade == true)
        {

            StartCoroutine(CrossFade(nameOfSoundToFade, newAudioSource, 5f, 1f));
        }
        if (loop == false)
            Destroy(newAudioSource.gameObject, newAudioSource.clip.length);


    }

    public void FadeAndPlayAudioOnObject(AudioClip clip, GameObject objectToPlayOn, float pitch, float volume, float fadeInTime, float spatialBlend = 1f, bool loop = false, bool randPitch = false)
    {

        GameObject go = new GameObject("AudioSource");
        go.AddComponent<AudioSource>();
        AudioSource newAudioSource = go.GetComponent<AudioSource>();
        newAudioSource.name = clip.name + '_' + objectToPlayOn.name;

        newAudioSource.volume = 0;
        newAudioSource.clip = clip;
        newAudioSource.transform.position = objectToPlayOn.transform.position;
        newAudioSource.gameObject.transform.parent = objectToPlayOn.gameObject.transform;
        newAudioSource.Play();
        StartCoroutine(FadeIn(newAudioSource, fadeInTime, volume));
    }

    public void StopSound(string nameOfSoundToStop)
    {
        GameObject soundToStopObject = GameObject.Find(nameOfSoundToStop);
        
        soundToStopObject.GetComponent<AudioSource>().Stop();
        Destroy(soundToStopObject);

    }

    public void StopSound(string nameOfSoundToStop, float fadeTime)
    {
        GameObject soundToStopObject = GameObject.Find(nameOfSoundToStop);
        StartCoroutine(FadeOut(nameOfSoundToStop, fadeTime));

    }

    
    #region Fading Coroutines
    IEnumerator CrossFade(string nameOfSoundToFade, AudioSource fadeInSource, float fadeTime, float volume)
    {
        GameObject fromObj = null;
        if (GameObject.Find(nameOfSoundToFade) != null)
        {
            fromObj = GameObject.Find(nameOfSoundToFade);
            fromObj.name += "_fading";
        }
        
        AudioSource fadeOutSource = fromObj.GetComponent<AudioSource>();

        while (fadeOutSource.volume > 0.01f)
        {
            fadeOutSource.volume -= Time.deltaTime / fadeTime;
            if(fadeInSource.volume < volume)
            {
                fadeInSource.volume += Time.deltaTime / fadeTime;
            }
            yield return null;
        }
    }

    IEnumerator FadeOut(string nameOfSound, float fadeTime)
    {
        GameObject soundToStopObject = GameObject.Find(nameOfSound);
        soundToStopObject.name += "_fadeOut";
        AudioSource curAudioSource = soundToStopObject.GetComponent<AudioSource>();
        while(curAudioSource.volume > 0.01f)
        {
            curAudioSource.volume -= Time.deltaTime / fadeTime;
            yield return null;

        }
        soundToStopObject.GetComponent<AudioSource>().Stop();
        Destroy(soundToStopObject);
    }

    IEnumerator FadeIn(AudioSource source, float fadeTime, float maxVolume)
    {
        AudioSource curAudioSource = source;

        while(curAudioSource.volume < maxVolume)
        {
            curAudioSource.volume += Time.deltaTime / fadeTime;
            yield return null;
        }
    }
    #endregion



}
