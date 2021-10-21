using UnityEngine;
using System.Collections;

public class WARNING : MonoBehaviour 
{
	public Transform WARNINGmsg;
	public float MessageSpeed = 10.0f;
	public float MessageDuration = 4.0f;
	public Vector2 scrollSpeed = Vector2.one;
	private float newScale;
	private Material warning;
	private Vector3 targetvector;

	// Use this for initialization
	void Start ()
	{
		warning = GetComponent<Renderer>().material;
	}
	
	// Update is called once per frame
	void Update ()
	{
		warning.mainTextureOffset += scrollSpeed *5 *Time.deltaTime;
	}

	void wait()
	{
		StartCoroutine("MessageStart");
	}
	IEnumerator MessageStart()
	{
		float t = 0.0f;
		while(t < MessageDuration)
		{

			targetvector = new Vector3 (1,WARNINGmsg.localScale.y,WARNINGmsg.localScale.z);
			Vector3 newScale = WARNINGmsg.localScale;
			newScale.x = Mathf.Lerp(WARNINGmsg.localScale.x,targetvector.x,MessageSpeed*Time.deltaTime);
			WARNINGmsg.localScale = newScale;
			t += Time.deltaTime;
			yield return null;
		}
		Debug.Log("MSG Coroutine");
		messageEnd();
	}

		void messageEnd ()
	{
		Vector3 newScale = WARNINGmsg.localScale;
		newScale.x = Mathf.Lerp(targetvector.x,WARNINGmsg.localScale.x,MessageSpeed*Time.deltaTime);
		WARNINGmsg.localScale = newScale;
		WARNINGmsg.gameObject.SetActive(false);
	}

}
