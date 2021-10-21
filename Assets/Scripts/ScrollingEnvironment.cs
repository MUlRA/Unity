using UnityEngine;
using System.Collections;

public class ScrollingEnvironment : MonoBehaviour
{
	private Material clouds;
	public Vector2 scrollSpeed = Vector2.one;

	// Use this for initialization
	void Start () 
	{
		clouds = GetComponent<Renderer>().material;
	}
	
	// Update is called once per frame
	void Update ()
	{

		if (Input.GetButton ("Jump") && HealthNBoosterManager.Instance.isTired == false )
		{
				clouds.mainTextureOffset += scrollSpeed *5 *Time.deltaTime;
		}
			else 
		{
				clouds.mainTextureOffset += scrollSpeed *Time.deltaTime;
		}
	}

}
