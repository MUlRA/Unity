﻿using UnityEngine;
using System.Collections;

public class Shipmovement : MonoBehaviour 
{
	private static Shipmovement instance = null;
	public static Shipmovement Instance
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
	public float movementSpeed = 1.0f;
	public string [] tags;
	public string collisionTag = "Ground";
	public int invert= -1; //Neg for invert, pos for not
	public float BankLag = 1.0f;
	public GameObject SmokeTrail;
	public int LoadoutModifier;


	// Update is called once per frame
	void Update()
	{
		if (HealthNBoosterManager.Instance.isCutscene == false)
		{
			float horizontal = Input.GetAxis("Horizontal");
			float vertical = Input.GetAxis("Vertical");
			float bankAxis = Input.GetAxis("Horizontal");



			Vector3 direction = new Vector3(horizontal, invert * vertical, 0);
			Vector3 finalDirection = new Vector3(horizontal, invert * vertical, 10.0f);
			transform.position += direction * movementSpeed * Time.deltaTime;
			transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(finalDirection), Mathf.Deg2Rad * 100.0f);

			//bank
			Vector3 newRotationEuler = transform.localRotation.eulerAngles;
			newRotationEuler.z = -20 * bankAxis * BankLag;
			Quaternion newQuat = Quaternion.identity;
			newQuat.eulerAngles = newRotationEuler;
			transform.rotation = newQuat;

			//boundary
			Vector3 boundaryVector = transform.position;
			boundaryVector.x = Mathf.Clamp(boundaryVector.x, -1200f, 1200f);
			boundaryVector.y = Mathf.Clamp(boundaryVector.y, -600f, 500f);
			transform.position = boundaryVector;





			if (Input.GetButtonDown("Fire1"))
			{
                Weapons.Instance.Fire(LoadoutModifier);
			}

			if (Input.GetButtonDown("Fire2") && (HealthNBoosterManager.Instance.currentBomb > 0))
			{
				Weapons.Instance.FireBomb();

			}
			if (Input.GetButtonDown("Jump") && HealthNBoosterManager.Instance.isTired == false)
			{
				movementSpeed = 300.0f;
			}
			if (Input.GetButtonUp("Jump"))
			{
				movementSpeed = 750.0f;
			}
		}
	}
	public void Knockback()
	{

	}
}
