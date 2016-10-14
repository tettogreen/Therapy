using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {

	public float velocity;

	private Paddle paddle; 
	private Vector3 paddleToBallVector;

	private float previousBallX = 0;
	private float previousBallY = 0;
	private float lastStuckTimeX = 0;
	private float lastStuckTimeY = 0;

	private Rigidbody2D rg;
	private bool isLaunched = false;
	private bool wasStuckX = false;			//Was ball stuck during the previous stuck check?
	private bool wasStuckY = false;			
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

			//Wait for launch
			if (Input.GetMouseButtonDown (0)) {
				rg.velocity = new Vector2 (0f, velocity);
				isLaunched = true;
			}
		} else {
			CheckStuck (transform.position.x, ref previousBallX, ref wasStuckX, ref lastStuckTimeX);
			CheckStuck (transform.position.y, ref previousBallY, ref wasStuckY, ref lastStuckTimeY);
		}


	}

	void OnCollisionEnter2D (Collision2D collision)
	{
		//Play hit sound (if appropriate time offset passed since the start of the previous playback)
		if (Time.time - previousAudioStartTime >= minPlaybackOffset) {
			previousAudioStartTime = Time.time;
			GetComponent<AudioSource> ().Play ();
		}
		//Reset stuck values if ball collides Paddle
		if (collision.gameObject.GetComponent<Paddle>() != null) {
			wasStuckX = false;
			wasStuckY = false;
		}	
	}

	//Releases the ball if it has been stuck in Y coordinates for more than 5 seconds
	void CheckStuck (float currentCoord, ref float previousBallCoord, ref bool wasStuck, ref float lastStuckTime)
	{
		//Checks if ball is stuck in Y coordinates.
		bool isStuck = (Mathf.Abs (previousBallCoord - currentCoord) < 0.0005f);
		if (!isStuck) {
			wasStuck = false;			//is also set to 'false' in OnCollision2D()
		} else if (!wasStuck) {
			lastStuckTime = Time.time;
			wasStuck = true;
		} else if (Time.time - lastStuckTime > 5f) {
			rg.velocity = new Vector2 (0f, 0f);
			transform.position = paddle.transform.position + paddleToBallVector;
			wasStuck = false;
			isLaunched = false;
		}
		previousBallCoord = currentCoord;
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
}
