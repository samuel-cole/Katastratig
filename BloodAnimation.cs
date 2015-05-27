using UnityEngine;
using System.Collections;
[RequireComponent(typeof(SpriteRenderer))]
public class BloodAnimation : MonoBehaviour //spay blood everywhere!!!!			See Animation Controller script for further details
{

	private SpriteRenderer myRenderer;
	public Sprite[] bloodSprites;

	private float fixedTimer = 0.1f;

	private int spriteNumber;	// what is the number of the sprite i'm currently seeing
	private int spriteMax; 		// get the maximum number of sprites in the list

	void Start()
	{
		myRenderer = gameObject.GetComponent<SpriteRenderer>();

		spriteMax = bloodSprites.Length;

		//spriteNumber = Random.Range (0, spriteMax - 1);	Not as cool as it seems
		spriteNumber = 0;
	}

	void Update () 
	{
		myRenderer.sprite = bloodSprites [spriteNumber];

		Timer ();
	}

	void Timer()
	{
		fixedTimer -= Time.fixedDeltaTime;
		
		if (fixedTimer < 0)
		{
			spriteNumber ++;
			fixedTimer = 0.1f;
		}
		
		if (spriteNumber > spriteMax-1)
		{
			spriteNumber = 0;
		}
	}
}
