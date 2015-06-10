// This script randomly changes the sprite texture when an UrnPrefab is spawned. Just for variety and juice.
// Created by Rowan Donaldson.

using UnityEngine;
using System.Collections;
[RequireComponent(typeof(SpriteRenderer))]

public class UrnSpriteScript : MonoBehaviour 	
{
	//List of sprites that could be selected from.
	public Sprite[] urnSprites;

	void Start () 
	{
		gameObject.GetComponent<SpriteRenderer>().sprite = urnSprites[Random.Range(0, urnSprites.Length - 1)];
	}
	
}
