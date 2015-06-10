// This controls the audio that enemies play. By randomizing the pitch on creation, we can make a vast spectrum of noises from just a few audio clips.
// Created by Rowan Donaldson.

using UnityEngine;
using System.Collections;
[RequireComponent(typeof(AudioSource))]

public class EnemyAudio : MonoBehaviour
{
	//The sound to be played upon spawning.
	public AudioClip spawnSound;
	//The sound to be played when ranged enemies attack.
	public AudioClip enemyRanged;
	//The sound to be played when melee enemies bump into each other.
	public AudioClip[] enemyMelee;
	//The currently selected audio.
	private AudioSource myAudio;
	
	void Awake()
	{
		myAudio = GetComponent<AudioSource>(); 
	}

	void Start () 
	{
		if (Application.loadedLevelName == "GameScene")
		{
			myAudio.clip = spawnSound;
			myAudio.pitch = Random.Range (0.7f, 1.5f);
			myAudio.Play();
		}
	}
	
	public void Melee()
	{
		myAudio.clip = enemyMelee[Random.Range(0, enemyMelee.Length)];
		myAudio.volume = 0.5f;
		myAudio.Play();
	}
	
	public void Ranged()
	{
		myAudio.clip = enemyRanged;
		myAudio.volume = 4;
		myAudio.Play();
	}
}
