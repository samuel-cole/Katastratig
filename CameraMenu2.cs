// Camera behaviour for the end of the Start Scene. Causes the camera to stop circling around the arena and focus on the menu.
// Created by Rowan Donaldson.

using UnityEngine;
using System.Collections;
[RequireComponent(typeof(Camera))]

public class CameraMenu2 : MonoBehaviour 
{
	//The camera to be moved around.
	private Camera myCam;
	//Value used for rotating the camera.
	private float rotateNumber = 0;
	//What the camera should be doing at the moment- false for circling around the arena, true for zooming in to the start menu.
	private bool zoomIn = false;
	//An object that holds the transform that this camera should end up at after moving.
	public GameObject finalTarget;

	void Start () 
	{
		myCam = gameObject.GetComponent<Camera>();
		myCam.fieldOfView = 5;
	}
	
	
	void Update () 
	{
		if (myCam.fieldOfView < 60)
		{
			myCam.fieldOfView += 2;
		}
		
		if (rotateNumber < 30)
		{
			rotateNumber += 0.1f;
		}
		
		if(!zoomIn)
		{
			gameObject.transform.localEulerAngles = new Vector3(rotateNumber,180,0); // Make the camera change focus and rotation
		}
		
		if (zoomIn)
		{
			gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, finalTarget.transform.position, 0.8f*Time.deltaTime);
			gameObject.transform.rotation = Quaternion.Lerp(gameObject.transform.rotation, finalTarget.transform.rotation, 0.8f*Time.deltaTime);
		}
	}
	
	public void Zoom()
	{
		if (!zoomIn)
		{
			zoomIn = true;
		}
	}
}
