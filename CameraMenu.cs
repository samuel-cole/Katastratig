// Camera behaviour for the Intro and Start Scenes.
// Created by Rowan Donaldson.

using UnityEngine;
using System.Collections;

public class CameraMenu : MonoBehaviour
{
	//The speed that the camera should travel at.
	public float speed = 0.2f;

	//Used for choosing which type of camera movement should be used.
	public int option = 2;

	//Reference to a cameramenu 2 script- this is used to stop the circling bit at the start of the start scene.
	public CameraMenu2 cam2;

	void Update () 
	{
		if (option == 1)
		{
			//Move down corridor, used for the intro scene.
			if (gameObject.transform.position.x > 0)
			{
				gameObject.transform.position = new Vector3(gameObject.transform.position.x-Time.deltaTime * speed, gameObject.transform.position.y, gameObject.transform.position.z);
			}	
		}
		
		if (option == 2)
		{
			//Move around the arena in a circle, used for the start of the start scene.
			if (gameObject.transform.eulerAngles.y < 355)
			{
				gameObject.transform.Rotate (Vector3.up * Time.deltaTime * speed);
			}
			else if (gameObject.transform.eulerAngles.y > 355)
			{
				cam2.Zoom();
			}
		}
	}
}
