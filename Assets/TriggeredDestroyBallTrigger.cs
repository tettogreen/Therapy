using UnityEngine;
using System.Collections;

public class TriggeredDestroyBallTrigger : MonoBehaviour {
	public GameObject triggerObject;

	private bool isActivated = false;

	void OnTriggerStay2D (Collider2D collider)
	{
		if (isActivated && collider.CompareTag ("Ball")) {
			Debug.Log (Time.time + ": DestroyBallCollider is triggered");
			Ball ball = collider.GetComponent<Ball> ();
			if (ball)
				ball.Destroy ();
		}
	}

	void OnTriggerEnter2D (Collider2D collider)
	{
		if (collider.gameObject == triggerObject)
			isActivated = true;
	}

	void OnTriggerExit2D (Collider2D collider)
	{
		if (collider.gameObject == triggerObject)
			isActivated = false;
	}
}
