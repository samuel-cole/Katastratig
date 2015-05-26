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

	private AudioSource enemyAudio;
	
	void Awake()
	{
		enemyAudio = GetComponent<AudioSource>(); 
	}

	void Start () 
	{
		if (Application.loadedLevelName == "GameScene")
		{
			enemyAudio.clip = spawnSound;
			enemyAudio.pitch = Random.Range (0.7f, 1.5f);
			enemyAudio.Play();
		}
	}
	
	public void Yell()
	{
		enemyAudio.clip = yells[Random.Range(0, yells.Length)];
		enemyAudio.Play();
	}
	
	public void Melee()
	{
		enemyAudio.clip = enemyMelee[Random.Range(0, enemyMelee.Length)];
		enemyAudio.Play();
	}
	
	public void Ranged()
	{
		enemyAudio.clip = enemyRanged;
		enemyAudio.Play();
	}
	
	public void Die()
	{
		enemyAudio.clip = deathSound;
		enemyAudio.Play();
	}
}
