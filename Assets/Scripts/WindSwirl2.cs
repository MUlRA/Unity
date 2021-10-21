using UnityEngine;
using System.Collections;

public class WindSwirl2 : MonoBehaviour 
{
	public float amplitude = 1.0f;
	public float verticalspeed= 1.0f;
	public float horizontalspeed= 1.0f;
	public float forwardspeed= 1.0f;
	public float Speed = 10.0f;
	public float delay = 10.0f;
	public float deathtimer = 20.0f;
	private float timecounter;
	private Vector3 TempPos;

	// Use this for initialization
	void Start ()
	{
		Invoke ("death", deathtimer);
		TempPos = transform.position;
		Invoke ("loop", 2.0f);
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

	void loop()
	{
		timecounter += Time.deltaTime * Speed;
		TempPos.z = Mathf.Cos (timecounter)*horizontalspeed;
		TempPos.y = Mathf.Sin (Time.realtimeSinceStartup * verticalspeed) * amplitude;
		amplitude = 1.0f;
		verticalspeed=1.0f;
		horizontalspeed= 1.0f;
		Speed = 1.0f;
		Invoke ("Normal", delay);
	}
	void Normal()
	{
		TempPos.z += horizontalspeed;
		TempPos.y = Mathf.Sin (Time.realtimeSinceStartup * verticalspeed) * amplitude;
		transform.position = TempPos;
		amplitude = 5.0f;
		verticalspeed = 5.0f;
		horizontalspeed = 5.0f;
		Speed = 10.0f;
	}
}
