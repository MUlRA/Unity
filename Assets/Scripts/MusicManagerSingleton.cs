using UnityEngine;
using System.Collections;

[RequireComponent (typeof (AudioSource))]
public class MusicManagerSingleton : MonoBehaviour
{
	private static MusicManagerSingleton instance = null;
	public static MusicManagerSingleton Instance
	{
		get { return instance; }
	}
	void Awake()
	{
		if (instance != null && instance != this)
		{
			instance.gameObject.SendMessage("SetMusic",GetComponent<AudioSource>().clip);
			Destroy(this.gameObject);
			return;
		}
		else
		{
			instance = this;
		}
		DontDestroyOnLoad(this.gameObject);
		gameObject.name = "$MusicManager";
	}
	
	void SetMusic(AudioClip newClip)
	{
		if(newClip != null)
		{
			if(GetComponent<AudioSource>().clip != newClip)
			{
				GetComponent<AudioSource>().clip = newClip;
				GetComponent<AudioSource>().Play();
			}
		}
		else
		{
			GetComponent<AudioSource>().Stop();
			GetComponent<AudioSource>().clip = null;
		}
	}
	
	public void DuckMusic(float volume, float time, float fadeInDuration, float fadeOutDuration)
	{
		StartCoroutine(ActuallyDuckMusic(volume, time, fadeInDuration, fadeOutDuration));
	}
	
	IEnumerator ActuallyDuckMusic(float volume, float time, float fadeInDuration, float fadeOutDuration)
	{
		//Variables
		float t = 0.0f;
		float initialVolume = GetComponent<AudioSource>().volume;
		
		//Fade in
		while(t < fadeInDuration)
		{
			GetComponent<AudioSource>().volume = Mathf.Lerp(initialVolume, volume, t/fadeInDuration);
			t += Time.deltaTime;
			yield return null;
		}
		
		//Stay at target volume
		GetComponent<AudioSource>().volume = volume;
		yield return new WaitForSeconds(time);
		
		//Fade out
		t = 0;
		while(t < fadeOutDuration)
		{
			GetComponent<AudioSource>().volume = Mathf.Lerp(volume, initialVolume, t/fadeOutDuration);
			t += Time.deltaTime;
			yield return null;
		}
		GetComponent<AudioSource>().volume = initialVolume;
	}
}