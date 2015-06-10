// Script for shaking the camera- used while the player is rolling.
// Created by Rowan Donaldson.

using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
	//Transform holding the current location of the camera.
	private Transform myTransform;

	//Timer used for determining how long the camera should continue shaking for.
	private float shake = 0.0f;
	//The amount that the camera should shake.
	public float shakeAmount = 0.5f;


	void Start () 
	{
		myTransform = gameObject.GetComponent<Transform>() as Transform;
	}

	void Update () 
	{
		Vector3 startPos = myTransform.localPosition;
		
		shake -= Time.deltaTime;

		if (shake > 0)
		{
			myTransform.localPosition = startPos + Random.insideUnitSphere * shakeAmount;	
		}
		else
		{
			myTransform.localScale = startPos;
		}
	}
	
	public void CamShake()
	{
		shake = 0.4f;
	}
}
