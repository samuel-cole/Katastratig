// Used for camera lerping in the game scene- used for getting the camera to follow the player.
// Created by Rowan Donaldson.

using UnityEngine;
using System.Collections;

public class CameraLerpFollow : MonoBehaviour 
{
	//The gameobject that the camera should follow (generally set to player).
	public GameObject camTarget;
	
	void FixedUpdate()
	{
		gameObject.transform.position = Vector3.Lerp (gameObject.transform.position, camTarget.transform.position, Time.fixedDeltaTime * 0.5f);
	}
}
