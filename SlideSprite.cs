// This script controls how the Start Screen menu backgrounds float slightly.
// Created by Rowan Donaldson.

using UnityEngine;
using System.Collections;

public class SlideSprite : MonoBehaviour 		
{
	//The maximum offset of the background in the x and y axes.
	public float max = 0.06f;
	//Value used to impact the speed of the movement- a higher number leads to slower movement. 
	public int speedMod = 30;

	
	void Update()
	{
		gameObject.transform.position = new Vector3(Mathf.PingPong(Time.fixedTime /speedMod, max), Mathf.PingPong(Time.fixedTime/speedMod, max), gameObject.transform.position.z);
	}
}
