// Controls how score is added during the play scene.
// Created by Samuel Cole, Rowan Donaldson and Sebastian Kitsakis.

using UnityEngine;
using System.Collections;

public class ScoreManager : MonoBehaviour 	
{
	//The player's current score.
	[HideInInspector]
	public int score;
	//The player's current multiplier.
	[HideInInspector]
	public int multiplierNumber;

	//The amount of time between score increments.
	public float scoreIncrementMax = 0.1f;
	//The amount of time until the next score increment.
	private float scoreIncrementTimer;

	//The amount of time after increasing the multiplier before it is reset.
	public float multiplierTimerMax = 2.0f;
	//The amount of time until the multiplier is reset.
	private float multiplierTimer;
	//Script that manages the multiplier HUD element.
	public MultiplierScript multiplierText;
	//Script that manages the thumbs that appear on-screen while the multiplier is up.
	public EffectsScript effects;
	//Whether or not the thumbs are currently being displayed on the HUD.
	private bool effectActive = false;

	void Start () 
	{
		score = 0;
		multiplierNumber = 1;
		multiplierTimer = multiplierTimerMax;
		scoreIncrementTimer = scoreIncrementMax;
	}
	
	void Update () 
	{
		scoreIncrementTimer -= Time.deltaTime;

		if (scoreIncrementTimer < 0)
		{
			scoreIncrementTimer = 0.1f;
			score = score + multiplierNumber;
		}
		
		if (multiplierTimer > 0)
		{
			multiplierTimer -= Time.deltaTime;	
		}
		
		if (multiplierTimer < 0)
		{	
			if (effectActive)
			{
				effects.DropScore();
				effectActive = false;
			}
			multiplierNumber = 1;
		}
		
		if (multiplierNumber < 1)
		{
			multiplierNumber = 1;
		}
		
		if (multiplierNumber > 4)
		{
			multiplierNumber = 4;
		}
	}

	//Increases the multiplier and triggers all of the associated HUD effects.
	public void IncreaseMultiplier()
	{
		++multiplierNumber;
		multiplierTimer = multiplierTimerMax;
		//Trigger a function on another script == this one makes the "X 4" text scale up and down
		multiplierText.Multiply(); 
		effects.Reveal();
		effectActive = true;
	}

	//Saves out score data to file.
	public void GameOver()
	{
		PlayerPrefs.SetInt("score", score);
		PlayerPrefs.SetFloat ("time", Time.timeSinceLevelLoad);
		int highScore = PlayerPrefs.GetInt("highscore");
		if (score > highScore)
		{
			PlayerPrefs.SetInt("highscore", score);
		}
		PlayerPrefs.Save();
	}
}
