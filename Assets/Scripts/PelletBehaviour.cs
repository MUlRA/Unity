using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PelletBehaviour : MonoBehaviour
{
	public float Range = 3.0f;
	public float Spread = 1f;
	public float delay = 1.0f;
	public GameObject Spark;
	public string[] tags;
	void Start()
	{
		StartCoroutine("Shot");
		Invoke("DestroyNow", delay);
	}
	void DestroyNow()
	{
		Destroy(gameObject);
	}

	void OnTriggerEnter(Collider col)
	{
		foreach (string tag in tags)
		{
			if (col.tag == tag)
			{

				Instantiate(Spark, transform.position, Random.rotation);
			}
		}
	}

	IEnumerator Shot()
	{

		float t = 0.0f;
		while (t < Range)
		{

			Vector3 newScale = transform.localScale;
			newScale.z += Spread * Time.deltaTime;
			newScale.y += Spread * Time.deltaTime;
			newScale.x += Spread * Time.deltaTime;
			transform.localScale = newScale;
			t += Time.deltaTime;
			yield return null;
		}
		Debug.Log("BANG!!!");

	}
}
