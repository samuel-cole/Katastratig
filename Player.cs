using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour 
{
	//Values given for each variable are values that felt enjoyable during an early test of player movement.
	//Force-based values (speed, roll speed and roll influence) all presume that the player has a drag value of 10.

	//Used for changing the colour of the player in test runs to check if dodge rolls are working properly.
	private SpriteRenderer test;

	//player is used to store the player's rigidbody, for use in physics calculations.
	private Rigidbody player;

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

	//The amount of time that the player must wait after a roll is completed before beginning a new one.
	public float rollTimeCooldown = 0.5f;

	public float meleeAggroDistance = 3.0f;

	// Use this for initialization
	void Start () 
	{
		player = GetComponent<Rigidbody>();
		test = GetComponent<SpriteRenderer>();
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
			if (Input.GetKey (KeyCode.Space) && rollTimeCurrent <= -rollTimeCooldown)
			{
				rolling = force;
				rollTimeCurrent = rollTime;
				test.color = Color.red;
			}

		}
		//Handle stuff for when the player is rolling- 
		//at this stage, the player should continue to move in the direction that they are rolling in, while also being able to have *some* influence on their direction.
		else
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
		rollTimeCurrent -= Time.deltaTime;

		if (rollTimeCurrent <= 0)
		{
			rolling = Vector3.zero;
			test.color = Color.white;
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
