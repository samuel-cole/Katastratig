using UnityEngine;
using System.Collections;
[RequireComponent(typeof(AudioSource))]
public class EnemyAudio : MonoBehaviour 
{
	public AudioClip spawnSound;
	public AudioClip deathSound;
	public AudioClip enemyRanged;
	public AudioClip[] enemyMelee;
	public AudioClip[] yells;

	private AudioSource audio;
	
	void Awake()
	{
		audio = GetComponent<AudioSource>(); 
	}

	void Start () 
	{
		if (Application.loadedLevelName == "GameScene")
		{
			audio.clip = spawnSound;
			audio.pitch = Random.Range (0.7f, 1.5f);
			audio.Play();
		}
	}
	
	public void Yell()
	{
		audio.clip = yells[Random.Range(0, yells.Length)];
		audio.Play();
	}
	
	public void Melee()
	{
		audio.clip = enemyMelee[Random.Range(0, enemyMelee.Length)];
		audio.Play();
	}
	
	public void Ranged()
	{
		audio.clip = enemyRanged;
		audio.volume = 4;
		audio.Play();
	}
	
	public void Die()
	{
		audio.clip = deathSound;
		audio.Play();
	}
}
