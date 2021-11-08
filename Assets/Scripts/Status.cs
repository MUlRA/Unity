using UnityEngine;
using System.Collections;

public class Status : MonoBehaviour {
	public GameObject Burningsmoke;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		GameObject go = Instantiate (Burningsmoke, transform.position, Quaternion.identity) as GameObject; 
		go.transform.parent = this.transform;
	}
}
