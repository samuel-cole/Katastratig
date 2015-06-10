// This controls the ranged enemies.
// Created by Samuel Cole.

using UnityEngine;
using System.Collections;

public class EnemyRanged : MonoBehaviour 			
{													
	//The player that this agent should be chasing.
	private Transform player;

	//The AI of this enemy.
	private NavMeshAgent agent;
	//Controls animation for the upper half of the player.
	private AnimationController spriteAnimation;
	//Controls the animation for the players feet.
	public AnimationController feetAnimation;
	//Controls firing.
	private ArcherRaycast raycaster;

	//This stores the current action that the AI should be taking.
	//AIStates is defined in EnemyMelee.
	private AIStates state = AIStates.CHASING;
	//This accessor/mutator property for state is used for controlling all state changes.
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
					feetAnimation.StopAction();
				}
			}
			else if (value == AIStates.ATTACKING)
			{
				agent.destination = player.position;

				agent.speed = attackSpeed;

				if (spriteAnimation != null)
				{
					spriteAnimation.Action();
					feetAnimation.Action();
				}
			}

			state = value;
		}
	}

	// These are Nav Mesh Agents variables
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
		if (raycaster.ChargeTime < 0.8f)
		{
			State = AIStates.ATTACKING;
		}
		//If no longer attacking.
		else if (State == AIStates.ATTACKING)		
		{
			State = AIStates.CHASING;
		}

		switch (State) 
		{
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
			break;
		default:
			break;
		}
	}
}
