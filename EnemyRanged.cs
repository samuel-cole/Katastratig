using UnityEngine;
using System.Collections;
//[RequireComponent(typeof(NavMeshAgent))]

public class EnemyRanged : MonoBehaviour // The enemy looks for AI points through the tag 'AI_Point' assigned in the inspector
{

	private Transform player; // Find the player

	private NavMeshAgent agent; //The AI of this enemy

	private AnimationController spriteAnimation;	//Controls animation.
	private ArcherRaycast raycaster; //Controls firing.

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

			state = value;
		}
	}

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

	void Awake()
	{
		player = GameObject.FindGameObjectWithTag("Player").transform;
		agent = GetComponent<NavMeshAgent>();
		raycaster = transform.FindChild("RangedAttack").GetComponent<ArcherRaycast>();

		State = AIStates.CHASING;

		spriteAnimation = transform.FindChild ("EnemyAnimation").GetComponent<AnimationController> ();
	}

	void Update () 
	{
		if (raycaster.Timer < 0.8f)
		{
			State = AIStates.ATTACKING;
		}
		else if (State == AIStates.ATTACKING)		//If no longer attacking.
		{
			State = AIStates.CHASING;
		}

		//spriteAnimation.transform.up = player.transform.position - spriteAnimation.transform.position;

		switch (State) 
		{
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
}
