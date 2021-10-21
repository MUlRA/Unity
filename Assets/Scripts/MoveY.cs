using UnityEngine;
using System.Collections;

public class MoveY : MonoBehaviour
{
	public float speed = 500.0f;

	// Update is called once per frame
	void Update ()
	{
		transform.position += transform.up*-speed*Time.deltaTime;

	}
}
