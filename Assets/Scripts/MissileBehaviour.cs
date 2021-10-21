using UnityEngine;
using System.Collections;

public class MissileBehaviour : MonoBehaviour 
{
	public string [] tags;
	public string collisionTag = "Player";
	public string BombTag = "Bomb";
	public float speed = 1.0f;
	public float Health = 2.0f;
	public float turnSpeed = 30.0f;
	public float startSeeking = 1.0f;
	public float stopSeeking = 4.0f;
	public float selfdestruct =  10.0f;
	public float damageAmount = 1.0f;
	public GameObject Smoke;
	private float t = 0.0f;
		
	// Use this for initialization
	void Start ()
	{
		Invoke ("SelfDestruct", selfdestruct);
	}
	
	// Update is called once per frame
	void Update ()
	{
			t += Time.deltaTime;
			if (t > startSeeking && t < stopSeeking)
		{
				transform.parent = null;
				//Find player and figure out how to rotate towards him/her
				GameObject player = GameObject.FindGameObjectWithTag ("Player");
				if (player != null) 
			{
				//LookAt
				Quaternion newRotation = Quaternion.LookRotation (player.transform.position - transform.position);
				//Turn
				transform.rotation = Quaternion.RotateTowards (transform.rotation, newRotation, turnSpeed * Time.deltaTime);
			}
		}
		//Go forward
		transform.position += transform.forward * speed * Time.deltaTime;
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

			Instantiate (Smoke, transform.position, Smoke.transform.rotation);
			Destroy (gameObject);
	}
}