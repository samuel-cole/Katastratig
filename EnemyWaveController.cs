using UnityEngine;
using System.Collections;

public class EnemyWaveController : MonoBehaviour // Spawns enemies according to the wave
{
	public GameObject enemyPrefab;	// From prefabs folder
	private GameObject[] enemies;	// array of existing enemies found in level
	public int numberOfEnemies;
	
	private GameObject[] spawnPoints;	// Find all the existing enemy spawn points
	private int random;
	
	private float timer;
	public float timeBetweenWaves = 5.0f; // These variables determine how often new Enemies spawn

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
			if (numberOfEnemies < 51)		// Cap the max number of enemies to 100
			{
				numberOfEnemies += 2;
				timer = timeBetweenWaves;
			}
		}
		
		
	
		if (enemies.Length < numberOfEnemies)	// If the current number of enemies is less than enemy wave counter, add an enemy;
		{
			ChooseARandomSpawnPoint();														// Choose a random spawn point
			Instantiate(enemyPrefab, spawnPoints[random].transform.position, Quaternion.identity);	// And Spawn it here
		}
	}
	
	void ChooseARandomSpawnPoint()
	{
		//int numberOfSpwanPoints = spawnPoints.Length;
		//random = Random.Range(0, numberOfSpwanPoints);
		random = Random.Range(0, spawnPoints.Length);
	}
}
