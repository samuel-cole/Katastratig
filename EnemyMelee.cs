// This controls the melee enemies.
// Created by Samuel Cole and Rowan Donaldson.

using UnityEngine;
using System.Collections;

public enum AIStates
{
	CHASING,
	AVOIDING,
	ATTACKING
}

public class EnemyMelee : MonoBehaviour
{
	//The player that this agent should be chasing.
	private Transform player; 
	//The AI of this enemy.
	private NavMeshAgent agent; 

	//Controls animation for the upper half of the player.
	private AnimationController spriteAnimation;
	//Controls the animation for the players feet.
	public AnimationController feetAnimation;
	
	//This stores the current action that the AI should be taking.
	private AIStates state = AIStates.CHASING;
	//This accessor/mutator property for state is used for controlling all state changes.
	public AIStates State
	{
		get { return state; }
		set
		{
			if ((state == AIStates.ATTACKING) != (value == AIStates.ATTACKING))	
			{
				//If either the new or old state is attacking, but the other isn't.
				stopTimeCurrent = stopTime;
				agent.speed = 0;
			}
			if (value == AIStates.CHASING)
			{
				agent.destination = player.position;

				if (spriteAnimation != null)
				{
					spriteAnimation.StopAction();
					feetAnimation.StopAction();
				}
			}
			else if (value == AIStates.ATTACKING)
			{
				agent.destination = chargePosition;

				if (spriteAnimation != null)
				{
					spriteAnimation.Action();
					feetAnimation.Action();
				}
			}
			else if (value == AIStates.AVOIDING)
			{
				agent.destination = aiPoints[randomPoint].transform.position;

				if (spriteAnimation != null)
				{
					spriteAnimation.StopAction();
					feetAnimation.StopAction();
				}
			}

			state = value;
		}
	}

	//This array contains all of the invisible 'pink' ai points around the game scene.
	private GameObject[] aiPoints; 	
	//A random index to the 'aiPoints' array for the point that this agent should move to while avoiding.
	private int randomPoint;
	//A precaution used to stop the AI from getting stuck in AI points.
	private float waitTimer = 1.0f;

	//Timer that dictates how long the AI should head towards a node before returning its attention to the player.
	public float nodeTimer = 0.5f;
	//The current amount of time to wait until returning to chasing the player.
	private float nodeTimerCurrent;

	//Atack related stuff.
	//How long melee enemies should wait before attacking again.
	public float attackCooldown = 4.0f;
	//The current amount of time before this enemy attacks again.
	private float attackCooldownCurrent;
	//The position that this enemy should charge towards.
	public Vector3 chargePosition;
	//The amount of time before and after attacks that this enemy should wait before moving.
	public float stopTime = 1.0f;
	//The current amount of time until this enemy begins moving again.
	private float stopTimeCurrent;

	//These are Nav Mesh Agents variables
	//Easier to change navMeshAgent Variables in this script than on the component.
	//The speed of this agent while walking.
	public float speed = 10.0f;
	//Accessor/mutator property for speed.
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
	//The speed of this agent while attacking.
	public float attackSpeed = 30.0f;
	//Accessor/mutator property for attackSpeed.
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
	//The angular speed of this agent.
	public float angularSpeed = 500.0f;
	//Accessor/mutator property for angular speed.
	public float AngularSpeed
	{
		get { return angularSpeed; }
		set 
		{
			angularSpeed = value;
			agent.angularSpeed = value;
		}
	}
	//The acceleration of this agent.
	public float acceleration = 50.0f;
	//Accessor/mutator property for this agent.
	public float Acceleration
	{
		get { return acceleration; }
		set 
		{
			acceleration = value;
			agent.acceleration = value;
		}
	}
	
	//Refence to the Audio Script for playing sounds.
	private EnemyAudio myAudio;
	
	//Prefabs to be spawned upon being killed/killing enemies.
	public GameObject bloodPrefab;
	public GameObject bodyMeleePrefab;
	public GameObject bodyRangedPrefab;

	void Awake()
	{
		// Making sure the enemy class knows where the player and AI Points are
		aiPoints = GameObject.FindGameObjectsWithTag("AiPoint");	
		player = GameObject.FindGameObjectWithTag("Player").transform;
		agent = GetComponent<NavMeshAgent>();

		State = AIStates.CHASING;

		nodeTimerCurrent = nodeTimer;
		attackCooldownCurrent = attackCooldown;
		stopTimeCurrent = stopTime;

		spriteAnimation = transform.FindChild ("EnemyAnimation").GetComponent<AnimationController> ();
		
		myAudio = transform.gameObject.GetComponent<EnemyAudio>()as EnemyAudio;
	}

