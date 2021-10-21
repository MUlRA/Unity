using UnityEngine;
using System.Collections;

public class Spawn : MonoBehaviour
{
	public GameObject [] spawnObject;
	public float xRange = 1.0f;
	public float PosyRange = 1.0f;
	public float NegyRange = 1.0f;
	public float WallMinSpawnTime = 1.0f;
	public float WallMaxSpawnTime = 10.0f;

	//Randow Weighted Spawner
	public float EnemyspawnTime = 5f; // The amount of time between each spawn.
	public float EnemyspawnDelay = 3f; // The amount of time before spawning starts.
	public float chanceSpawnRare = 0.1f; // Chance that a rare enemy will spawn.
	public GameObject[] normalEnemies; // Array of regular enemy prefabs.
	public GameObject[] rareEnemies; // Array of the rare enemy prefabs.


	// Use this for initialization
	void Start () 
	{
		InvokeRepeating("EnemySpawn", EnemyspawnDelay, EnemyspawnTime);
		Invoke ("SpawnWall", Random.Range (WallMinSpawnTime, WallMaxSpawnTime));
		// Start calling the Spawn function repeatedly after a delay.

	}
	
	// Update is called once per frame 
	void SpawnWall ()
	{
		float xOffset = Random.Range (-xRange, xRange);
		float yOffset = Random.Range (NegyRange, PosyRange);
		int spawnObjectIndex = Random.Range (0, spawnObject.Length);
		Instantiate (spawnObject [spawnObjectIndex],transform.position + new Vector3 (xOffset, yOffset, 0.0f),  transform.rotation = Quaternion.Euler(0, Random.Range (0,360), 0));
		Invoke("SpawnWall", Random.Range (WallMinSpawnTime, WallMaxSpawnTime));
	}

	//???
	GameObject[] spawnArray;
	int enemyIndex;
	
	void EnemySpawn ()
	{
		// Chance to spawn rare, or normal enemies.
		if(Random.Range(0f, 1f) > chanceSpawnRare)
		{
			spawnArray = normalEnemies;
		}
		else
		{
			spawnArray = rareEnemies;
		}
		
		// Instantiate an enemy.
		enemyIndex = Random.Range(0, spawnArray.Length);
	    Instantiate(spawnArray[enemyIndex], transform.position, transform.rotation);
	
	}
}
