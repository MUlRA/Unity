using UnityEngine;
using System.Collections;

public class DestroyAfterDelay : MonoBehaviour
{
	public float delay = 3.0f;

	// Use this for initialization
	void Start ()
	{
		Invoke("DestroyNow",delay);
	}
	
	void DestroyNow()
	{
		Destroy(gameObject);
	}
}
