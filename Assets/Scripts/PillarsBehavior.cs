using UnityEngine;
using System.Collections;

public class PillarsBehavior : MonoBehaviour
{
	public bool destroySelf = true;
	public GameObject [] destroyObjects;
	public float damageAmount = 1.0f;
	public float delay = 1.0f;
	public string collisionTag = "Player";
	public bool destroyOnTrigger = false;

	// Use this for initialization
	void Start ()
	{
		Invoke("DestroyNow",delay);
	}

	// Use this for initialization
	void OnTriggerEnter(Collider col)
	{

		if(col.tag == collisionTag)
		{
			HealthNBoosterManager.Instance.DamagedPlayer(damageAmount);
			//shake
		}
		if(destroyOnTrigger)
		{
			Destroy (gameObject);
		}
	}
	void DestroyNow()
	{
		Destroy(gameObject);
	}


}