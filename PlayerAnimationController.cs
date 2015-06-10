// Used for controlling player animations- basically the same as the animation controller, but with some custom components.
// Created by Rowan Donaldson.

using UnityEngine;
using System.Collections;
[RequireComponent(typeof(SpriteRenderer))]

public class PlayerAnimationController : MonoBehaviour
{
	//Whether to display the movement sprites or action sprites- false for movement, true for action.
	private bool dodge = false;

	//The renderer to display the animation.
	private SpriteRenderer myRenderer;

	//The sprite to display while idle.
	public Sprite staticSprite;
	//The animation to display while moving.
	public Sprite[] movementSprites;
	//The animation to display while dodging.
	public Sprite[] actionSprites;	

	//The index of the currently displayed sprite within its sprite array.
	private int spriteNumber;
	//The total number of sprites in the current sprite array being used for animating.
	private int spriteMax;
	//The total number of sprites in the movement sprite set.
	private int movementMax;
	//The total number of sprites in the dodging sprite set.
	private int actionMax;
	
	//The amount of time that each frame of the animation will be displayed.
	public float switchTimer = 0.1f;
	//Timer used for switching between sprites- upon reaching 0, the displayed sprite will be switched to the next one in the animation.
	private float switchTimerCurrent;
	
	[HideInInspector]
	//Whether or not the player is currently moving.
	public bool moving;
	[HideInInspector]
	//Whether or not this animation should be displayed or not.
	public bool visible = true;

	void Start () 
	{
		myRenderer = gameObject.GetComponent<SpriteRenderer>();
		spriteNumber = 0;
		switchTimerCurrent = switchTimer;
		
		movementMax = movementSprites.Length;
		actionMax = actionSprites.Length;
	}
	
	void Update () 
	{
		if (!visible)
		{
			myRenderer.enabled = false;
		}
		else
		{
			myRenderer.enabled = true;
		}
	
		if (moving)
		{
			if (!dodge)
			{
				spriteMax = movementMax;
				if (spriteNumber < spriteMax)
				{
					myRenderer.sprite = movementSprites [spriteNumber];
				}
			}
			else if (dodge)
			{
				spriteMax = actionMax;
				myRenderer.sprite = actionSprites[spriteNumber];
			}
		}
		else if (!moving)
		{
			myRenderer.sprite = staticSprite;
		}
			
		Timer();	
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
			dodge = false;
		}
	}
	
	public void Action()
	{
		spriteNumber = 0;
		dodge = true;
	}
	
	public void StopAction()
	{
		spriteNumber = 0;
		dodge = false;
	}
}
