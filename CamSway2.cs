// A smooth camera movement used during the Hotline Miami Easter Egg. 
// Created by Rowan Donaldson.

using UnityEngine;
using System.Collections;

public class CamSway2 : MonoBehaviour
{
	//The amount to rotate the camera.
	private float wibble = 0;
	//The direction the camera should be rotated in.
	private bool left = false;

	void Update () 
	{
		if (wibble > 1.5f)
		{
			left = false;
		}
		if (wibble < -1.5f)
		{
			left = true;
		}
		
		if (left)
		{
			wibble += 0.1f;
		}
		else if (!left)
		{
			wibble -= 0.1f;
		}
		gameObject.transform.Rotate (0, wibble *Time.deltaTime *2, 0);
	}
}