using UnityEngine;
using System.Collections;

public class EnemyBeamBehaviour : MonoBehaviour 
{
	public float delay = 1.0f;
	public GameObject Spark;
	public string [] tags;
	public float damageAmount = 1.0f;
	public string collisionTag = "Player";
	// Use this for initialization
	void Start ()
	{
		Invoke("DestroyNow",delay);
	}
	void DestroyNow()
	{
		Destroy(gameObject);
	}
	
	void OnTriggerEnter (Collider col)
	{
		foreach( string tag in tags)
		{
			if(col.tag == tag)
			{
				Instantiate(Spark,transform.position,Random.rotation);
				Invoke ("DestroyNow",0);

			}
		}
		if(col.tag == collisionTag)
		{
			Instantiate(Spark,transform.position,Random.rotation);
			HealthNBoosterManager.Instance.DamagedPlayer(damageAmount);
			Invoke ("DestroyNow",0);
		}
	}
}