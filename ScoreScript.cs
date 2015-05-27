using UnityEngine;
using System.Collections;
using UnityEngine.UI;
[RequireComponent(typeof(Text))]
public class ScoreScript : MonoBehaviour 
{
	public ScoreManager scoreManager;
	public EffectsScript effects;
	
	private Text text;
	
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
