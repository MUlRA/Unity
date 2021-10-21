using UnityEngine;
using System.Collections;

public class IonExplosionBehaviour : MonoBehaviour 
{
	public float fadeSpeed = 3.0f;
	public float rotationSpeed = 10.0f;
	public float ExplosionDuration = 3.0f;
	public float ExplosionSpeed = 10.0f;
	public float dyingspeed = 10.0f;
	public float LightFade = 1.0f;
	public float LightLit = 1.0f;
	public GameObject Ion;
	private Renderer IonR;
	private Light IonL; 


	// Use this for initialization
	void Start () 
	{
		IonL = Ion.GetComponent<Light> ();
		IonR = Ion.GetComponent<Renderer> ();
		StartCoroutine("Explosion");
	}
	void Update ()
	{
		transform.Rotate (Vector3.up * rotationSpeed * Time.deltaTime);
	}

	IEnumerator Explosion()
	{
	
		float t = 0.0f;
		while (t < ExplosionDuration) 
		{
			IonL.intensity = Mathf.Lerp (IonL.intensity,15f,LightLit);
			IonL.range = Mathf.Lerp (IonL.range,9000f,LightFade * Time.deltaTime);
			Vector3 newScale = transform.localScale;
			newScale.z += ExplosionSpeed * Time.deltaTime;
			newScale.y += ExplosionSpeed * Time.deltaTime;
			newScale.x += ExplosionSpeed * Time.deltaTime;
			transform.localScale = newScale;
			CameraFollow.Instance.Shake ();
			t += Time.deltaTime;
			yield return null;
		}
		Debug.Log ("BOOOOOOM!!!");

		StartCoroutine ("Implosion",2.0f);
	}

	IEnumerator Implosion()
	{
		float t = 0.0f;
		while (t < ExplosionDuration) 
		{
			IonL.intensity = Mathf.Lerp (IonL.intensity,0f,LightFade * Time.deltaTime);
			IonL.range = Mathf.Lerp (IonL.range,0f,LightLit *t* Time.deltaTime);
			//fucking alpha bs
			IonR.material.color = new Color(IonR.material.color.r, IonR.material.color.g, IonR.material.color.b, Mathf.Lerp(1,0, fadeSpeed*t));
			Vector3 newScale = transform.localScale;
			newScale = Vector3.Lerp(transform.localScale,new Vector3 (0,0,0), Time.deltaTime *dyingspeed );
			transform.localScale = newScale;

			t += Time.deltaTime;
			yield return null;
		}
		Destroy (gameObject);

	}
}
