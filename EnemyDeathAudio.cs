using UnityEngine;
using System.Collections;
[RequireComponent(typeof(AudioSource))]
public class EnemyDeathAudio : MonoBehaviour 
{
	private AudioSource audio;
	public AudioClip clip;

	void Start () 
	{
		if (Application.loadedLevelName == "GameScene")
		{
			audio = gameObject.GetComponent<AudioSource>();
		}

		if (audio != null)
		{
			if(!audio.isPlaying)
			{
				audio.pitch = Random.Range (0.7f, 1.5f);
				audio.Play ();
			}
		}
	}
}
