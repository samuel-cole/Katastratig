using UnityEngine;
using System.Collections;
[RequireComponent(typeof(AudioSource))]
public class PlayerAudio : MonoBehaviour 
{
	private AudioSource audio;
	
	
	void Start () 
	{
		audio = gameObject.GetComponent<AudioSource>();
		audio.volume = 0.3f;
	}
	
	public void Roll()
	{
		if (!audio.isPlaying)
		{
			audio.volume = 0.3f;
			audio.Play();
		}
	}
}
