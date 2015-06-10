// Controls basic animaitons. Requires two sets of sprites. 
// Created by Rowan Donaldson.

using UnityEngine;
using System.Collections;
[RequireComponent(typeof(SpriteRenderer))]

public class AnimationController : MonoBehaviour 	
{
	//Whether to display the attacking animation or the standard movement one. True for attacking.
	public bool attack = false;

	//The sprite renderer that will render this animation.
	private SpriteRenderer myRenderer;

	//An array containing all of the sprites that this animation will use for normal movement.
	public Sprite[] movementSprites;
	//An array containing all of the sprites that this animation will use while attacking.
	public Sprite[] actionSprites; 

	//The index of the current sprite within the array of sprites.
	private int spriteNumber;	
	//The total number of sprites in the array currently being used- will always equal either actionMax or movementMax.
	private int spriteMax; 		

	//The total number of sprites in the 'movementSprites' array.
	private int movementMax;
	//The total number of sprites in the 'actionSprites' array.
	private int actionMax;

	//The amount of time that each frame of the animation will be displayed.
	public float switchTimer = 0.1f;
	//Timer used for switching between sprites- upon reaching 0, the displayed sprite will be switched to the next one in the animation.
	private float switchTimerCurrent;


	void Start () 
	{
		myRenderer = gameObject.GetComponent<SpriteRenderer>();

		spriteNumber = 0;
		actionMax = actionSprites.Length;
		movementMax = movementSprites.Length;

		switchTimerCurrent = switchTimer;
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
		Timer();
	}
	
	void Timer()
	{
		switchTimerCurrent -= Time.deltaTime;
		
		if (switchTimerCurrent < 0)
		{
			++spriteNumber;
			switchTimerCurrent = 0.1f;
		}
		
		if (spriteNumber > spriteMax - 1)
		{
			spriteNumber = 0;
			attack = false;
		}
	}
	
	public void Action()
	{
		attack = true;
	}
	
	public void StopAction()
	{
		spriteNumber = 0;
		attack = false;
	}
}
