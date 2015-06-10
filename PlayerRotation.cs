// A custom script that controls the Player's rotation in world space.
// Created by Rowan Donaldson.

using UnityEngine;
using System.Collections;

public class PlayerRotation : MonoBehaviour 		
{
	//The angle that the player should be facing in degrees.
	private float angle;
	//The speed at which the player should turn.
	public float turnSpeed = 8.0f;

	//Whether the player is moving.
	private bool moving = false;
	//The players animation controller- used for getting whether the player is currently moving.
	private PlayerAnimationController animate;
	
	void Start()
	{
		animate = gameObject.GetComponent<PlayerAnimationController>();
	}
	
	void Update () 
	{
		// A precaution
		if (animate != null) 
		{
			animate.moving = moving;
		}
		
		var x = Input.GetAxis("GameHorizontal");
		var y = Input.GetAxis("GameVertical");
			
		if ((Mathf.Abs (y) > 0.15f) || (Mathf.Abs (x) > 0.15f))
		{
			//Get angle, convert to degrees.
			angle = Mathf.Atan2 (x,y)*(180/Mathf.PI);
			moving = true;
		}
		else
		{
			moving = false;
		}
	
		if (Mathf.Abs (x) > 0.15f)
		{
			if (x > 0.15f)
			{
				angle += turnSpeed/4;
			}
			
			if (x < 0.15f)
			{
				angle -= turnSpeed/4;
			}
		}		
	
		gameObject.transform.rotation = Quaternion.Lerp(gameObject.transform.rotation, Quaternion.Euler (new Vector3(90,angle,0)), Time.deltaTime * turnSpeed);
	}
}
