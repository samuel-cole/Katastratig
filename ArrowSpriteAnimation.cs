// Used for controlling the arrow projectile sprite- note that arrows are entirely cosmetic, and that ranged enemies actually kill with a raycast before firing an arrow.
// Created by Rowan Donaldson.

using UnityEngine;
using System.Collections;

public class ArrowSpriteAnimation : MonoBehaviour 
{
	//The speed at which this arrow should move.
	public float speed = 2.4f;

	//The amount of time that this arrow should exist for before deleting itself.
	public float killTimer = 3.0f;

	void Start () 
	{
		gameObject.transform.position = new Vector3(gameObject.transform.position.x, 0.2f, gameObject.transform.position.z);
	}
	
	void Update () 
	{
		killTimer -= Time.deltaTime;
		
		if (killTimer < 0)
		{
			Destroy(gameObject);
		}
	
		gameObject.transform.Translate(Vector3.up * speed);	
	}
}
