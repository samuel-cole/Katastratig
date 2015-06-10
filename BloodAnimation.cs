// Used for creating a blood splatter animation.
// Created by Rowan Donaldson.

using UnityEngine;
using System.Collections;
[RequireComponent(typeof(SpriteRenderer))]

public class BloodAnimation : MonoBehaviour
{
	//The renderer to be used to display this animation.
	private SpriteRenderer myRenderer;
	//Array containing all of the sprites used for the animation.
	public Sprite[] bloodSprites;

	//The amount of time that each frame of the animation will be displayed.
	public float switchTimer = 0.1f;
	//Timer used for switching between sprites- upon reaching 0, the displayed sprite will be switched to the next one in the animation.
	private float switchTimerCurrent;

	//Index of the currently displayed sprite within the 'bloodSprites' array.
	private int spriteNumber;	
	//The length of the bloodSprites array.
	private int spriteMax; 		

	void Start()
	{
		myRenderer = gameObject.GetComponent<SpriteRenderer>();

		spriteMax = bloodSprites.Length;

		switchTimerCurrent = switchTimer;

		spriteNumber = 0;
	}

	void Update () 
	{
		myRenderer.sprite = bloodSprites [spriteNumber];

		Timer ();
	}

	void Timer()
	{
		switchTimerCurrent -= Time.fixedDeltaTime;
		
		if (switchTimerCurrent < 0)
		{
			++spriteNumber;
			switchTimerCurrent = switchTimer;
		}
		
		if (spriteNumber > spriteMax-1)
		{
			spriteNumber = 0;
		}
	}
}
