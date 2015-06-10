// Controls the GUI score text.
// Created by Rowan Donaldson.

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
[RequireComponent(typeof(Text))]

public class ScoreScript : MonoBehaviour 		
{
	//Scoremanager that controls the score.
	public ScoreManager scoreManager;
	//The GUI text to modify.
	private Text text;
	//The score to change the text to.
	private int myScore = 333;

	void Start () 
	{
		text = gameObject.GetComponent<Text>();
	}
	

	void Update () 
	{		
		if (scoreManager != null)
		{
			myScore = scoreManager.score;
		}
		
		text.text = ("Score: " + myScore);
		text.color = new Color (0,0,0);
	}
}
