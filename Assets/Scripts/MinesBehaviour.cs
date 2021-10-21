using UnityEngine;
using System.Collections;

public class MinesBehaviour : MonoBehaviour 
{
	public float Health = 2.0f;
	public float selfdestruct =  10.0f;
	public float damageAmount = 1.0f;
	public GameObject Explosion;
	public GameObject MinesGibs;
	public string [] tags;
	public string collisionTag = "Player";
	public string BombTag = "Bomb";
	private GameObject target;

	// Use this for initialization
	void Start ()
	{
		Invoke ("SelfDestruct",selfdestruct);
		//GameObject target = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update ()
	{
		//Creepily look at the player
		//transform.rotation = Quaternion.LookRotation(target.transform.position - transform.position);
	}
	void OnTriggerEnter (Collider col)
	{
		foreach( string tag in tags)
		{
			if(col.tag == tag)
			{
				Health -= 1.0f;
				if (Health <= 0)
				{
					//Death animation
					Invoke ("SelfDestruct",0);
				}
			}
		}
		if(col.tag == collisionTag)
		{
			Invoke ("SelfDestruct",0);
			HealthNBoosterManager.Instance.DamagedPlayer(damageAmount);
			//shake
			
		}
		if (col.tag == BombTag) 
		{
			Invoke ("SelfDestruct",0);
		}
	}
	void SelfDestruct()
	{
		
		Instantiate (Explosion, transform.position, Explosion.transform.rotation);
		Destroy (gameObject);
	}
}

