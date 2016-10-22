using UnityEngine;
using System.Collections;

public class LoseCollider : MonoBehaviour {

	public LevelManager levelManager;
	public Paddle paddle;

	void OnTriggerEnter2D (Collider2D collider)
	{
		Debug.Log(Time.time + ": LoseCollider is triggered");
//		levelManager.LoadLevel("Lose");
//		Destroy(paddle.gameObject);
		paddle.Destroy();
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
