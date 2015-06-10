// Script for moving the camera in the results scene. Moves in a very long sway.
// Created by Rowan Donaldson.

using UnityEngine;
using System.Collections;

public class CamSway : MonoBehaviour
{
	//Bool used for determining which direction to move in.
	private bool myForward = true;

	void Update () 
	{			
		//Determine direction
		if (gameObject.transform.position.z > 290.0f)
		{
			myForward = true;
		}
		if (gameObject.transform.position.z < 160.0f)
		{
			myForward = false;
		}
	
		//And then move in that direction.
		if (myForward)
		{
			gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z - Time.deltaTime);
		}
		else if (!myForward)
		{
			gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + Time.deltaTime);
		}
	}
}
