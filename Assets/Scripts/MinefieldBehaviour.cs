using UnityEngine;
using System.Collections;

public class MinefieldBehaviour : MonoBehaviour
{
	public GameObject Mines;
	public float xRange = 1.0f;
	public float zRange = 1.0f;
	public float NegyRange = -100.0f;
	public float PosyRange = 100.0f;
	public float MineDensity = 10.0f;
	public float DeathTimer = 1.0f;




	// Use this for initialization
	void Start () 
	{
		Invoke ("SpawnMines", 0);
		Invoke ("Death", DeathTimer);
	}
	
	// Update is called once per frame 
	void SpawnMines ()
	{
		float xOffset = Random.Range (-xRange, xRange);
		float yOffset = Random.Range (NegyRange, PosyRange);
		float zOffset = Random.Range (-zRange, zRange);
		Instantiate (Mines,transform.position + new Vector3 (xOffset, yOffset, zOffset), Mines.transform.rotation);
		Invoke ("SpawnMines", MineDensity);
	}
	void Death()
	{
		Destroy (gameObject);
		
	}
}
