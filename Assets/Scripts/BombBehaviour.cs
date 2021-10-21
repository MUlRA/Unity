using UnityEngine;
using System.Collections;

public class BombBehaviour : MonoBehaviour 
{
	public float timer = 5.0f;
	public GameObject IonExplosion;
	public string collisionTag = "Player";
	// Use this for initialization
	void Start ()
	{

		Invoke ("Death", timer);
	}
	void OnTriggerEnter (Collider col)
	{
		if (col.tag == "Player" ) 
		{
			return;
			}
		else Invoke ("Death", 0);
		}
	void Death()
	{
		Instantiate (IonExplosion, transform.position, IonExplosion.transform.rotation);
		Destroy (gameObject);
	}
}
