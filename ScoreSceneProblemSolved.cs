// Makes it easier to select continue on the Score screen by delaying the time before the GUI event manager becomes active.
// Created by Rowan Donaldson.

using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
[RequireComponent(typeof(EventSystem))]

public class ScoreSceneProblemSolved : MonoBehaviour 
{
	//The events system that handles player input.
	private EventSystem system;
	//Timer that handles how long the game should wait before allowing the player to control the menu.
	private float hiddenTimer = 1.0f;
	//Statemanager used for getting into the game if the player pushes the space button before normal control is granted to them (for the impatient players).
	public StateManager scenes;
	
	void Start () 
	{
		system = gameObject.GetComponent<EventSystem>();
		system.sendNavigationEvents = false;
	}
	
	void Update () 
	{
		hiddenTimer -= Time.deltaTime;
		
		if (hiddenTimer <= 0)
		{
			system.sendNavigationEvents = true;
		}
		else if (hiddenTimer > 0)	
		{
			if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
			{
				scenes.StartGame();
			}
		}
	}
}
