using UnityEngine;
using System.Collections;

public class PlayerRotation : MonoBehaviour 
{
	private float angle;
	private float turnSpeed = 8.0f;
	
	private bool moving = false;
	private PlayerAnimationController animate;
	
	void Start()
	{
		animate = gameObject.GetComponent<PlayerAnimationController>();
	}
	
	void Update () 
	{
		if (animate != null)
		{
			animate.moving = moving;						// make sure this reference works!!!
		}
		
	
		var x = Input.GetAxis("MenuHorizontal");								// This would be better if it could reference the player movement directly, instead of Unity's input manager
		var y = Input.GetAxis("MenuVertical");
			
			if ((Mathf.Abs (y) > 0.15f) || (Mathf.Abs (x) > 0.15f))
			{
				angle = Mathf.Atan2 (x,y)*(180/Mathf.PI);
				moving = true;
			}
			else
			{
				moving = false;
			}
	
			if (Mathf.Abs (x) > 0.15f)
			{
				if (x > 0.15f)
				{
					angle += turnSpeed/4;
				}
				
				if (x < 0.15f)
				{
					angle -= turnSpeed/4;
				}
			}		
	
		gameObject.transform.rotation = Quaternion.Lerp(gameObject.transform.rotation, Quaternion.Euler (new Vector3(90,angle,0)), Time.deltaTime * turnSpeed);
	}
}
