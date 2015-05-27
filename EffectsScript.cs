using UnityEngine;
using System.Collections;

public class EffectsScript : MonoBehaviour 		// This makes the hands special effect image change colour, alpha and rotate
{
	private GameObject camera;
	public GameObject child1;
	public GameObject child2;
	private SpriteRenderer childRenderer1;
	private SpriteRenderer childRenderer2;
	private float alphaFloat = 0;

	
	private bool thumbsUp = false;
	//private float anotherDamnTimer = 0;
	
	void Awake()
	{
		childRenderer1 = child1.GetComponent<SpriteRenderer>();
		childRenderer2 = child2.GetComponent<SpriteRenderer>();
		childRenderer1.color = new Color(1,1,1,0);
		childRenderer2.color = new Color(1,1,1,0);
	}

	void Start () 
	{
		camera = GameObject.Find("Camera");
		gameObject.transform.rotation = Quaternion.Lerp(gameObject.transform.rotation, Quaternion.Euler (new Vector3(180,0,0)), Time.deltaTime * 3);
	}
	
	
	void Update () 
	{	
		gameObject.transform.position = new Vector3 (camera.transform.position.x, camera.transform.position.y - 5, camera.transform.position.z-1.5f); //camera.transform.position;

		if (thumbsUp)
		{
			gameObject.transform.rotation = Quaternion.Lerp(gameObject.transform.rotation, Quaternion.Euler (new Vector3(0,0,0)), Time.deltaTime * 3);
			alphaFloat += 0.02f;
		}
		else
		if (!thumbsUp)
		{
			gameObject.transform.rotation = Quaternion.Lerp(gameObject.transform.rotation, Quaternion.Euler (new Vector3(180,0,0)), Time.deltaTime * 3);
			alphaFloat -= 0.01f;
		}
		/*
		if (!thumbsUp)
		{
			anotherDamnTimer -= Time.deltaTime;
		}
		
		if (anotherDamnTimer > 0)
		{
			alphaFloat -= 0.02f;
		}*/
		
		if (alphaFloat > 1.0f)
		{
			alphaFloat = 1.0f;
		}
		
		if (alphaFloat < 0.0f)
		{
			alphaFloat = 0.0f;
		}
		
		// Make it look good
		childRenderer1.color = new Color(1,1,1,alphaFloat);
		childRenderer2.color = new Color(1,1,1,alphaFloat);
	}

	public void Reveal()
	{
		thumbsUp = true;
	}
	
	public void DropScore()
	{
		thumbsUp = false;
		//anotherDamnTimer = 1.0f;
	}
}
