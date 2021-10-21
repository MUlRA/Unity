using UnityEngine;
using System.Collections;

public class SpawnOnTrigger : MonoBehaviour
{
	public GameObject spawnObject;
	public float cooldown = 1.0f;
	private float cooldownTimer = 0.0f;
	
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter(Collider col)
	{
		SpawnObject(col);
	}
	
	void OnTriggerStay(Collider col)
	{
		SpawnObject(col);
	}
	
	void SpawnObject(Collider col)
	{
		if(cooldownTimer <= 0)
		{
			GameObject player = GameObject.FindGameObjectWithTag("Player");
			if(player != null)
			{
				Instantiate(spawnObject,col.ClosestPointOnBounds(player.transform.position),Quaternion.identity);
				cooldownTimer = cooldown;
			}
		}
		else
		{
			cooldownTimer -= Time.deltaTime;
		}
	}
}
