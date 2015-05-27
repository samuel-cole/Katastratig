using UnityEngine;
using System.Collections;

public class ArrowSpriteAnimation : MonoBehaviour 
{
	private Vector3 myForward;
	private bool shoot = true;
	private float speed = 1.2f;
	
	private float killTimer = 3.0f;

	void Start () 
	{
		gameObject.transform.position = new Vector3(gameObject.transform.position.x, 0.2f, gameObject.transform.position.z);
	}
	
	void Update () 
	{
		killTimer -= Time.deltaTime;
		
		if (killTimer < 0)
		{
			Destroy(gameObject);
		}
	
		if (shoot)
		{
			gameObject.transform.Translate(Vector3.up * speed);	
		}
	}
	
	void Shoot()
	{
		if (!shoot)
		{
			shoot = true;
		}
	}
}
