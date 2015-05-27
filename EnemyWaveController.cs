using UnityEngine;
using System.Collections;

public class EnemyWaveController : MonoBehaviour // Spawns enemies according to the wave
{
	public GameObject enemyPrefab;	// From prefabs folder
	public GameObject enemyRangedPrefab;
	private GameObject[] enemies;	// array of existing enemies found in level
	public int numberOfEnemies;
	
	private GameObject[] spawnPoints;	// Find all the existing enemy spawn points
	private int random;
	private int random2;
	
	private float timer;
	public float timeBetweenWaves = 5.0f; // These variables determine how often new Enemies spawn

	private Vector3 direction = new Vector3(1, 0, 0); //This is used to offset spawning enemies by a small amount.

	void Awake()
	{
		spawnPoints = GameObject.FindGameObjectsWithTag("EnemySpawn");
	}

	void Start()
	{
		timer = timeBetweenWaves;
	}

	void Update () 
	{
		enemies = GameObject.FindGameObjectsWithTag("Enemy"); // constantly check for enemies
		
		timer -= Time.deltaTime;
		
		if (timer < 0.1f)
		{
			if (numberOfEnemies < 51)		// Cap the max number of enemies to 50
			{
				numberOfEnemies += 2;
				timer = timeBetweenWaves;
			}
		}
		
		
	
		if (enemies.Length < numberOfEnemies)	// If the current number of enemies is less than enemy wave counter, add an enemy;
		{
			ChooseARandomSpawnPoint();														// Choose a random spawn point

			if (random2 < 15)
			{
				Instantiate(enemyPrefab, spawnPoints[random].transform.position + direction, Quaternion.identity);	// And Spawn it here
				NewDirection();
			}
			else
			if (random2 == 15)
			{
				Instantiate(enemyRangedPrefab, spawnPoints[random].transform.position + direction, Quaternion.identity);	// And Spawn Ranged here!!!!!!
				NewDirection();
			}
			else
			{
				Instantiate(enemyPrefab, spawnPoints[random].transform.position + direction, Quaternion.identity);	// And Spawn it here
				NewDirection();
			}
		}
	}

	void NewDirection()
	{
		Vector3 oldDirection = direction;
		while (oldDirection == direction)
		{
			float randomNo = Random.value;
			if (randomNo < 0.25f)
			{
				direction = new Vector3(1, 0, 0);
			}
			else if (randomNo < 0.5f)
			{
				direction = new Vector3(0, 0, 1);
			}
			else if (randomNo < 0.75f)
			{
				direction = new Vector3(-1, 0, 0);
			}
			else
			{
				direction = new Vector3(0, 0, -1);
			}
		}
	}
	
	void ChooseARandomSpawnPoint()
	{
		random = Random.Range(0, spawnPoints.Length);
		random2 = Random.Range (0, 16);
	}
}
