using UnityEngine;
using System.Collections;

public class BeamBehaviour : MonoBehaviour
{
	public float delay = 1.0f;
	public GameObject Spark;
	public string [] tags;

	
	// Use this for initialization
	void Start ()
	{
		Invoke("DestroyNow",delay);
	}
	void DestroyNow()
	{
		Destroy(gameObject);
	}
	
	void OnTriggerEnter (Collider col)
	{
		foreach( string tag in tags)
		{
			if(col.tag == tag)
			{

				Instantiate(Spark,transform.position,Random.rotation);
				Destroy(gameObject);
			}
		}
	}

}
