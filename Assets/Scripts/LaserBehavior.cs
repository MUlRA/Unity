using UnityEngine;
using System.Collections;

public class LaserBehavior : MonoBehaviour 
{
	public float damageAmount = 1.0f;
	public GameObject Spark;
	public string [] tags;
	public string collisionTag = "Player";



	void OnTriggerEnter (Collider col)
	{
		foreach( string tag in tags)
		{
			if(col.tag == tag)
			{
				Instantiate(Spark,col.ClosestPointOnBounds(transform.position),Quaternion.identity);
			}
		}
		if (col.tag == collisionTag)
		{
			HealthNBoosterManager.Instance.DamagedPlayer(damageAmount);
			Instantiate(Spark,col.ClosestPointOnBounds(transform.position),Quaternion.identity);
		}
	}
	
}	
