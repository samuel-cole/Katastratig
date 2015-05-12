using UnityEngine;
using System.Collections;
//[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMelee : MonoBehaviour // The enemy looks for AI points through the tag 'AI_Point' assigned in the inspector
{

	private GameObject player; // Find the player
	public bool chasingPlayer = true; // Set this to true when Player class is created
	
	private GameObject[] aiPoints; 	// This array finds all the invisible 'pink' ai points around the game scene
	private int random;
	//private int currentAiPointNumber; // A precaution
	private float waitTimer = 1.0f; // A precaution


	// These are Nav Mesh Agents variables
	public float speed = 10.0f;
	public float angularSpeed = 500.0f;
	public float acceleration = 50.0f;
	
	//Refence to the Audio Script
	private EnemyAudio audio;

	void Awake()
	{
		aiPoints = GameObject.FindGameObjectsWithTag("AiPoint");	// Making sure the enemy class knows where the player and AI Points are
		player = GameObject.FindGameObjectWithTag("Player");
		
		audio = transform.gameObject.GetComponent<EnemyAudio>()as EnemyAudio;
	}

	void Update () 
	{
		if (waitTimer < 0.0f)
		{
			waitTimer = 0.0f;
		}
	
		GameObject targetObject = aiPoints[random]; // Select one of the ai points for the nav mesh to navigate towards
		
		gameObject.GetComponent<NavMeshAgent>().speed = speed;
		gameObject.GetComponent<NavMeshAgent>().angularSpeed = angularSpeed;
		gameObject.GetComponent<NavMeshAgent>().acceleration = acceleration;	// Easier to change navMeshAgent Variables on this scrip than on component
	
	
		if (!chasingPlayer)			// In the event of 'enemy clustering', enemy breifly moves to a different destination
		{
			gameObject.GetComponent<NavMeshAgent>().destination = targetObject.transform.position;
		}
		else
		if (chasingPlayer)
		{
			if ( player == null)		// A failsafe, if Player does not exist.
			{
				gameObject.GetComponent<NavMeshAgent>().destination = Vector3.zero; 
				Debug.Log ("Player Object is Missing");
			}
			else
			if (player != null)
			{
				gameObject.GetComponent<NavMeshAgent>().destination = player.transform.position;
			}
		}
	}
	
	
	void FindAnotherAiPoint()		// This function randomly finds one of the existing waypoints.
	{
		int numberOfAiPoints = aiPoints.Length;
		random = Random.Range (0, numberOfAiPoints);
	}
	
	
	void OnTriggerEnter(Collider other)			
	{
		if (other.tag == "AiPoint")	// Trigger upon entering AI point
		{
			//waitTimer = 1.0f;
			FindAnotherAiPoint();
			chasingPlayer = true;
		}
		
		if (other.tag == "EnemyTrigger")
		{
			FindAnotherAiPoint(); 		// if enemies collide, make them both find another waypoint
			//Debug.Log ("repath!!!");
			chasingPlayer = false;
			
			// MAKE A NOISE!!!
			audio.Melee();
		}
	}
	
	void OnTriggerStay(Collider other)	// A failsafe. If the navMeshAgent's velocity makes the enemy get stuck in an AI point, then give it a new destination after a second.
	{
		if (other.tag == "AiPoint")
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
