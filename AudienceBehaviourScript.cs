// Used for making audience members face the player.
// Created by Rowan Donaldson.

using UnityEngine;
using System.Collections;

public class AudienceBehaviourScript : MonoBehaviour
{
	public Transform playerTarget;

	void Awake () 
	{
		if(Application.loadedLevelName == "GameScene")
		{
			playerTarget = GameObject.FindGameObjectWithTag("Player").transform;
		}
	}
	
	void Update () 
	{
		if (playerTarget != null)
		{
			//Look at player
			Vector3 targetPos = playerTarget.position;
			targetPos.y = gameObject.transform.position.y;
			gameObject.transform.LookAt(targetPos);
		}
		else if (playerTarget == null)
		{
			//Look at world center.
			Vector3 targetPos = Vector3.zero;
			targetPos.y = gameObject.transform.position.y;
			gameObject.transform.LookAt(targetPos);
		}
	}
}