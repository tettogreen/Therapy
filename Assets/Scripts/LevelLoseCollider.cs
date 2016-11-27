using UnityEngine;
using System.Collections;

public class LevelLoseCollider : MonoBehaviour
{

	public Paddle paddle;

	void OnTriggerEnter2D (Collider2D collider)
	{
		if (collider.CompareTag ("Paddle")) {
			Debug.Log (Time.time + ": LoseCollider is triggered by Paddle");
			paddle.Destroy ();
		} else if (collider.CompareTag ("Ball")) {
			Debug.Log (Time.time + ": LoseCollider is triggered by Ball");
			paddle.Destroy ();
		}
	}

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}
