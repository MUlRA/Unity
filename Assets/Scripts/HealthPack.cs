using UnityEngine;
using System.Collections;

public class HealthPack : MonoBehaviour
{
	public float healthAmount = 1.0f;
	public string collisionTag = "Player";
	public float movementSpeed = 0.05f;
	public float horizontalspeed= 1.0f;
	private GameObject target;
	private Vector3 originPosition;
	private float lerpValue;
	private Vector3 TempPos;
	
	// Use this for initialization
	void Start ()
	{	
		TempPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () 
	{
		GameObject target = GameObject.FindGameObjectWithTag("Player");
		if (target != null)
		{
			lerpValue += movementSpeed * Time.deltaTime;
			TempPos = Vector3.Lerp (transform.position, target.transform.position, lerpValue);
			transform.position = TempPos;
		}
	}
	void OnTriggerEnter (Collider col)
	{

		if(col.tag == collisionTag)
		{
			HealthNBoosterManager.Instance.RestoreHealth(healthAmount);
			Debug.Log ("Health restore!!");
			Invoke ("death",5.0f);
		}
	}
	void death ()
	{
		Destroy(gameObject);
	}
}
