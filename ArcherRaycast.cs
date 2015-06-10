// Attached to ranged enemies. Controls arrows and shooting. 
// Created by Samuel Cole and Rowan Donaldson.

using UnityEngine;
using System.Collections;
[RequireComponent(typeof(LineRenderer))]	

public class ArcherRaycast : MonoBehaviour 			
{					
	//Renderer used to display the line indicating where the shot is being aimed.
	private LineRenderer line;

	//RaycastHit used for detecting what the shot has/will hit.
	private RaycastHit hit;

	//Bool used for determining whether the shot should be being charged up or not- set to true for charging, and false for not charging.
	private bool shotCharging;
	//Accessor/Mutator property for shotCharging.
	private bool ShotCharging
	{
		get { return shotCharging; }
		set
		{
			if (!value && shotCharging)
			{
				//If the shot was, but no longer is, charging, reset the timer and stop rendering the line.
				chargeTime = chargeTimeMax;
				line.SetWidth(0.1f, 5.0f);
				materialColor.a = 0.0f;
			}
			shotCharging = value;
		}
	}
	//Stores the amount of time that a shot has to be charged for before it is fired.
	public float chargeTimeMax = 1.0f;
	//Timer used for determining whether the current shot is charged up enough to fire- counts downwards (fires at 0).
	private float chargeTime;
	//Accessor property for chargeTime.
	public float ChargeTime
	{
		get { return chargeTime; }
	}

	//Sound played upon firing an arrow.
	private EnemyAudio enemyAudio;
	//The animation controller controlling this archers animations- used for setting whether to display the attacking animation or the default movement one.
	private AnimationController anim;
	//Color used for setting the colour of the line that is displayed to show where the shot will go.
	private Color materialColor;
	
	//The arrow object to spawn when firing.
	public GameObject arrowPrefab;
	//The point and direction at which the arrow should be spawned.
	public GameObject arrowPoint;
	//Whether an arrow should be created upon firing or not.
	private bool gotArrow = false;
	//Used for toggling whether arrows are visible or not.
	public ArrowProxyScript proxy;
	
	//Blood prefab- spawned on the bodies of killed enemies.
	public GameObject bloodPrefab;
	//Body melee prefab- spawned on the bodies of killed melee enemies.
	public GameObject bodyMeleePrefab;
	//Body ranged prefab- spawned on the bodies of killed ranged enemies.
	public GameObject bodyRangedPrefab;

	void Start () 
	{	
		GameObject parent = gameObject.transform.parent.gameObject;
		enemyAudio = parent.GetComponent<EnemyAudio>();
		anim = parent.GetComponentInChildren<AnimationController>();
		
		line = gameObject.GetComponent<LineRenderer>();
		materialColor = line.material.color;
		materialColor.a = 0.0f;
		
		line.useWorldSpace = true;
		line.SetWidth(0.1f, 10.0f);

		ShotCharging = false;
		chargeTime = chargeTimeMax;
		
		if (proxy != null)
		{
			proxy.Invisible();
		}
	}
	
	
	void Update () 
	{
		if (ShotCharging)
		{
			if (chargeTime > 0)
			{
				//Still charging up.
				chargeTime -= Time.deltaTime;
				line.SetWidth(0.1f, chargeTime * 10.0f);
				anim.Action();
			}
			else
			{
				//Firing!
				line.SetWidth(0.1f, 0.1f);
				enemyAudio.Ranged();
				Kill ();
			}

			//Make the line render.
			materialColor.a += 0.02f;		
		}
			
		line.material.color = materialColor;
		

		//Check to see if the player is within line of sight.
		Ray	ray = new Ray(gameObject.transform.position, gameObject.transform.parent.FindChild ("EnemyAnimation").TransformDirection(Vector3.up));	
		if (Physics.Raycast(ray,out hit, 20))
		{
			if (proxy != null)
			{
				proxy.Visible();
			}
			
			if (hit.collider.gameObject.CompareTag("Player"))
			{
				ShotCharging = true;
			}
			else 
			{
				//Check for the case where the player is within the ray, however the ray is cut short by enemies being in front of the ray first.
				float distanceRemaining = 20 - hit.distance;
				while (distanceRemaining > 0.1f)
				{
					//Create a new ray from the point of collision + 1 unit in the direction being headed in.
					Ray newRay = new Ray(hit.point + ray.direction, gameObject.transform.parent.FindChild("EnemyAnimation").TransformDirection(Vector3.up));
					RaycastHit newHit;
					if (Physics.Raycast (newRay, out newHit, distanceRemaining))
					{
						if (newHit.collider.gameObject.CompareTag("Player"))
						{
							ShotCharging = true;
							break;
						}
						else
						{
							distanceRemaining -= newHit.distance;
						}
					}
					else
					{
						break;
					}
				}
			}
		}
		//If charging has gone far enough, continue to charge up even if the player is no longer in sight (makes the game easier).
		else if (chargeTime < chargeTimeMax * 0.5f && chargeTime > 0.0f)
		{
			if (proxy != null)
			{
				proxy.Visible();
			}
			ShotCharging = true;
		}
		//Stop charging, player not in sight and not charged enough to continue anyway.
		else
		{
			if (proxy != null)
			{
				proxy.Invisible();
			}
			gotArrow = false;
		
			ShotCharging = false;
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

	//This function is called when the arrow should be released.
	void Kill()
	{
		// Spawn an arrow
		if (!gotArrow)
		{
			Instantiate(arrowPrefab, arrowPoint.transform.position, Quaternion.Euler(90, arrowPoint.transform.rotation.eulerAngles.y, arrowPoint.transform.rotation.eulerAngles.z)); //This spawns the arrow object FAR infront of the actual ranged enemy object, but it looks appropriate. 
			gotArrow = true;
		}
		
		
		//If there is something in the path of the arrow to kill, then kill it.
		if (hit.collider != null)
		{
			GameObject target =  hit.collider.gameObject;

			if (target.CompareTag("Player"))
			{		
				if (target.GetComponent<Player>().RollTimeCurrent < 0.0f)
				{
					GameObject.FindGameObjectWithTag("GameController").GetComponent<StateManager>().Results();
				}
			}
			else if (target.CompareTag("Enemy") || target.CompareTag("EnemyTrigger"))
			{		
				if (target.CompareTag("EnemyTrigger"))
				{
					target = target.transform.parent.gameObject;
				}
				if (target.GetComponent<EnemyRanged>())
				{
					Instantiate(bodyRangedPrefab, new Vector3(target.transform.position.x, 0.1f, target.transform.position.z), Quaternion.Euler(90, target.transform.rotation.eulerAngles.y, target.transform.rotation.eulerAngles.z));
				}
				else if (target.GetComponent<EnemyMelee>())
				{
					Instantiate(bodyMeleePrefab, new Vector3(target.transform.position.x, 0.1f, target.transform.position.z), Quaternion.Euler(90, target.transform.rotation.eulerAngles.y, target.transform.rotation.eulerAngles.z));
				}
				
				Instantiate(bloodPrefab, target.transform.position, Quaternion.Euler(90, target.transform.rotation.eulerAngles.y, target.transform.rotation.eulerAngles.z));
			
				GameObject.Destroy(target);
			}
		}
		ShotCharging = false;
		
		anim.StopAction();	// stop the attack animation
	}
}
