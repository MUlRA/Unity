using UnityEngine;
using System.Collections;

public class SecurityBehaviour : MonoBehaviour
{
	public string [] tags;
	public GameObject gib;
	public GameObject Smoke;
	public float delay = 1.0f;
	public float damageAmount = 1.0f;
	public string collisionTag = "Player";
	public string BombTag = "Bomb";
	private float shake_decay;
	private float shake_intensity;
	public float Health = 1.0f;
	public float rotationSpeed = 30.0f;
	public float movementSpeed = 10.0f;
	public float forwardspeed = 10.0f;
	public float verticalspeed= 1.0f;
	public float horizontalspeed= 1.0f;
	public float amplitude = 1.0f;
	private GameObject target;
	private Vector3 originPosition;
	private Vector3 goalPosition;
	private Quaternion originRotation;
	private float time;
	private Vector3 TempPos;

	// Use this for initialization
	void Start ()
	{

		TempPos = transform.position;
		//Invoke("Move");
		Invoke("Death",delay);
	}
	
	// Update is called once per frame
	void Update () 
	{
		GameObject target = GameObject.FindGameObjectWithTag("Player");
		if (target != null)
		{
			TempPos.x = Mathf.Lerp (transform.position.x, target.transform.position.x, horizontalspeed * Time.deltaTime);
			TempPos.y = Mathf.Lerp (transform.position.y, target.transform.position.y, verticalspeed * Time.deltaTime);
			transform.position = TempPos;
		}
		transform.Rotate (Vector3.up * rotationSpeed * Time.deltaTime);
		originRotation = transform.rotation;
		originPosition = transform.position;
	

	}
	//shake
	 void Shake ()
	{
		
		originPosition = transform.position;
		originRotation = transform.rotation;
		shake_intensity = 0.05f;
		shake_decay = 0.05f;
	}
	void OnTriggerEnter (Collider col)
	{
		foreach( string tag in tags)
		{
			if(col.tag == tag)
			{
				Health -= HealthNBoosterManager.Instance.Power;
				Invoke ("Shake",0);
				if (shake_intensity > 0)
				{
					transform.position = originPosition + Random.insideUnitSphere * shake_intensity;
					transform.rotation = new Quaternion(
						originRotation.x + Random.Range (-shake_intensity,shake_intensity) * .2f,
						originRotation.y + Random.Range (-shake_intensity,shake_intensity) * .2f,
						originRotation.z + Random.Range (-shake_intensity,shake_intensity) * .2f,
						originRotation.w + Random.Range (-shake_intensity,shake_intensity) * .2f);
					shake_intensity -= shake_decay;
					{
						if (Health <= 0)
						{
							Invoke ("Shake",0);
							//Death animation
							Invoke ("Death",0.25f);
						}
					}
				}
		   }
		}
		if(col.tag == collisionTag)
		{
			HealthNBoosterManager.Instance.DamagedPlayer(damageAmount);
		}
		if (col.tag == BombTag) 
		{
			Invoke ("Death",0);
		}
	}

	void Death()
	{
		Instantiate(gib,transform.position,Random.rotation);
		Instantiate(Smoke,transform.position,Smoke.transform.rotation);
		Destroy(gameObject);
	}
}