	void Update () 
	{
		attackCooldownCurrent -= Time.deltaTime;

		//Used for checking if we stopped this frame.
		bool stopped = stopTimeCurrent > 0;	

		stopTimeCurrent -= Time.deltaTime;

		//If we just became un-stopped
		if (stopped == true && stopTimeCurrent > 0 == false)	
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
			if ( player == null)		
			{
				// A failsafe, if Player does not exist.
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
	
	// This function randomly finds one of the existing waypoints.
	void FindAnotherAiPoint()		
	{
		int numberOfAiPoints = aiPoints.Length;
		randomPoint = Random.Range (0, numberOfAiPoints);
	}
	
	
	void OnTriggerEnter(Collider a_other)			
	{
		// Trigger upon entering AI point
		if (a_other.tag == "AiPoint")	
		{
			FindAnotherAiPoint();
			State = AIStates.CHASING;
		}
		else if (a_other.tag == "EnemyTrigger")
		{
			EnemyMelee other = a_other.transform.parent.GetComponent<EnemyMelee>();
			//If the other guy is melee
			if (other != null)	
			{
				//If neither guy is attacking
				if (State != AIStates.ATTACKING && other.State != AIStates.ATTACKING)	
				{
					//If enemies collide, make them both find another waypoint
					FindAnotherAiPoint(); 		

					State = AIStates.AVOIDING;
					
					nodeTimerCurrent = nodeTimer;

					myAudio.Melee();
				}
				//If I am attacking
				else if (agent.velocity.sqrMagnitude > 30.0f && State == AIStates.ATTACKING)	
				{
					//And he is too
					if (other.agent.velocity.sqrMagnitude > 30.0f && other.State == AIStates.ATTACKING)	
					{
						//Destroy both
						DestroyObject(a_other.transform.parent.gameObject);
						Instantiate(bloodPrefab, a_other.transform.position, Quaternion.Euler(90, a_other.transform.rotation.eulerAngles.y, a_other.transform.rotation.eulerAngles.z));
						Instantiate(bodyMeleePrefab, new Vector3(a_other.transform.position.x, 0.1f, a_other.transform.position.z), Quaternion.Euler(90, a_other.transform.rotation.eulerAngles.y, a_other.transform.rotation.eulerAngles.z));

						DestroyObject(gameObject);
						Instantiate(bloodPrefab, transform.position, Quaternion.Euler(90, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z));
						Instantiate(bodyMeleePrefab, new Vector3(transform.position.x, 0.1f, transform.position.z), Quaternion.Euler(90, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z));
					}
					//And he isn't attacking
					else 
					{
						//Just destroy him
						DestroyObject(a_other.transform.parent.gameObject);
						Instantiate(bloodPrefab, a_other.transform.position, Quaternion.Euler(90, a_other.transform.rotation.eulerAngles.y, a_other.transform.rotation.eulerAngles.z));
						Instantiate(bodyMeleePrefab, new Vector3(a_other.transform.position.x, 0.1f, a_other.transform.position.z), Quaternion.Euler(90, a_other.transform.rotation.eulerAngles.y, a_other.transform.rotation.eulerAngles.z));
					}
				}
			}
			//If the other guy is ranged
			else if (a_other.transform.parent.GetComponent<EnemyRanged>())
			{
				if (agent.velocity.sqrMagnitude > 30.0f && State == AIStates.ATTACKING)
				{
					DestroyObject(a_other.transform.parent.gameObject);
					Instantiate(bloodPrefab, a_other.transform.position, Quaternion.Euler(90, a_other.transform.rotation.eulerAngles.y, a_other.transform.rotation.eulerAngles.z));
					Instantiate(bodyRangedPrefab, new Vector3(a_other.transform.position.x, 0.1f, a_other.transform.position.z), Quaternion.Euler(90, a_other.transform.rotation.eulerAngles.y, a_other.transform.rotation.eulerAngles.z));
				}
			}
		}
	}

	// A failsafe. If the navMeshAgent's velocity makes the enemy get stuck in an AI point, then give it a new destination after a second.
	void OnTriggerStay(Collider a_other)	
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

			//If charge position is outside of circle
			if (chargePosition.sqrMagnitude > 324) 	
			{
				//Put it back in the circle.
				chargePosition = chargePosition.normalized * 18.0f; 
			}

			State = AIStates.ATTACKING;
		}
	}
}
