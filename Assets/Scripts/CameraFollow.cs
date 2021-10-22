using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
	private static CameraFollow instance = null;
	public static CameraFollow Instance
		// Use this for initialization
		
	{
		get {return instance;}
	}
	void Awake()
	{
		if (instance != null && instance != this)
		{		
			Destroy (this.gameObject);
			return;
			
		} 
		else 
		{
			instance = this;
		}
		
	}
	public Transform objectToFollow;
	public Vector2 movementRatio = Vector2.one;
	public float tiltAngle = 1.0f;
	public float Boost = 1.0f;
	public int score = 0; 
	private float shake_decay;
	private float shake_intensity;
	private Vector3 originPosition;
	private Quaternion originRotation;





	// Use this for initialization
	void Start () 
	{
		originRotation = transform.rotation;
		originPosition = transform.position;
	}
	
	// Update is called once per frame
	void LateUpdate ()
	{
		{
			if (Input.GetButton ("Jump") && HealthNBoosterManager.Instance.isTired == false )
			{
				Shake ();
				if (Camera.main.fieldOfView < 90)
				{
					Camera.main.fieldOfView = Mathf.Lerp (Camera.main.fieldOfView,90,10*Time.deltaTime);
				}
			}
			else
			{

				if (Camera.main.fieldOfView > 60)
				{
					Camera.main.fieldOfView = Mathf.Lerp (Camera.main.fieldOfView,60,10*Time.deltaTime);
				}


			}

		}
		float tiltAroundZ = Input.GetAxis("Horizontal") * -tiltAngle;
		float tiltAroundX = Input.GetAxis("Vertical") * tiltAngle;
		Quaternion target = Quaternion.Euler( tiltAroundX, 0, tiltAroundZ);
		transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime);


		Vector3 newPosition = objectToFollow.position;
		newPosition.x *= movementRatio.x;
		newPosition.y *= movementRatio.y;
		newPosition.z = transform .position.z;
		transform .position = newPosition;

	}

	//shake
	public void Shake ()
	{
		Debug.Log ("SHAKE!!");
		originPosition = transform.position;
		originRotation = transform.rotation;
		shake_intensity = 0.025f;
		shake_decay = 0.02f;
		if (shake_intensity > 0)
		{
			transform.position = originPosition + Random.insideUnitSphere * shake_intensity;
			transform.rotation = new Quaternion(
				originRotation.x + Random.Range (-shake_intensity,shake_intensity) * .2f,
				originRotation.y + Random.Range (-shake_intensity,shake_intensity) * .2f,
				originRotation.z + Random.Range (-shake_intensity,shake_intensity) * .2f,
				originRotation.w + Random.Range (-shake_intensity,shake_intensity) * .2f);
			shake_intensity -= shake_decay;
		}
	}
	public void IsHit (float damageAmount)
	{
		originPosition = transform.position;
		originRotation = transform.rotation;
		shake_intensity = damageAmount/3;
		shake_decay = 0.02f;
		Debug.Log ("Is hit, shake for" +shake_intensity +"!!");

		if (shake_intensity > 0)
		{
			transform.position = originPosition + Random.insideUnitSphere * shake_intensity;
			transform.rotation = new Quaternion(
				originRotation.x + Random.Range (-shake_intensity,shake_intensity) * .2f,
				originRotation.y + Random.Range (-shake_intensity,shake_intensity) * .2f,
				originRotation.z + Random.Range (-shake_intensity,shake_intensity) * .2f,
				originRotation.w + Random.Range (-shake_intensity,shake_intensity) * .2f);
			shake_intensity -= shake_decay;
		}
	}
}
