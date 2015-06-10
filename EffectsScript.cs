// This controls the Thumbs that appear on the GUI.
// Created by Rowan Donaldson.

using UnityEngine;
using System.Collections;

public class EffectsScript : MonoBehaviour
{
	//The position of the camera to display the thumbs on.
	private Transform myCamera;

	//The hands to be passed in. While these aren't used directly during run-time, these are used as a way to pass in the renderers to be stored in childRenderer1 and childRenderer2.
	public GameObject child1;
	public GameObject child2;

	//The renderers of the hands. Used for modifying the alpha of the hands.
	private SpriteRenderer childRenderer1;
	private SpriteRenderer childRenderer2;

	//The alpha value that the hands should be displayed with.
	private float alphaFloat = 0;

	//The direction that the thumbs should lerp towards- true for up, false for down.
	private bool thumbsUp = false;
	
	void Awake()
	{
		childRenderer1 = child1.GetComponent<SpriteRenderer>();
		childRenderer2 = child2.GetComponent<SpriteRenderer>();
		childRenderer1.color = new Color(1,1,1,0);
		childRenderer2.color = new Color(1,1,1,0);
	}

	void Start () 
	{
		myCamera = GameObject.Find("Camera").transform;
		gameObject.transform.rotation = Quaternion.Lerp(gameObject.transform.rotation, Quaternion.Euler (new Vector3(180,0,0)), Time.deltaTime * 3);
	}
	
	
	void Update () 
	{	
		gameObject.transform.position = new Vector3 (myCamera.position.x, myCamera.position.y - 5, myCamera.position.z-1.5f); //myCamera.transform.position;

		if (thumbsUp)
		{
			gameObject.transform.rotation = Quaternion.Lerp(gameObject.transform.rotation, Quaternion.Euler (new Vector3(0,0,0)), Time.deltaTime * 3);
			alphaFloat += 0.02f;
		}
		else if (!thumbsUp)
		{
			gameObject.transform.rotation = Quaternion.Lerp(gameObject.transform.rotation, Quaternion.Euler (new Vector3(180,0,0)), Time.deltaTime * 3);
			alphaFloat -= 0.01f;
		}
	
		
		if (alphaFloat > 1.0f)
		{
			alphaFloat = 1.0f;
		}
		
		if (alphaFloat < 0.0f)
		{
			alphaFloat = 0.0f;
		}
		
		// Make it look good
		childRenderer1.color = new Color(1,1,1,alphaFloat);
		childRenderer2.color = new Color(1,1,1,alphaFloat);
	}

	public void Reveal()
	{
		thumbsUp = true;
	}
	
	public void DropScore()
	{
		thumbsUp = false;
	}
}
