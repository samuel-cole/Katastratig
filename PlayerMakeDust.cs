using UnityEngine;
using System.Collections;

public class PlayerMakeDust : MonoBehaviour 
{
	public GameObject dustPrefab;
	private float dustTimer;
	private bool rolling = false;
	private float rollTimer;
	
	void Update()
	{
		rollTimer -= Time.deltaTime;
		dustTimer -= Time.deltaTime;
		
		if (rollTimer <= 0)
		{
			rolling = false;
		}
		else
		if (rollTimer > 0)
		{
			rolling = true;
		}
		
		if (rolling)
		{
			if (dustTimer < 0)																			// Just a precaution to stop the player spawning too many dust gameobjects
			{
				Instantiate(dustPrefab, gameObject.transform.position, Quaternion.Euler(90,0,0));
				dustTimer = 0.05f;
			}
		}
		/*
		if (Input.GetKeyDown(KeyCode.Space))
		{
			MakeDust();
		}
		*/		//Testing purposes
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
