using UnityEngine;
using System.Collections.Generic;

public class WeakPointBehaviour : MonoBehaviour
{
	public float maxHealth = 40.0f;
	public GameObject[] Turrets;
	public float currentHealth;
	public GameObject Unlock;
	public GameObject Smoke;
	public GameObject DeathEffect;
	public GameObject Explosion;
	public GameObject Reticle;
	public string collisionTag = "PlayerBullets";
	public GameObject BOSS;
	public DroneDestroyerBehaviour droneDestroyerBehaviour;
	private GameObject go;
	private GameObject HeadsUp;
	private Rigidbody Destroyer;

	// Use this for initialization
	void Start () 
	{
		droneDestroyerBehaviour = BOSS.GetComponent<DroneDestroyerBehaviour> ();
		HeadsUp = GameObject.FindGameObjectWithTag ("HUD");
		Destroyer = BOSS.GetComponent<Rigidbody> ();
		currentHealth = maxHealth;
	}
	
	// Update is called once per frame
	void OnTriggerEnter (Collider col)
	{
		//lock until turrets are down
		if (Turrets [0] == null && Turrets [1] == null && Turrets [2] == null) 
		{
			Instantiate (Unlock, transform.position, Quaternion.identity);
			if (col.tag == collisionTag) 
			{
				currentHealth -= Weapons.Instance.Power;
				if (currentHealth <= 0) 
				{
					droneDestroyerBehaviour.enabled = false;
					Instantiate (DeathEffect, col.ClosestPointOnBounds (transform.position), Quaternion.identity);
					GameObject go = Instantiate (Smoke, transform.position, Quaternion.identity) as GameObject; 
					go.transform.parent = BOSS.transform;
					HealthNBoosterManager.Instance.isCutscene = true;
					HeadsUp.SetActive (false);
					Reticle.SetActive (false);
					Invoke ("Death", 3f);
				}
			}
		} 
		else 
			{
			Instantiate (Unlock, transform.position, Quaternion.identity);
			}
	}

	void Death()
	{
		Destroyer.isKinematic = false;
		//pushdown
		Destroyer.AddForce(BOSS.transform.up*-100,ForceMode.VelocityChange);
		Destroyer.AddTorque(BOSS.transform.forward*5f,ForceMode.Acceleration);
		Invoke ("Explode",4f);
		
		
	}
	void Explode()
	{
		Instantiate (Explosion, BOSS.transform.position, Quaternion.identity);
		Destroy(BOSS,2f);
	}
}
