// Controls the multiplier HUD element.
// Created by Rowan Donaldson.

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
[RequireComponent(typeof(Text))]

public class MultiplierScript : MonoBehaviour 		
{	
	//The script that will determine the scores.
	public ScoreManager scoreManager;

	//The HUD text for the multiplier.
	private Text text;
	//The value of the multiplier.
	private int myMultiplier;

	//The amount to increase/decrease the scale of the text.
	private float scale = 0.0f;
	//Whether the text should currently be increasing in scale or not. Set to true for increasing.
	private bool bump = false;

	//The colour that the text should be set to (the same value is used for R, G and B values).
	private float manualColour = 1;


	void Start () 
	{
		text = gameObject.GetComponent<Text>();
	}
	
	void Update () 
	{
		if (scoreManager != null)
		{
			myMultiplier = scoreManager.multiplierNumber;
		}

		// Ways to render the text
		if (myMultiplier > 1)
		{
			text.text = ("x" + myMultiplier);
		}
		else
		{
			text.text = (" ");
		}
		
		
		manualColour -= Time.deltaTime;
		
		if (manualColour > 1)
		{
			manualColour = 1;
		}
		
		if (manualColour < 0)
		{
			manualColour = 0;
		}
		
		text.color = new Color (manualColour, manualColour, manualColour);



		if (bump)
		{
			scale = 0.1f;
		}
		else if (gameObject.transform.localScale.x > 1)
		{
			scale = -0.1f;
		}
		//Don't let it get too small.
		else if (gameObject.transform.localScale.x < 1)			
		{
			scale = 0.0f;
		}
		
		if (gameObject.transform.localScale.x > 2)
		{
			bump = false;
		}
		
		gameObject.transform.localScale += new Vector3(scale, scale, scale);
	}
	
	public void Multiply()
	{
		if (!bump)
		{
			manualColour = 1.0f;
			bump = true;
		}
	}
}
