// Used during the Hotline Miami Easter Egg. Moves the giant Lion Head around the arena after the Player.
// Created by Rowan Donaldson.

using UnityEngine;
using System.Collections;

public class LionHeadScript : MonoBehaviour 	
{
	//The object that the lion head should chase.
	public GameObject target;
	
	void Update () 
	{
		gameObject.transform.RotateAround(target.transform.position, Vector3.down, Time.deltaTime * 100);
		gameObject.transform.position = Vector3.Lerp (gameObject.transform.position, target.transform.position, Time.fixedDeltaTime * 0.5f);
	}
}
