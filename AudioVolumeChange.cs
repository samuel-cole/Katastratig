// Used for making the audience rise in volume.
// Created by Rowan Donaldson.

using UnityEngine;
using System.Collections;
[RequireComponent(typeof(AudioSource))]

public class AudioVolumeChange : MonoBehaviour 
{
	//The audio file used to represent the audience noises.
	private AudioSource myAudio;
	//The volume at which the audio should be played.
	private float noise;

	void Start () 
	{
		myAudio = gameObject.GetComponent<AudioSource>();
		myAudio.volume = 0;
	}
	
	void Update () 
	{
		noise = Mathf.Lerp (0, 1, Time.time /5); 
		myAudio.volume = noise;
	}
}
