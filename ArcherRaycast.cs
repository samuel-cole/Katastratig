using UnityEngine;
using System.Collections;
[RequireComponent(typeof(LineRenderer))]			// Remember to set all AIpoints to "Ignore Raycast" layer
public class ArcherRaycast : MonoBehaviour 
{
	private LineRenderer line;
	
	private RaycastHit hit;
	
	private bool timerDown;
	private float timer;
	public float Timer
	{
		get { return timer; }
	}
	
	private EnemyAudio enemyAudio;
	private GameObject parent;
	private AnimationController anim;
	private Color materialColor;
	private GameManager gameManager;

	void Start () 
	{	
		parent = gameObject.transform.parent.gameObject;
		enemyAudio = parent.GetComponent<EnemyAudio>();
		anim = parent.GetComponentInChildren<AnimationController>();
		gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
		
		line = gameObject.GetComponent<LineRenderer>();
		materialColor = line.material.color;
		materialColor.a = 0.0f;
		
		line.useWorldSpace = true;
		line.SetWidth(0.1f, 10.0f);
	}
	
	
	void Update () 
	{
		//Color materialColor = line.material.color;
		if (timerDown)
		{
		
			if (timer > 0)
			{
				timer -= Time.deltaTime;
				line.SetWidth(0.1f, timer*10);
				anim.Action();			// start attack animation
			}
			else
			{
				line.SetWidth(0.1f, 0.1f);
				enemyAudio.Ranged(); // make a noise!!
				Kill ();
			}
			
			materialColor.a += 0.02f;		// make the line render
		}
		else
		{
			timer = 1.0f;
			line.SetWidth(0.1f, 5.0f);
			materialColor.a = 0.0f;			// stop the line render
		}
			
		//materialColor.a = timer;
		
		if (materialColor.a < 0)		// lock the line render
		{
			materialColor.a = 0;
		}
		
		if (materialColor.a > 1.0f)
		{
			materialColor.a = 1.0f;
		}
			
		line.material.color = materialColor;
		
		///////////////////////////////////////////////////////////////
	
		Ray	ray = new Ray(gameObject.transform.position, gameObject.transform.parent.FindChild ("EnemyAnimation").TransformDirection(Vector3.up));
		
		if (Physics.Raycast(ray,out hit, 20))
		{
			//if (hit.collider.gameObject.CompareTag("Player"))
			{
				timerDown = true;
			}
		}
		else if (timer < 0.5f && timer > 0.0f)
		{
			timerDown = true;
		}
		else
		{
			timerDown = false;
		}
		
		line.SetPosition (0, gameObject.transform.position);
	
		if (hit.collider != null)
		{
			line.SetPosition (1, ray.GetPoint(20));
		}
		else
		{
			line.SetPosition (1, ray.GetPoint(20));
		}
	}
	
	void Kill()
	{
		if (hit.collider != null)
		{
			if (hit.collider.gameObject.CompareTag("Player"))
			{
				if (hit.collider.gameObject.GetComponent<Player>().RollTimeCurrent < 0.0f)
				{
					GameObject.FindGameObjectWithTag("GameController").GetComponent<StateManager>().Results();
				}
			}
			else
			{	
				gameManager.IncreaseMultiplier();
				GameObject.Destroy(hit.collider.gameObject);
			}
		}
		timerDown = false;
		//timer = 1.0f;
		anim.StopAction();	// stop the attack animation
	}
}
