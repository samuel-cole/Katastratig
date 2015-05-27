using UnityEngine;
using System.Collections;
[RequireComponent(typeof(SpriteRenderer))]
public class AnimationController : MonoBehaviour 
{
	public bool attack = false;
	
	private SpriteRenderer myRenderer;

	public Sprite[] movementSprites;
	public Sprite[] actionSprites; 
	
	private int spriteNumber;	// what is the number of the sprite i'm vurrently seeing
	private int spriteMax; 		// get the maximum number of sprites in the list
	
	private int actionMax;
	private int movementMax;

	private float switchTimer = 0.1f;


	void Start () 
	{
		myRenderer = gameObject.GetComponent<SpriteRenderer>();

		spriteNumber = 0;
		actionMax = actionSprites.Length;
		movementMax = movementSprites.Length;
	}
	
	
	void Update () 
	{		
		if (!attack)
		{	
			spriteMax = movementMax;
			myRenderer.sprite = movementSprites[spriteNumber];
		}
		else
		if (attack)
		{
			spriteMax = actionMax;
			myRenderer.sprite = actionSprites[spriteNumber];
		}
		
		Timer();

	}
	
	void Timer()
	{
		switchTimer -= Time.deltaTime;
		
		if (switchTimer < 0)
		{
			// do stuff here
			spriteNumber ++;
			switchTimer = 0.1f;
		}
		
		if (spriteNumber > spriteMax-1)
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
		//attack = false;
		spriteNumber = 0;
		attack = false;
	}
}
