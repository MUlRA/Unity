using UnityEngine;
using System.Collections;

public class CrystalBehaviour: MonoBehaviour
{
	public float Health = 5.0f;
	public float damageAmount = 1.0f;
	public float amplitude = 1.0f;
	public float verticalspeed= 1.0f;
	public string [] Tags;
	public string collisionTag = "Player";
	public string BombTag = "Bomb";
	public GameObject[] spawnObject;
	public GameObject gib;
	private Vector3 TempPos;
	private Vector3 oscillating;
	// Update is called once per frame
	void Update () 
	{
		TempPos = transform.position;
		TempPos.y = oscillating.y;
		oscillating.y = Mathf.Sin (Time.realtimeSinceStartup * verticalspeed) * amplitude;
		transform.position = TempPos;
	
	}
	void OnTriggerEnter (Collider col)
	{
				foreach (string tag in Tags) {
						if (col.tag == "PlayerBullets") 
			{
				Health -= WeaponAndAccessories.Instance.Power;
								if (Health <= 0) {
										//Death animation
										Invoke ("Death", 0);
								}
						} 
						else if (col.tag == "Player") {
								HealthNBoosterManager.Instance.DamagedPlayer (damageAmount);
						}
						else if (col.tag == BombTag) {
								Invoke ("Death", 0);
						}
				}
		}
	void Death ()
	{

		int spawnObjectIndex = Random.Range (0, spawnObject.Length);
		Instantiate (spawnObject [spawnObjectIndex],transform.position, spawnObject [spawnObjectIndex].transform.rotation);
		Instantiate (gib,transform.position,Random.rotation); 
		Destroy (gameObject);
	}

}
