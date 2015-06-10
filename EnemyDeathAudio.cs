// Plays a custom sound clip for the enemy death event. Can't be on the enemy object, as it cannot be played while the object is being destroyed.
// Created by Rowan Donaldson.

using UnityEngine;
using System.Collections;
[RequireComponent(typeof(AudioSource))]

public class EnemyDeathAudio : MonoBehaviour 		
{
	//The audiosource to play from.
	private AudioSource myAudio;
	//The clip to play.
	public AudioClip clip;

	void Start () 
	{
		if (Application.loadedLevelName == "GameScene")
		{
			myAudio = gameObject.GetComponent<AudioSource>();
		}

		if (myAudio != null)
		{
			if(!myAudio.isPlaying)
			{
				myAudio.clip = clip;
				myAudio.pitch = Random.Range (0.7f, 1.5f);
				myAudio.Play ();
			}
		}
	}
}
