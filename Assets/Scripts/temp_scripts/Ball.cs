using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {

	public float velocity;

	private Paddle paddle; 
	private Vector3 paddleToBallVector;

	private Rigidbody2D rg;
	private bool isLaunched = false;		
	private float minPlaybackOffset	= 0.1f;	//Offset between the starting time of the previous and the current audio playback.

	private static float previousAudioStartTime;	//The time the previous explosion sound started the playback 


	// Use this for initialization
	void Start () {
		paddle = GameObject.FindObjectOfType<Paddle>();
		paddleToBallVector = transform.position - paddle.transform.position;
		rg = gameObject.GetComponent<Rigidbody2D>();
	}


	// Update is called once per frame
	void Update ()
	{
		if (!isLaunched) {

			//lock the ball to the paddle
			transform.position = paddle.transform.position + paddleToBallVector;
			if (Input.GetMouseButtonDown (0)) {
				Launch();
			}

		} else {
			ChangeVelocity(velocity);
			RandomTweak();
		}


	}


	void OnCollisionEnter2D (Collision2D collision)
	{
		//Play hit sound (if appropriate time offset passed since the start of the previous playback)
		if (Time.time - previousAudioStartTime >= minPlaybackOffset) {
			previousAudioStartTime = Time.time;
			GetComponent<AudioSource> ().Play ();
		}
	}


	public void DestroyBall ()
	{
		//Play explosion animation
		Animator animator = GetComponentInChildren<Animator> ();
		if (animator) {
			animator.Play ("Explosion");
			animator.transform.SetParent (null);
		}

		Destroy(gameObject);
	}


	void Launch ()
	{
		rg.velocity = new Vector2 (0f, velocity);
		isLaunched = true;
	}

	void ChangeVelocity (float targetVelocity)
	{
		if (rg.velocity.magnitude != targetVelocity) {
			rg.velocity = rg.velocity.normalized * targetVelocity;
		}
	}

	//randomly changes velocity of the ball a bit to prevent stucking / inifinite loop
	void RandomTweak ()
	{
		Vector2 tweakVelocity = new Vector2 (Random.Range(0, 0.1f), Random.Range(0, 0.1f));
		rg.velocity += tweakVelocity;
	}

}
