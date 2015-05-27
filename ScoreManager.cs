using UnityEngine;
using System.Collections;

public class ScoreManager : MonoBehaviour 
{
	[HideInInspector]
	public int score;
	[HideInInspector]
	public int multiplierNumber;
	[HideInInspector]
	public bool multiplierOn = false;
	
	private float internalTimer;
	private float multiplierTimer = 2.0f;
	public MultiplierScript multiplierText;
	public EffectsScript effects;
	private bool effectActive = false;

	void Start () 
	{
		score = 0;
		multiplierNumber = 1;
		internalTimer = 1.0f;
	}
	
	void Update () 
	{
		internalTimer -= Time.deltaTime;
		
		
		
		if (internalTimer < 0)
		{
			internalTimer = 1.0f;
			//score ++;
			score = score + (1*multiplierNumber);
		}
		
		if (multiplierTimer > 0)
		{
			multiplierTimer -= Time.deltaTime;	
		}
		
		if (multiplierTimer < 0)
		{	
			if (effectActive)
			{
				effects.DropScore();										//Here's the problem
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
	
	public void DoubleScore()
	{
		multiplierNumber ++ ;
		multiplierTimer = 2.0f;
		multiplierText.Multiply(); // trigger a function on another script == this one makes the "X 4" text scale up and down
		effects.Reveal();
		effectActive = true;
	}

	public void GameOver()
	{
		PlayerPrefs.SetInt("score", score);
		int highScore = PlayerPrefs.GetInt("highscore");
		if (score > highScore)
		{
			PlayerPrefs.SetInt("highscore", score);
		}
		PlayerPrefs.Save();
	}
}
