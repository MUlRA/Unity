using UnityEngine;
using System.Collections;

public class AA_MissileLauncher : MonoBehaviour 
{
	public string [] tags;
	public GameObject gib;
	public string collisionTag = "Player";
	public string BombTag = "Bomb";
	public float verticalspeed= 1.0f;
	public float Height = 400.0f;
	public float RotSpeed= 1.0f;
	public float amplitude = 1.0f;
	public float Health = 10.0f;
	public float BurstDelay =1.0f;
	public float startShooting = 10.0f;
	public float stopShooting = 10.0f;
	public Transform MissileSpawnPoints;
	public float selfdestruct =  15.0f;
	public float damageAmount = 1.0f;  
	public GameObject Smoke;
	public GameObject Missile; 
	public int missileIndex = 3;
	private Vector3 TempPos;
	private Vector3 oscillating;
	private float time;
	public float angle;
	public float period;

	// Use this for initialization
	void Start () 
	{
		if(Random.Range(0f, 1f) > 0.5f)
		{
			transform.position = new Vector3 (-1000,transform.position.x,transform.position.z);
		}		
		else
		{
			transform.position = new Vector3 (1000,transform.position.x,transform.position.z);
		}

		Invoke ("SelfDestruct", selfdestruct);
		Invoke ("MissileCooldown", BurstDelay);

	}
	void FixedUpdate ()
	{
		//Rotate
		time = time + Time.deltaTime;
		float phase = Mathf.Sin(time / period);
		transform.rotation = Quaternion.Euler( new Vector3(0, phase * angle, 0));
		//Hovering

		TempPos = transform.position;
		TempPos.y = oscillating.y - Height;
		oscillating.y = Mathf.Sin (Time.realtimeSinceStartup * verticalspeed) * amplitude;
      	transform.position = TempPos;
		}

	IEnumerator MissileCoroutine()
	{
		Debug.Log("MissileCoroutinestart");
		Transform spawnPoint = MissileSpawnPoints;
		{
			for (int i = 0; i < missileIndex; i++)
			{
				Instantiate (Missile, spawnPoint.position, spawnPoint.rotation);
		    	yield return new WaitForSeconds(1);
			}
			yield return null;
		}
		Invoke ("MissileCooldown", BurstDelay);
	}
	void MissileCooldown ()
	{
		StartCoroutine ("MissileCoroutine");
		}

	void OnTriggerEnter (Collider col)
	{
		foreach( string tag in tags)
		{
			if(col.tag == tag)
			{
				Health -= HealthNBoosterManager.Instance.Power;
				if (Health <= 0)
				{
					//Death animation
					Invoke ("SelfDestruct",0);
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
			Invoke ("SelfDestruct",0);
		}
	}

	void SelfDestruct()
	{
		Instantiate(gib,transform.position,Random.rotation);
		Instantiate (Smoke,transform.position, Smoke.transform.rotation);
		Destroy (gameObject);
	}
}
