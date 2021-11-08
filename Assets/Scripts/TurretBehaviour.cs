using UnityEngine;
using System.Collections;

public class TurretBehaviour : MonoBehaviour
{
	public float maxHealth = 40.0f;
	private float currentHealth;
	public GameObject DeathEffect;
	public string collisionTag = "PlayerBullets";

	// Use this for initialization
	void Start () 
	{
		currentHealth = maxHealth;
	}
	
	// Update is called once per frame
	void OnTriggerEnter (Collider col)
	{
	if (col.tag == collisionTag)
		{
			currentHealth -=Weapons.Instance.Power;
			if (currentHealth <= 0)
			{

				Instantiate (DeathEffect,col.ClosestPointOnBounds(transform.position),Quaternion.identity);
				Destroy (gameObject);
			}
		}
	}
}
