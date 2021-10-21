using UnityEngine;
using System.Collections;

public class RandomSpawner : MonoBehaviour
{
	public GameObject[] spawnObject;
	public Transform parent;
	public float xRange = 1.0f;
	public float yRange = 1.0f;
	public float minSpawnTime = 1.0f;
	public float maxSpawnTime = 10.0f;
	
	void Start()
	{
		Invoke("SpawnWall", Random.Range(minSpawnTime,maxSpawnTime));
	}
	
	void SpawnWall()
	{
		float xOffset = Random.Range(-xRange, xRange);
		float yOffset = Random.Range(-yRange, yRange);
		int spawnObjectIndex = Random.Range(0,spawnObject.Length);
		GameObject instance = Instantiate(spawnObject[spawnObjectIndex],transform.position + new Vector3(xOffset,yOffset,0.0f), spawnObject[spawnObjectIndex].transform.rotation) as GameObject;
		instance.transform.parent = parent;
		Invoke("SpawnWall", Random.Range(minSpawnTime,maxSpawnTime));
	}
}
