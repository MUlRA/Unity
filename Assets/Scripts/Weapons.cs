using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Weapons : MonoBehaviour 
	{
		private static Weapons instance = null;
		public static Weapons Instance

	
		// Use this for initialization
		
	{
		get {return instance;}
	}
	public float Power = 1;
	private float bombAmount = -1.0f;
	public Rigidbody bullet;
	public Rigidbody pellet;
	public Rigidbody bomb;
	public Transform Muzzle;
	public GameObject PelletsSFX;
	public float velocity = 10.0f;
	public float Cooldown;
	public int pelletCount;
	public float Spread;
	public Transform[] lasers;
	List<Quaternion> pellets;

	
	
	
	void Awake()
	{
		if (instance != null && instance != this)
		{
			Destroy(this.gameObject);
			return;

		}
		else
		{
			instance = this;
		}

		foreach (Transform laser in lasers) 
		{
			laser.gameObject.SetActive(false);
		}
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
	void Update ()
	{
		Cooldown -= Time.deltaTime;
		if (Cooldown <= 0)
		{
			Cooldown = 0;
		}
	}
		
		// Use this for initialization
	public void Fire (int LoadoutModifier) 
	{
		Transform spawnPoint = Muzzle;
		if (LoadoutModifier == 0)
		{
			//Default Weapon
			velocity=9000;
			Power = 1;
			Rigidbody newBullet = Instantiate (bullet, spawnPoint.position, spawnPoint.rotation) as Rigidbody;
			newBullet.AddForce (transform.forward * velocity, ForceMode.VelocityChange);

		}
		if (LoadoutModifier == 1) 
		{
			//Scatter Weapon
			if (Cooldown ==0)
			{
				velocity=7000;
				Power = 0.75f;
				Cooldown = 2;
				Instantiate(PelletsSFX, transform.position, transform.rotation);

				Rigidbody newBullet = Instantiate(pellet, spawnPoint.position, spawnPoint.rotation) as Rigidbody;
				newBullet.AddForce(transform.forward * velocity, ForceMode.VelocityChange);
			}
		}
		if (LoadoutModifier == 2) 
		{
			if (Cooldown == 0) 
			{
				velocity=6666;
				Power=2f;
				Cooldown = 1;
				FireLaser ();
			}
		}
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

		}
		Debug.Log("FIRE!!!");
		StartCoroutine("LaserCoroutine");
	}
	
	IEnumerator LaserCoroutine()
	{
		float t = 0.0f;
		while(t < 0.5f)
		{
			foreach(Transform laser in lasers)
			{
				if(laser != null)
				{
					laser.gameObject.SetActive(true);
					Vector3 newScale = laser.localScale;
					newScale.y += -velocity*Time.deltaTime;
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
		Debug.Log ("Laser on cooldown");
	}
	public void FireBomb()
	{
		Rigidbody newBomb = Instantiate (bomb, transform.position, transform.rotation) as Rigidbody;
		newBomb.AddForce (transform.forward * 2000, ForceMode.VelocityChange);
		HealthNBoosterManager.Instance.Bombs(bombAmount);
	}	
}
