using UnityEngine;
using System.Collections;
using UnityEngine.UI;
[RequireComponent(typeof(Text))]
public class MultiplierScript : MonoBehaviour 
{
	public ScoreManager scoreManager;
	private Text text;
	private int myMultiplier;
	
	private float scale = 0.0f;
	private bool bump = false;
	private float internalTimer = 0.5f;	// how long should the text shape change??	

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
			text.text = (" "); // just easier this way
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
		
		text.color = new Color (manualColour, manualColour, manualColour);		// This is the easiest way of doing this!!!



		if (bump)
		{
			scale = 0.1f;
		}
		else
		if ((!bump) && (gameObject.transform.localScale.x > 1))
		{
			scale = -0.1f;
		}
		else
		if (gameObject.transform.localScale.x < 1)			// don't let it get to small
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
