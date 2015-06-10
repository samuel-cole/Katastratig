// Used for creating a static blood splatter. Also manages some score elements related to dead enemies.
// Created by Rowan Donaldson.

using UnityEngine;
using System.Collections;
[RequireComponent(typeof(SpriteRenderer))]

public class BloodScript : MonoBehaviour
{
	//Array of sprites used for blood- one of these will be selected randomly for this blood splatter.
	public Sprite[] bloodSprites;
	
	void Start () 
	{
		gameObject.transform.position = new Vector3(gameObject.transform.position.x, 0.11f, gameObject.transform.position.z);
		gameObject.transform.rotation = Quaternion.Euler(90, Random.Range(0.0f, 360.0f), 0);
		gameObject.GetComponent<SpriteRenderer>().sprite = bloodSprites[Random.Range(0, bloodSprites.Length)];
		
		GameObject sceneManager = GameObject.Find("SceneManager");
		ScoreManager score = null;

		if (sceneManager != null)
		{	
			score = sceneManager.GetComponent<ScoreManager>();
		}
		
		if (score != null)
		{
			score.IncreaseMultiplier();
		}
	}
}
