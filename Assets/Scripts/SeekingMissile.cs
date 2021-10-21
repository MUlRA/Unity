using UnityEngine;
using System.Collections;

public class SeekingMissile : MonoBehaviour
{
	public float speed = 1.0f;
	public float turnSpeed = 30.0f;
	public float startSeeking = 1.0f;
	public float stopSeeking = 4.0f;
	
	private float t = 0.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		t += Time.deltaTime;
		
		if(t > startSeeking && t < stopSeeking)
		{
			transform.parent = null;
			//Find player and figure out how to rotate towards him/her
			GameObject player = GameObject.FindGameObjectWithTag("Player");
			if(player != null)
			{
				Quaternion newRotation = Quaternion.LookRotation(player.transform.position - transform.position);
			
				//Turn
				transform.rotation = Quaternion.RotateTowards(transform.rotation,newRotation,turnSpeed*Time.deltaTime);
			}
		}
		
		//Go forward
		transform.position += transform.forward*speed*Time.deltaTime;
	}
}
