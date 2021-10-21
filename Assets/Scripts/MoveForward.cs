using UnityEngine;
using System.Collections;

public class MoveForward : MonoBehaviour
{
	public float speed = 500.0f;

	// Update is called once per frame
	void Update ()
	{
		transform.position += transform.forward*speed*Time.deltaTime;
		{
			if (Input.GetButton ("Jump")&& HealthNBoosterManager.Instance.isTired == false )
				{
				speed = 1500.0f;
				}
			else 
				{
				speed = 500.0f;
				}
		}
	}
}
