using UnityEngine;
using System.Collections;

public class AddToScoreOnTriggerEnter : MonoBehaviour
{
	public int pointValue = 100;
	
	void OnTriggerEnter(Collider col)
	{
		Debug.Log (col,col);
		ScoreManager.Instance.score += pointValue;
	}
}
