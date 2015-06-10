// Spawns enemies according to how much time has passed. 
// Created by Rowan Donaldson.

using UnityEngine;
using System.Collections;

public class EnemyWaveController : MonoBehaviour
{
	//Melee enemy to spawn.
	public GameObject enemyPrefab;
	//Ranged enemy to spawn.
	public GameObject enemyRangedPrefab;
	//Array of existing enemies found in the level.
	private GameObject[] enemies;	
	//The number of enemies that should exist- more enemies will be spawned when the actual number of enemies is less than this number.
	public int numberOfEnemies;

	//List of gameobjects that enemies can spawn at.
	private GameObject[] spawnPoints;

	// The amount of time between waves of enemies.
	public float timeBetweenWaves = 5.0f; 
	//The amount of time before the next wave spawns.
	private float timeBetweenWavesCurrent;

	//This is used to offset spawning enemies by a small amount.
	private Vector3 direction = new Vector3(1, 0, 0); 

	void Awake()
	{
		spawnPoints = GameObject.FindGameObjectsWithTag("EnemySpawn");
	}

	void Start()
	{
		timeBetweenWavesCurrent = timeBetweenWaves;
	}

	void Update () 
	{
		//Constantly check for enemies
		enemies = GameObject.FindGameObjectsWithTag("Enemy"); 

		timeBetweenWavesCurrent -= Time.deltaTime;
		
		if (timeBetweenWavesCurrent < 0.0f)
		{
			// Cap the max number of enemies. Helps in reducing the ridiculous amount of noise that lots of enemies make.
			if (numberOfEnemies < 26)		
			{
				numberOfEnemies += 2;
				timeBetweenWavesCurrent = timeBetweenWaves;
			}
		}
		
		
		// If the current number of enemies is less than enemy wave counter, add an enemy;
		if (enemies.Length < numberOfEnemies)	
		{

			int randomSpawnPoint = Random.Range(0, spawnPoints.Length);
			int enemyType = Random.Range (0, 12);

			if (enemyType < 10)
			{
				Instantiate(enemyPrefab, spawnPoints[randomSpawnPoint].transform.position + direction, Quaternion.identity);	
				NewDirection();
			}
			else if (enemyType >= 10)
			{
				Instantiate(enemyRangedPrefab, spawnPoints[randomSpawnPoint].transform.position + direction, Quaternion.identity);
				NewDirection();
			}
			else
			{
				Instantiate(enemyPrefab, spawnPoints[randomSpawnPoint].transform.position + direction, Quaternion.identity);
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
}
