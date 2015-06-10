// Camera behaviour for the Credits Scene.
// Created by Rowan Donaldson.

using UnityEngine;
using System.Collections;

public class CameraRotateScript : MonoBehaviour
{
	//The direction that the camera should rotate in.
	public bool backwards = false;

	void Update () 
	{
		if (!backwards)
	 	{
			gameObject.transform.Rotate(Vector3.forward * Time.deltaTime * 8.0f);
		}
		else
		if (backwards)
		{
			gameObject.transform.Rotate(Vector3.back * Time.deltaTime * 8.0f);
		}
	}
}
