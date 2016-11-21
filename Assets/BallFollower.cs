using UnityEngine;
using System.Collections;

public class BallFollower : MonoBehaviour {

	public GameObject ball;
	public float lerpFactor;	//Lerp factor of position translation

	public bool isFollowing;
	public bool IsFollowing { get; set; }
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		if (isFollowing) {
			Follow();
		}	
	}

	void Follow () {
		Vector2 offsetVector = new Vector2(transform.position.x, ball.transform.position.y);
		transform.position = Vector2.Lerp(transform.position, offsetVector, lerpFactor);
	}
}
