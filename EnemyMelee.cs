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

	private Transform player; // Find the player

	private NavMeshAgent agent; //The AI of this enemy

	private AnimationController spriteAnimation;	//Controls animation.
	
	//This stores the current action that the AI should be taking.
	private AIStates state = AIStates.CHASING; // Set this to true when Player class is created
	public AIStates State
	{
		get { return state; }
		set
		{
			if ((state == AIStates.ATTACKING) != (value == AIStates.ATTACKING))	//If either the new or old state is attacking, but the other isn't.
			{
				stopTimeCurrent = stopTime;
				agent.speed = 0;
			}
			if (value == AIStates.CHASING)
			{
				agent.destination = player.position;

				if (spriteAnimation != null)
				{
					spriteAnimation.StopAction();
				}
			}
			else if (value == AIStates.ATTACKING)
			{
				agent.destination = chargePosition;

				if (spriteAnimation != null)
				{
					spriteAnimation.Action();
				}
			}
			else if (value == AIStates.AVOIDING)
			{
				agent.destination = aiPoints[random].transform.position;

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
	private EnemyAudio enemyAudio;

	//Reference to the game manager- used for adding multiplier when killing other enemies.
	private GameManager gameManager;

	void Awake()
	{
		aiPoints = GameObject.FindGameObjectsWithTag("AiPoint");	// Making sure the enemy class knows where the player and AI Points are
		if (GameObject.FindGameObjectWithTag("Player") != null)
		{
			player = GameObject.FindGameObjectWithTag("Player").transform;
		}

		agent = GetComponent<NavMeshAgent>();

		State = AIStates.CHASING;

		nodeTimerCurrent = nodeTimer;
		attackCooldownCurrent = attackCooldown;
		stopTimeCurrent = stopTime;

		spriteAnimation = transform.FindChild ("EnemyAnimation").GetComponent<AnimationController> ();
		
		enemyAudio = transform.gameObject.GetComponent<EnemyAudio>()as EnemyAudio;

		gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
	}

	void Update () 
	{
		attackCooldownCurrent -= Time.deltaTime;

		bool stopped = stopTimeCurrent > 0;	//Used for checking if we stopped this frame.

		stopTimeCurrent -= Time.deltaTime;

		if (stopped == true && stopTimeCurrent > 0 == false)	//If we just became un-stopped
		{
			agent.speed = ((State == AIStates.ATTACKING)?attackSpeed:speed);
		}

		if (State == AIStates.AVOIDING)
		{
			nodeTimerCurrent -= Time.deltaTime;
			if (nodeTimerCurrent <= 0)
			{
				State = AIStates.CHASING;
			}
		}

		if (stopTimeCurrent > 0)
		{
			transform.rotation =  Quaternion.LookRotation(agent.destination - transform.position);
		}
	
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
			if ((new Vector2(transform.position.x, transform.position.z) -  new Vector2(chargePosition.x, chargePosition.z)).sqrMagnitude < 0.1f)
			{
				State = AIStates.CHASING;
				attackCooldownCurrent = attackCooldown;
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
					enemyAudio.Melee();
				}
				else if (agent.velocity.sqrMagnitude > 30.0f && State == AIStates.ATTACKING)	//If I am attacking
				{
					if (other.agent.velocity.sqrMagnitude > 30.0f && other.State == AIStates.ATTACKING)	//And he is too
					{
						//Destroy both
						DestroyObject(a_other.transform.parent.gameObject);
						DestroyObject(gameObject);
						gameManager.IncreaseMultiplier();
						gameManager.IncreaseMultiplier();
					}
					else //And he isn't attacking
					{
						//Just destroy him
						DestroyObject(a_other.transform.parent.gameObject);
						gameManager.IncreaseMultiplier();
					}
				}
			}
			else if (a_other.transform.parent.GetComponent<EnemyRanged>())
			{
				if (agent.velocity.sqrMagnitude > 30.0f && State == AIStates.ATTACKING)
				{
					DestroyObject(a_other.transform.parent.gameObject);
					gameManager.IncreaseMultiplier();
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

	//This is called by the player when enemy melee units are within charge range.
	public void Aggro(Vector3 a_playerPosition)
	{
		if (State != AIStates.ATTACKING && attackCooldownCurrent <= 0) 
		{
			chargePosition = transform.position + 2 * (a_playerPosition - transform.position);

			if (chargePosition.sqrMagnitude > 324) //if charge position is out of circle
			{
				chargePosition = chargePosition.normalized * 18.0f;	//put it back in the circle.
			}

			State = AIStates.ATTACKING;
		}
	}
}
