using UnityEngine;
using System.Collections;

public class DestroyBallTrigger : MonoBehaviour
{

	void OnTriggerEnter2D (Collider2D collider)
	{
		if (collider.CompareTag ("Ball")) {
			Debug.Log (Time.time + ": DestroyBallCollider is triggered");
			Ball ball = collider.GetComponent<Ball> ();
			if (ball)
				ball.Destroy ();
		}
	}
}
