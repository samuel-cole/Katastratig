using UnityEngine;
using System.Collections;
[RequireComponent(typeof(SpriteRenderer))]
public class PlayerAnimationController : MonoBehaviour // Basically the same as the animation controller, but with some custom components // Put On Player child object - PlayerAnimation
{
	private bool attack = false;

	private SpriteRenderer myRenderer;
	
	public Sprite staticSprite;
	public Sprite[] movementSprites;
	public Sprite[] actionSprites;	
	
	private int spriteNumber;
	private int spriteMax;
	private int movementMax;
	private int actionMax;
	
	private float switchTimer = 0.1f;
	
	[HideInInspector]
	public bool moving;

	void Start () 
	{
		myRenderer = gameObject.GetComponent<SpriteRenderer>();
		spriteNumber = 0;
		
		movementMax = movementSprites.Length;
		actionMax = actionSprites.Length;
	}
	
	void Update () 
	{
		if (moving)
		{
			if (!attack)
			{
				spriteMax = movementMax;
				myRenderer.sprite = movementSprites [spriteNumber];
			}
			else
			if (attack)
			{
				spriteMax = actionMax;
				myRenderer.sprite = actionSprites[spriteNumber];
			}
		}
		else
		if (!moving)
		{
			myRenderer.sprite = staticSprite;
		}
			
		Timer();	
	}
	
	void Timer()
	{
		switchTimer -= Time.deltaTime;
		
		if (switchTimer < 0)
		{
			spriteNumber ++;
			//switchTimer = 0.1f;
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
		//myRenderer.sprite = actionSprites[1];
		attack = true;
	}
	
	public void StopAction()
	{
		spriteNumber = 0;
		attack = false;
	}
}
