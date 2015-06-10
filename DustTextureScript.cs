// A script attached to each DustPrefab object- used for randomising the angle they spawn at, and destroying each dust sprite when it has existed for too long. 
// Created by Rowan Donaldson.

using UnityEngine;
using System.Collections;

public class DustTextureScript : MonoBehaviour
{	
	//The amount of time until the dust sprite is destroyed.
	private float timeOut = 0.5f;
	
	void Start()
	{
		gameObject.transform.rotation = Quaternion.Euler(90.0f, Random.Range(0.0f, 360.0f), 0.0f);
	}
	
	void Update()
	{
		timeOut -= Time.deltaTime;
		
		if (timeOut < 0)
		{
			Destroy(gameObject);
		}
	}
}
