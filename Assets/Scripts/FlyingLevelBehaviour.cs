using UnityEngine;
using System.Collections;

public class FlyingLevelBehaviour : MonoBehaviour 
{
	public float Timer = 3.0f;
	public float movementTime = 1.0f;
	public GameObject[] Bosses;
	public GameObject spawn;
	public WARNING warning;
	public MoveForward moveforward;
	private Vector3 goalpos;
	private Vector3 currentPosition;
	// Use this for initialization
	void Start () 
	{
		GetComponents<WARNING>();
		GetComponents<MoveForward>();
		Invoke ("KILLSPAWNER", Timer);

	}
	int enemyIndex;

	void KILLSPAWNER()
	{
		Destroy (spawn);
		Invoke ("WARNING", 20.0f);
	}
	void WARNING()
	{
		warning.StartCoroutine ("MessageStart");
		Invoke ("SpawnBoss", 7.0f);
		Invoke ("MoveBoss",7.0f);
			
	}
	void SpawnBoss()
	{
		enemyIndex = Random.Range(0, Bosses.Length);
		Instantiate(Bosses[enemyIndex], transform.position, transform.rotation); 
		Invoke ("StopMovement", 7.0f);
	}
	void StopMovement()
	{
		moveforward.enabled = false;
		}
	void MoveBoss()
	{
		Vector3 movementVector = new Vector3();
		movementVector.x = transform.localPosition.x;
		movementVector.y = transform.localPosition.y;
		movementVector.z = 2500.0f;
		goalpos = currentPosition + movementVector;
		currentPosition = transform.localPosition;
		StartCoroutine ("MovingIn");
	}
	IEnumerator MovingIn()
	{
		float t = 0.0f;
		while(t < movementTime)
		{
			t += Time.deltaTime;
			transform.localPosition = Vector3.Lerp(transform.localPosition,goalpos, t/movementTime);
			yield return null;
		}
	}
}

