// Custom control for player audio. Ultimately, footsteps sounds weren't added to the project, so this script only handles roll noises.
// Created by Rowan Donaldson.

using UnityEngine;
using System.Collections;
[RequireComponent(typeof(AudioSource))]

public class PlayerAudio : MonoBehaviour 	
{
	//The audio source of the roll sound.
	private AudioSource myAudio;
	
	
	void Start () 
	{
		myAudio = gameObject.GetComponent<AudioSource>();
		myAudio.volume = 0.3f;
	}
	
	public void Roll()
	{
		if (!myAudio.isPlaying)
		{
			myAudio.volume = 0.3f;
			myAudio.Play();
		}
	}
}
