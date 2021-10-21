using UnityEngine;
using System.Collections;

public class BurningSmokeBehaviour : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
	{
		Invoke ("Death",5.0f);
	}
	
	// Update is called once per frame
	void Death ()
	{
		Destroy (gameObject);
	}
}
