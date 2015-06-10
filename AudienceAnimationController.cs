// Used for controlling the audience animations- stops the audience from playing the same animation on every frame.
// Created by Rowan Donaldson.

using UnityEngine;
using System.Collections;
[RequireComponent(typeof(SpriteRenderer))]

public class AudienceAnimationController : MonoBehaviour
{
	//Sprite renderer used to render the audience members.
	private SpriteRenderer myRenderer;

	// Arrays of sprites used for the animations that the audience has.
	// Keeping these naming conventions makes it easier to assign textures in the inspector - in reality, it shoud be called "CheerSprites1" and "CheerSprites2"
	public Sprite[] movementSprites;	
	public Sprite[] actionSprites;

	//Used for determining whether the attack animation or movement animation should be playing (once again, it should actually be two different type of cheering sprites).
	public bool attack = false;

	//The index of the currently displayed sprite within its sprite array.
	private int spriteNumber;
	//The total number of sprites in the current sprite array being used for animating.
	private int spriteMax;

	//The total number of sprites from one animation set.
	private int actionMax;
	//And the total number of sprites from the other animation set.
	private int movementMax;

	//The amount of time that each frame of the animation will be displayed.
	public float switchTimer = 0.1f;
	//Timer used for switching between sprites- upon reaching 0, the displayed sprite will be switched to the next one in the animation.
	private float switchTimerCurrent;


	void Start () 
	{
		//Choose an animation set to display.
		if (Random.Range(0, 4) < 2)
		{
			attack = false;
		}
		else
		{
			attack = true;
		}
	
		myRenderer = gameObject.GetComponent<SpriteRenderer>();
		actionMax = actionSprites.Length;
		movementMax = movementSprites.Length;

		switchTimerCurrent = switchTimer;
		
		if (attack)
		{
			spriteNumber = Random.Range(0, actionMax);
		}
		else
		if (!attack)
		{
			spriteNumber = Random.Range(0, movementMax);
		}
	}
	
	void Update () 
	{
		if (!attack)
		{	
			spriteMax = movementMax;
			myRenderer.sprite = movementSprites[spriteNumber];
		}
		else if (attack)
		{
			spriteMax = actionMax;
			myRenderer.sprite = actionSprites[spriteNumber];
		}
	
	
		Timer ();
	}
	
	void Timer()
	{
		switchTimerCurrent -= Time.deltaTime;
		
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
