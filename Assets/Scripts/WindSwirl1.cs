using UnityEngine;
using System.Collections;

public class WindSwirl1 : MonoBehaviour 
{
	public float amplitude = 1.0f;
	public float verticalspeed= 1.0f;
	public float horizontalspeed= 1.0f;
	public float forwardspeed= 1.0f;
	public float deathtimer = 20.0f;
	private Vector3 TempPos;
	// Use this for initialization
	void Start ()
	{
		Invoke ("death", deathtimer);
		TempPos = transform.position;
	}
	
	// Update is called once per frame
	void Update ()
	{ 
		TempPos.z = forwardspeed;
		TempPos.x = horizontalspeed;
		TempPos.y = Mathf.Sin (Time.realtimeSinceStartup * verticalspeed) * amplitude;
		transform.position += TempPos;
	
	}
	void death ()
	{
		Destroy (gameObject);
	}
}
