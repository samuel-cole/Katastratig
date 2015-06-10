// Fixes the canvas at start menu with mouse input
// Created by Rowan Donaldson.

using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
[RequireComponent(typeof(EventSystem))]

public class StartCanvasFix : MonoBehaviour 	
{
	//Eventsystem that controls player input.
	private EventSystem mySystem;
	//The button to set as the default.
	public GameObject defaultButton;

	void Start () 
	{
		mySystem = gameObject.GetComponent<EventSystem>();
	}
	
	void Update () 
	{		
		if (mySystem.currentSelectedGameObject == null)
		{
			mySystem.SetSelectedGameObject(defaultButton);
		}
	}
}
