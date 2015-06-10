// Used for proceeding from the intro screen to the menu, if either space is pressed or the intro ends. 
// Created by Rowan Donaldson.

using UnityEngine;
using System.Collections;

public class FinishIntroScript : MonoBehaviour 
{
	//The state manager used for switching scenes.
	public StateManager gameManager;

	void Update () 
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			gameManager.Menu();
		}

		if (gameObject.transform.position.x <= 0)
		{
			gameManager.Menu();
		}
	}
}
