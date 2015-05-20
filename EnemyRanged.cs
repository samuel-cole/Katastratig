using UnityEngine;
using System.Collections;
//[RequireComponent(typeof(NavMeshAgent))]

public class EnemyRanged : MonoBehaviour // The enemy looks for AI points through the tag 'AI_Point' assigned in the inspector
{

	private Transform player; // Find the player

	private NavMeshAgent agent; //The AI of this enemy

	private AnimationController spriteAnimation;	//Controls animation.

	//This stores the current action that the AI should be taking.
	//AIStates is defined in EnemyMelee.
	private AIStates state = AIStates.CHASING; // Set this to true when Player class is created
	private AIStates State
	{
		get { return state; }
		set
		{
			if (value == AIStates.CHASING)
			{
				agent.destination = player.position;

				agent.speed = speed;

				if (spriteAnimation != null)
				{
					spriteAnimation.StopAction();
				}
			}
			else if (value == AIStates.ATTACKING)
			{
				agent.destination = player.position;

				agent.speed = attackSpeed;

				if (spriteAnimation != null)
				{
					spriteAnimation.Action();
				}
			}
			else if (value == AIStates.AVOIDING)
			{
				agent.destination = aiPoints[random].transform.position;

				agent.speed = speed;

				if (spriteAnimation != null)
				{
					spriteAnimation.StopAction();
				}
			}

			state = value;
		}
	}
	
	private GameObject[] aiPoints; 	// This array finds all the invisible 'pink' ai points around the game scene
	private int random;
	//private int currentAiPointNumber; // A precaution
	private float waitTimer = 1.0f; // A precaution

	//Timer that dictates how long the AI should head towards a node before returning its attention to the player.
	public float nodeTimer = 0.5f;
	private float nodeTimerCurrent;

	// These are Nav Mesh Agents variables
	// Easier to change navMeshAgent Variables on this scrip than on component
	public float speed = 10.0f;
	public float Speed
	{
		get { return speed; }
		set 
		{
			speed = value;
			if (State != AIStates.ATTACKING)
			{
				agent.speed = value;
			}
		}
	}

	public float attackSpeed = 30.0f;
	public float AttackSpeed
	{
		get { return attackSpeed; }
		set 
		{
			attackSpeed = value;
			if (State == AIStates.ATTACKING)
			{
				agent.speed = value;
			}
		}
	}
	public float angularSpeed = 500.0f;
	public float AngularSpeed
	{
		get { return angularSpeed; }
		set 
		{
			angularSpeed = value;
			agent.angularSpeed = value;
		}
	}

	public float acceleration = 50.0f;
	public float Acceleration
	{
		get { return acceleration; }
		set 
		{
			acceleration = value;
			agent.acceleration = value;
		}
	}
	
	//Refence to the Audio Script
	private EnemyAudio audio;

	void Awake()
	{
		aiPoints = GameObject.FindGameObjectsWithTag("AiPoint");	// Making sure the enemy class knows where the player and AI Points are
		player = GameObject.FindGameObjectWithTag("Player").transform;
		agent = GetComponent<NavMeshAgent>();

		State = AIStates.CHASING;

		nodeTimerCurrent = nodeTimer;

		spriteAnimation = transform.FindChild ("EnemyAnimation").GetComponent<AnimationController> ();
		
		audio = transform.gameObject.GetComponent<EnemyAudio>()as EnemyAudio;
	}

	void Update () 
	{
		if (State == AIStates.AVOIDING)
		{
			nodeTimerCurrent -= Time.deltaTime;
			if (nodeTimerCurrent <= 0)
			{
				State = AIStates.CHASING;
			}
		}


		Physics.S


		switch (State) 
		{
		case AIStates.AVOIDING:
			break;
		case AIStates.CHASING:
			if ( player == null)		// A failsafe, if Player does not exist.
			{
				agent.destination = Vector3.zero; 
				Debug.Log ("Player Object is Missing");
			}
			else if (player != null)
			{
				agent.destination = player.position;
			}
			break;
		case AIStates.ATTACKING:
			break;
		default:
			break;
		}
	}
	
	
	void FindAnotherAiPoint()		// This function randomly finds one of the existing waypoints.
	{
		int numberOfAiPoints = aiPoints.Length;
		random = Random.Range (0, numberOfAiPoints);
	}
	
	
	void OnTriggerEnter(Collider a_other)			
	{
		if (a_other.tag == "AiPoint")	// Trigger upon entering AI point
		{
			//waitTimer = 1.0f;
			FindAnotherAiPoint();
			State = AIStates.CHASING;
		}
		else if (a_other.tag == "EnemyTrigger")
		{
			EnemyMelee other = a_other.transform.parent.GetComponent<EnemyMelee>();
			if (other != null)	//If the other guy is melee
			{
				if (State != AIStates.ATTACKING && other.State != AIStates.ATTACKING)	//If neither guy is attacking
				{
					FindAnotherAiPoint(); 		// if enemies collide, make them both find another waypoint
					//Debug.Log ("repath!!!");
					State = AIStates.AVOIDING;
					
					nodeTimerCurrent = nodeTimer;
					
					// MAKE A NOISE!!!
					audio.Melee();
				}
			}
		}
	}
	
	void OnTriggerStay(Collider a_other)	// A failsafe. If the navMeshAgent's velocity makes the enemy get stuck in an AI point, then give it a new destination after a second.
	{
		if (a_other.tag == "AiPoint")
		{
			waitTimer -= Time.deltaTime;
			
			if (waitTimer < 0.1f)
			{
				FindAnotherAiPoint();
				waitTimer = 1.0f;
			}
			
			FindAnotherAiPoint();
		}
	}
}
