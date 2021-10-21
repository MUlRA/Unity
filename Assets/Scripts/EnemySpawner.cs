using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {
	public GameObject[] objects;


	// Use this for initialization
	void Start () {
	
	}
	public void SpawnRandom()
	{
		Instantiate(objects[UnityEngine.Random.Range(0, objects.Length - 1)]);
	}

	// Update is called once per frame
	void Update () {
	
	}
}
