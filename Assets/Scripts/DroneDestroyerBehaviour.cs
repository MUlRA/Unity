using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DroneDestroyerBehaviour : MonoBehaviour
{
	private static DroneDestroyerBehaviour instance = null;
	public static DroneDestroyerBehaviour Instance

		
	{
		get {return instance;}
	}
	void Awake()
	{
		if (instance != null && instance != this)
		{		
			Destroy (this.gameObject);
			return;
			
		} 
		else 
		{
			instance = this;
		}
		
	}
	public GameObject[] cannonParent;
	public Transform[] lasers;
	public Transform[] MissileSpawnPoints;
	public GameObject Missile;
	public float MissileDelay = 1.0f;
	public float laserRotationSpeed = 1.0f;
	public float laserShotDelay = 5.0f;
	public float laserChargeTime = 2.0f;
	public float laserSpeed = 1.0f;
	public float laserDuration = 1.0f;
	//public GameObject laserChargeSound;
	//public GameObject laserFireSound;
	//private List<GameObject> laserChargeInstances = new List<GameObject>();
	//private List<GameObject> laserFireInstances = new List<GameObject>();
	public float movementSpeed = 1.0f;
	public float movementTime = 1.0f;
	public float movementRadius = 10.0f;
	public float movementDelay = 5.0f;
	private Vector3 originPosition;
	private Vector3 goalpos;
	private Vector3 goalPosition;
	private Vector3 currentPosition;
	private Quaternion originRotation;

	private int missileIndex = 0;
	

	// Use this for initialization
	void Start ()
	{

		originPosition = transform.position;
		Invoke("ChargeLaser", laserShotDelay);
		Invoke ("ShootMissile", MissileDelay);
		Invoke("MoveBoss",movementDelay);
	}
	void ShootMissile ()
	{
		Transform spawnPoint = MissileSpawnPoints [missileIndex];
		missileIndex++;
		if (missileIndex >= MissileSpawnPoints.Length)
	   {
			missileIndex = 0;
		}
		Instantiate (Missile, spawnPoint.position, spawnPoint.rotation);
		Invoke ("ShootMissile", MissileDelay);
	}

	void MoveBoss()
	{
		Vector3 movementVector = Random.insideUnitSphere*movementRadius;
		movementVector.y = 0.0f;
		goalPosition = originPosition+movementVector;
		currentPosition = transform.position;
		StartCoroutine("ActuallyMoveBoss");
	}
	
	IEnumerator ActuallyMoveBoss()
	{
		float t = 0.0f;
		while(t < movementTime)
		{
			t += Time.deltaTime;
			transform.position = Vector3.Lerp(currentPosition,goalPosition,t/movementTime);
			yield return null;
		}
		Invoke("MoveBoss",movementDelay);
	}
	

	void ChargeLaser()
	{
		//Play some cool effect for charging, maybe some sounds, you know stuff like that.  Particles. Things.  ETC.
		foreach(Transform laser in lasers)
		{
			if(laser != null)
			{
				laser.gameObject.SetActive(true);
				//laserChargeInstances.Add(Instantiate(laserChargeSound,laser.position,laser.rotation) as GameObject);
				
			}
		}
		
		Debug.Log("IMMA CHARGIN' MY LASER!");
		Invoke ("FireLaser",laserChargeTime);
	}
	
	void FireLaser()
	{
		//foreach(GameObject soundObject in laserChargeInstances)
		{
			//Destroy(soundObject);
		}
		//laserChargeInstances.Clear();
		
		foreach(Transform laser in lasers)
		{
			/*laserFireInstances.Add(Instantiate(laserFireSound,laser.position,laser.rotation) as GameObject);
	*/
			Debug.Log("Firelaser");
		}
		Debug.Log("FIRE!!!");
		StartCoroutine("LaserCoroutine");
	}
	
	IEnumerator LaserCoroutine()
	{
		float t = 0.0f;
		while(t < laserDuration)
		{
			foreach(Transform laser in lasers)
			{
				if(laser != null)
				{
					Vector3 newScale = laser.localScale;
					newScale.y += laserSpeed*Time.deltaTime;
					laser.localScale = newScale;
				}
			}
			t += Time.deltaTime;
			yield return null;
		}
		Debug.Log("Laser Coroutine");
		LaserCooldown();
	}
	
	void LaserCooldown()
	{
		//foreach(GameObject soundObject in laserFireInstances)
		{
			//Destroy(soundObject);
		}
		//laserFireInstances.Clear();
		
		//Reset lasers to initial values
		foreach(Transform laser in lasers)
		{
			if(laser != null)
			{
				Vector3 newScale = laser.localScale;
				newScale.y = 0;
				laser.localScale = newScale;
				laser.gameObject.SetActive(false);

			}
		}
		Debug.Log("Off Cooldown");
		Invoke("ChargeLaser", laserShotDelay);
	}
	
	void MoveIn()
	{
		Vector3 movementVector = new Vector3();
		movementVector.x = transform.localPosition.x;
		movementVector.y = transform.localPosition.y;
		movementVector.z = 2500.0f;
		goalpos = currentPosition + movementVector;
		currentPosition = transform.localPosition;
		StartCoroutine ("MovingIn");
	}
	IEnumerator MovingIn()
	{
		float t = 0.0f;
		while(t < movementTime)
		{
			t += Time.deltaTime;
			transform.localPosition = Vector3.Lerp(transform.localPosition,goalpos, t/movementTime);
			yield return null;
		}
	}
	// Update is called once per frame
	void Update ()
	{
		GameObject target = GameObject.FindGameObjectWithTag("Player");
		if(target != null)
		{
			//Creepily look at the player
			foreach (GameObject turret in cannonParent)
			if(turret != null)
			{
			turret.transform.rotation = Quaternion.RotateTowards (turret.transform.rotation,Quaternion.LookRotation(target.transform.position - turret.transform.position),laserRotationSpeed*Time.deltaTime);
			}

		}

	}

}
