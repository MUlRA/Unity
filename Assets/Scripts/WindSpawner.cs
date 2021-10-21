using UnityEngine;
using System.Collections;

public class WindSpawner : MonoBehaviour 
{
	public GameObject[] WindSwirls;
	public float PosyRange = 1.0f;
	public float NegyRange = 1.0f;
	public float PosxRange = 1.0f;
	public float NegxRange = 1.0f;
	public float Windminspawn = 1.0f;
	public float Windmaxspawn = 5.0f;
	// Use this for initialization
	void Start () 
	{
		Invoke ("SpawnWind", Random.Range (Windminspawn, Windmaxspawn));
	}
	void SpawnWind ()
	{
		float xOffset = Random.Range (NegxRange, PosxRange);
		float yOffset = Random.Range (NegyRange, PosyRange);
		int spawnObjectIndex = Random.Range (0, WindSwirls.Length);
		Instantiate (WindSwirls [spawnObjectIndex],transform.position + new Vector3 (xOffset, yOffset, 0.0f), WindSwirls [spawnObjectIndex].transform.rotation);
		Invoke("SpawnWind", Random.Range (Windminspawn, Windmaxspawn));
	}
}
