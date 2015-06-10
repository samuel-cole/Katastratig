// This script instantiates Dust Prefab objects. 
// Created by Rowan Donaldson.

using UnityEngine;
using System.Collections;

public class PlayerMakeDust : MonoBehaviour
{
	//The dust prefab to spawn.
	public GameObject dustPrefab;
	//The amount of time until the next dust prefab should be spawned.
	private float dustTimer;
	//Whether the player is currently rolling.
	private bool rolling = false;
	//The amount of time until the current roll ends.
	private float rollTimer;
	
	void Update()
	{
		rollTimer -= Time.deltaTime;
		dustTimer -= Time.deltaTime;
		
		if (rollTimer <= 0)
		{
			rolling = false;
		}
		else if (rollTimer > 0)
		{
			rolling = true;
		}
		
		if (rolling)
		{
			// Just a precaution to stop the player spawning too many dust gameobjects
			if (dustTimer < 0)																			
			{
				Instantiate(dustPrefab, gameObject.transform.position, Quaternion.Euler(90,0,0));
				dustTimer = 0.05f;
			}
		}
	}
		
	public void MakeDust()
	{
		if (!rolling)
		{
			Instantiate(dustPrefab, gameObject.transform.position, Quaternion.Euler(90,0,0));
			rollTimer = 0.4f;
			dustTimer = 0.05f;
		}
	}
}
