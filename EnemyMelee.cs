using UnityEngine;
using System.Collections;
//[RequireComponent(typeof(NavMeshAgent))]
public enum AIStates
{
	CHASING,
	AVOIDING,
	ATTACKING
}

public class EnemyMelee : MonoBehaviour // The enemy looks for AI points through the tag 'AI_Point' assigned in the inspector
{

	private GameObject player; // Find the player
	//This stores the current action that the AI should be taking.
	public AIStates state = AIStates.CHASING; // Set this to true when Player class is created
	
	private GameObject[] aiPoints; 	// This array finds all the invisible 'pink' ai points around the game scene
	private int random;
	//private int currentAiPointNumber; // A precaution
	private float waitTimer = 1.0f; // A precaution

	//Timer that dictates how long the AI should head towards a node before returning its attention to the player.
	public float nodeTimer = 0.5f;
	private float nodeTimerCurrent;

	//Atack related stuff.
	public float attackCooldown = 4.0f;
	private float attackCooldownCurrent;
	public Vector3 chargePosition;
	public float stopTime = 1.0f;
	private float stopTimeCurrent;

	// These are Nav Mesh Agents variables
	// Easier to change navMeshAgent Variables on this scrip than on component
	public float speed = 10.0f;
	public float Speed
	{
		get { return speed; }
		set 
		{
			speed = value;
			if (state != AIStates.ATTACKING)
			{
				gameObject.GetComponent<NavMeshAgent>().speed = value;
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
			if (state == AIStates.ATTACKING)
			{
				gameObject.GetComponent<NavMeshAgent>().speed = value;
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
			gameObject.GetComponent<NavMeshAgent>().angularSpeed = value;
		}
	}

	public float acceleration = 50.0f;
	public float Acceleration
	{
		get { return acceleration; }
		set 
		{
			acceleration = value;
			gameObject.GetComponent<NavMeshAgent>().acceleration = value;

		}
	}
	
	//Refence to the Audio Script
	private EnemyAudio audio;

	void Awake()
	{
		aiPoints = GameObject.FindGameObjectsWithTag("AiPoint");	// Making sure the enemy class knows where the player and AI Points are
		player = GameObject.FindGameObjectWithTag("Player");

		nodeTimerCurrent = nodeTimer;
		attackCooldownCurrent = attackCooldown;
		stopTimeCurrent = stopTime;
		
		audio = transform.gameObject.GetComponent<EnemyAudio>()as EnemyAudio;
	}

	void Update () 
	{
		attackCooldownCurrent -= Time.deltaTime;
		stopTimeCurrent -= Time.deltaTime;

		if (waitTimer < 0.0f)
		{
			waitTimer = 0.0f;
		}

		if (stopTime <= 0)

		if (state == AIStates.AVOIDING)
		{
			nodeTimerCurrent -= Time.deltaTime;
			if (nodeTimerCurrent <= 0)
			{
				state = AIStates.CHASING;
			}
		}
	
		GameObject targetObject = aiPoints[random]; // Select one of the ai points for the nav mesh to navigate towards
	
		switch (state) 
		{
		case AIStates.AVOIDING:
			gameObject.GetComponent<NavMeshAgent>().destination = targetObject.transform.position;
			break;
		case AIStates.CHASING:
			if ( player == null)		// A failsafe, if Player does not exist.
			{
				gameObject.GetComponent<NavMeshAgent>().destination = Vector3.zero; 
				Debug.Log ("Player Object is Missing");
			}
			else if (player != null)
			{
				gameObject.GetComponent<NavMeshAgent>().destination = player.transform.position;
			}
			break;
		case AIStates.ATTACKING:
			if ((new Vector2(transform.position.x, transform.position.z) -  new Vector2(chargePosition.x, chargePosition.z)).sqrMagnitude < 0.1f)
			{
				state = AIStates.CHASING;
				attackCooldownCurrent = attackCooldown;
				gameObject.GetComponent<NavMeshAgent>().speed = speed;
				gameObject.GetComponent<NavMeshAgent>().destination = player.transform.position;
				Debug.Log("Reached player position");
			}
			else
			{
				Debug.Log((transform.position - chargePosition).sqrMagnitude);
			}
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
			state = AIStates.CHASING;
		}
		else if (a_other.tag == "EnemyTrigger")
		{
			if (state != AIStates.ATTACKING)
			{
				FindAnotherAiPoint(); 		// if enemies collide, make them both find another waypoint
				//Debug.Log ("repath!!!");
				state = AIStates.AVOIDING;
				
				nodeTimerCurrent = nodeTimer;
				
				// MAKE A NOISE!!!
				audio.Melee();
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

	//This is called by the player when enemy melee units are within charge range.
	public void Aggro(Vector3 a_playerPosition)
	{
		if (state != AIStates.ATTACKING && attackCooldownCurrent <= 0) 
		{
			chargePosition = transform.position + 2 * (a_playerPosition - transform.position);
			state = AIStates.ATTACKING;
			NavMeshAgent agent = gameObject.GetComponent<NavMeshAgent>();
			agent.speed = attackSpeed;
			agent.destination = chargePosition;
		}
	}
}
