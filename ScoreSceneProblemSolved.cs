using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
[RequireComponent(typeof(EventSystem))]
public class ScoreSceneProblemSolved : MonoBehaviour // Makes it easier to select continue on the Score screen
{
	private EventSystem system;
	private float hiddenTimer = 1.0f;
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
		else
		if (hiddenTimer > 0)	
		{
			if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
			{
				//system.sendNavigationEvents = true;
				scenes.StartGame();
			}
		}
	}
}
