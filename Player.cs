// Used for controlling the player character and handling input.
// Created by Samuel Cole.

using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
	//Force-based default values (speed, roll speed and roll influence) all presume that the player has a drag value of 10.

	//player is used to store the player's rigidbody, for use in physics calculations.
	private Rigidbody player;

	//The collider the player uses. Disabled while rolling, and re-enabled when no longer rolling.
	private BoxCollider playerCollider;

	//Pretty self-explanatary, speed influences the rate the player moves at.
	public float speed = 100.0f;

	//I've used a vector3 for rolling- values of 0, 0, 0 are used to show that the player is not rolling,
	//while otherwise it should be a normalized vector containing the direction the player is rolling in.
	private Vector3 rolling = Vector3.zero;

	//The speed at which the player rolls while dodging.
	public float rollSpeed = 200.0f;

	//The degree to which the player can influence the direction of a dodge roll mid-roll.
	public float rollInfluence = 50.0f;

	//The amount of time that a roll takes.
	public float rollTime = 0.3f;
	//Timer that counts down from roll time whenever the player rolls.
	private float rollTimeCurrent = 0.0f;
	//Accessor property for rollTimeCurrent
	public float RollTimeCurrent
	{
		get { return rollTimeCurrent; }
	}

	//The amount of time that the player must wait after a roll is completed before beginning a new one.
	public float rollTimeCooldown = 0.5f;

	//The distance from which the player causes melee enemies to charge.
	public float meleeAggroDistance = 3.0f;

	//Effects script used for making the camera shake when the player rolls.	
	public CameraShake cameraShake;
	//Effects script used for creating dust behind the player while rolling.
	public PlayerMakeDust makeDust;
	//Animation script for controlling the animations for the player's upper body.
	public PlayerAnimationController myAnimation;
	//Animation script for controlling the animations for the player's feet.
	public PlayerAnimationController feetAnimation;
	//Audio script used for playing sounds (only plays roll sounds, but could be possibly expanded later).
	public PlayerAudio myAudio;

	//Blood splatter object for spawning on death.
	public GameObject bloodSplatter;
	//Body object for spawning on death.
	public GameObject playerBodyPrefab;
	[HideInInspector]
	//Whether or not the player is currently alive- used for removing the normal player sprite after death, but before switching to the results screen.
	public bool alive = true;


	// Use this for initialization
	void Start () 
	{
		player = GetComponent<Rigidbody>();
		playerCollider = GetComponent<BoxCollider>();
	}
	
	// FixedUpdate is being used instead of update here because I do physics calculations.
	void FixedUpdate () 
	{
		//Handle stuff for when the player is not rolling.
		if (rolling == Vector3.zero)
		{
			Vector3 force = new Vector3();
			
			if (Input.GetKey(KeyCode.W))
			{
				force += new Vector3(0, 0, 1);
			}
			if (Input.GetKey (KeyCode.A))
			{
				force += new Vector3(-1, 0, 0);		
			}
			if (Input.GetKey (KeyCode.S))
			{
				force += new Vector3(0, 0, -1);		
			}
			if (Input.GetKey(KeyCode.D))
			{
				force += new Vector3(1, 0, 0);		
			}

			//Normalize to ensure that the force is equal even when moving diagonally.
			force.Normalize();

			player.AddForce(force * speed);

			//Here is where rolls are initiated.
			if (Input.GetKey (KeyCode.Space) && rollTimeCurrent <= -rollTimeCooldown && (Input.GetKey(KeyCode.D)|| Input.GetKey (KeyCode.S) || Input.GetKey (KeyCode.A) || Input.GetKey(KeyCode.W)))
			{
				rolling = force;
				rollTimeCurrent = rollTime;
				playerCollider.center = new Vector3(0.0f, 0.0f, -2.0f);
				
				//Do some effects to show that the player is rolling.
				myAnimation.Action();
				feetAnimation.Action();
				myAudio.Roll ();
				cameraShake.CamShake();
				makeDust.MakeDust();
			}

		}
		//Handle stuff for when the player is rolling- 
		//at this stage, the player should continue to move in the direction that they are rolling in, while also being able to have *some* influence on their direction.
		else
		{
			Vector3 force = new Vector3();

			if (Input.GetKey(KeyCode.W))//|| Input.GetKey(KeyCode.UpArrow))
			{
				force += new Vector3(0, 0, 1);
			}
			if (Input.GetKey (KeyCode.A))//|| Input.GetKey(KeyCode.LeftArrow))
			{
				force += new Vector3(-1, 0, 0);		
			}
			if (Input.GetKey (KeyCode.S))//|| Input.GetKey(KeyCode.DownArrow))
			{
				force += new Vector3(0, 0, -1);		
			}
			if (Input.GetKey(KeyCode.D))// || Input.GetKey(KeyCode.RightArrow))
			{
				force += new Vector3(1, 0, 0);		
			}

			force = force.normalized * rollInfluence;
			force += rolling * rollSpeed;

			player.AddForce(force);
		}

		Collider[] colliders = Physics.OverlapSphere(transform.position, meleeAggroDistance);
		for (uint i = 0; i < colliders.Length; ++i)
		{
			if (colliders[i].gameObject.GetComponent<EnemyMelee>() != null)
			{
				colliders[i].gameObject.GetComponent<EnemyMelee>().Aggro(transform.position);
			}
		}

	}

	//Update is used for all of the non-physics related things that happen during the update loop.
	void Update()
	{
		//Used for testing if the player stopped rolling this frame.
		bool rollingCheck = rollTimeCurrent > 0;

		rollTimeCurrent -= Time.deltaTime;

		//If the player stopped rolling this frame.
		if (rollingCheck && rollTimeCurrent <= 0) 	
		{
			rolling = Vector3.zero;
			playerCollider.center = new Vector3(0.0f, 0.0f, 0.0f);
		}
		
		if (!alive)
		{
			myAnimation.visible = false;
			feetAnimation.visible = false;
		}
	}


	void OnCollisionEnter(Collision a_collision)
	{
		if (a_collision.gameObject.CompareTag("Enemy"))
		{
			GameObject.FindGameObjectWithTag("GameController").GetComponent<StateManager>().Results();
		}
	}

}
