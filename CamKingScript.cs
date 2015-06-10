// Script for activating a hidden easter egg in reference to our influence, Hotline Miami. Press 'H' to activate.
// Created by Rowan Donaldson.

using UnityEngine;
using System.Collections;

public class CamKingScript : MonoBehaviour 
{
	//The normal camera- gets disabled when the easter egg is activated.
	public Camera myCamera;
	//The special camera that only gets activated when the easter egg is turned on.
	public Camera hotline;
	//Easter egg music.
	public AudioSource deepCover;
	//Easter egg lion sprite.
	public SpriteRenderer lion;

	void Start () 
	{
		myCamera.enabled = true;
		hotline.enabled = false;
		lion.enabled = false;
	}
	
	void Update ()
	{	
		if (Input.GetKeyDown(KeyCode.H))
		{
			gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y+50, gameObject.transform.position.z);
			myCamera.enabled = false;
			hotline.enabled = true;
			
			if(deepCover.isPlaying == false)
			{
				deepCover.Play ();
				lion.enabled = true;
			}
		}
	}
}
