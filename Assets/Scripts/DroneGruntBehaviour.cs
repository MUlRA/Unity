using UnityEngine;
using System.Collections;

public class DroneGruntBehaviour : MonoBehaviour
{
	public string [] tags;
	public GameObject gib;
	public GameObject Smoke;
	public Rigidbody Laser;
	public float SelfdestructDelay = 5.0f;
	public float velocity = 10.0f;
	public float Wait = 5.0f;
	public float damageAmount = 1.0f;
	public float RotationSpeed = 10f;
	public float Health = 1.0f;
	public float BurstDelay =1.0f;
	public string collisionTag = "Player";
	public string BombTag = "Bomb";
	public Transform cannon;
	private Vector3 originPosition;
	private Vector3 goalPosition;
	private Vector3 currentPosition;
	private GameObject target;

	public int laserindex = 3;

	public float movementSpeed = 1.0f;
	public float movementTime = 1.0f;
	public float movementRadius = 10.0f;
	public float movementDelay = 5.0f;
	
	// Use this for initialization
	void Start ()
	{
		originPosition = transform.position;
		Invoke("Death",SelfdestructDelay);
		Invoke("MoveAround",movementDelay);
		Invoke ("Cooldown", BurstDelay);
	}
	
	// Update is called once per frame
	void Update ()
	{
		GameObject target = GameObject.FindGameObjectWithTag("Player");
		if (target != null)
		{		
			//Creepily look at the player
			transform.rotation = Quaternion.LookRotation (target.transform.position - transform.position);
		}
	}
	void OnTriggerEnter (Collider col)
	{
		foreach( string tag in tags)
		{
			if(col.tag == tag)
			{
				Health -= Weapons.Instance.Power;

					if (Health <= 0)
				{
						//Death animation
						Invoke ("Death",0);
				}
			}
		}
		if(col.tag == collisionTag)
		{
			HealthNBoosterManager.Instance.DamagedPlayer(damageAmount);
			//shake
		}
		if (col.tag == BombTag) 
		{
			Invoke ("Death",0);
		}
	}
	void MoveAround()
	{

		Vector3 movementVector =  Random.insideUnitSphere *movementRadius;
		movementVector.z = Mathf.Clamp (movementVector.z, 600f, 300f);
		movementVector.x = Mathf.Clamp (movementVector.x, -1000f, 1000f);
		movementVector.y = Mathf.Clamp (movementVector.y, -600f, 500f);
		goalPosition = originPosition+movementVector;
		currentPosition = transform.position;
		StartCoroutine("MoveCorountine");
	}
	IEnumerator MoveCorountine()
	{
		float t = 0.0f;
		while(t < movementTime)
		{
			t += Time.deltaTime;  
			transform.position = Vector3.Lerp(currentPosition,goalPosition,t/movementTime);
			yield return null;
		}
		Invoke("MoveAround",movementDelay);
	}
	IEnumerator Firing()
	{
		Debug.Log("Firing");
		Transform spawnPoint = cannon;
		{
			for (int i = 0; i < laserindex; i++)
			{
				Rigidbody newLaser = Instantiate (Laser, spawnPoint.position, spawnPoint.rotation) as Rigidbody;
				newLaser.AddForce(transform.forward*velocity,ForceMode.VelocityChange);	
				yield return new WaitForSeconds(1);
			}
			yield return null;
		}
		Invoke ("Cooldown", BurstDelay);
	}
	void Cooldown ()
	{
		Debug.Log("LaserCooldown");
		StartCoroutine ("Firing");
	}

	void Death()
	{

		Instantiate(gib,transform.position,Random.rotation);
		Instantiate(Smoke,transform.position,Smoke.transform.rotation);
		Destroy(gameObject);
	}
}
