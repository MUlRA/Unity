﻿using UnityEngine;
using System.Collections;

public class HUD : MonoBehaviour 
{
	public int score = 0;
	public bool Shaking; 
	private float ShakeDecay;
	private float ShakeIntensity;    
	private Vector3 OriginalPos;
	private Quaternion OriginalRot;

	// Use this for initialization
	void Start ()
	{
		GUILayout.Label("Score:" + score);
		Shaking = false;   
	}
	
	
	// Update is called once per frame
	void Update () 
	{
		if(ShakeIntensity > 0)
		{
			transform.position = OriginalPos + Random.insideUnitSphere * ShakeIntensity;
			transform.rotation = new Quaternion(OriginalRot.x + Random.Range(-ShakeIntensity, ShakeIntensity)*.2f,
			                                                OriginalRot.y + Random.Range(-ShakeIntensity, ShakeIntensity)*.2f,
			                                                OriginalRot.z + Random.Range(-ShakeIntensity, ShakeIntensity)*.2f,
			                                                OriginalRot.w + Random.Range(-ShakeIntensity, ShakeIntensity)*.2f);
			
			ShakeIntensity -= ShakeDecay;
		}
		else if (Shaking)
		{
			Shaking = false;  
	  	}
	}
	
	
	void OnGUI()
	{

		if (Input.GetButtonDown("Jump"))
			DoShake();
		Debug.Log("Shake");
		
		 }        
	

		
		public void DoShake()
	{
		OriginalPos = transform.position;
		OriginalRot = transform.rotation;
		
		ShakeIntensity = 0.3f;
		ShakeDecay = 0.02f;
		Shaking = true;
	}    
	
	
}

