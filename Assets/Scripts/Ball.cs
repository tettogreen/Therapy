using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Animator))]
[RequireComponent (typeof(SoundController))]
[RequireComponent (typeof(Collider2D))]
[RequireComponent (typeof(Rigidbody2D))]
public class Ball : MonoBehaviour
{

	public float velocity;
	public bool stickToPaddle;

	private Paddle paddle;
	private Vector3 ballDefaultPosition;

	private Rigidbody2D rg;
	private Collider2D collider2d;

	private Animator animator;
	private SoundController soundController;

	private float minPlaybackOffset	= 0.1f;
	private Vector3 stuckPosition;
	private bool isStuckToPaddle = false;
//	private float paddleInteractiveHeight = 0.02f;   TODO Delete after refactoring
	//Offset between the starting time of the previous and the current audio playback.
	private static float previousAudioStartTime;
	//The time the previous explosion sound started the playback


	void Awake ()
	{
		animator = GetComponent<Animator> ();
		soundController = GetComponent<SoundController> ();
		rg = GetComponent<Rigidbody2D> ();
		collider2d = GetComponent<Collider2D> ();
	}

	// Use this for initialization
	void Start ()
	{

		paddle = GameObject.FindObjectOfType<Paddle> ();
		if (paddle)
			ballDefaultPosition = transform.position - paddle.transform.position;
		else
			Debug.LogWarning("Ball init failed: No paddle was found");
	}

	// Update is called once per frame
	void FixedUpdate ()
	{
		
		//#TODO Take everything except of ChangeVelocity into Paddle
		if (!animator.GetBool ("isLaunched")) {
			LockToPaddle();
			if (Input.GetMouseButtonDown (0)) {
				Launch ();
			}
		} else {
			ChangeVelocity (velocity);
//			RandomTweak();
		}


	}

	void OnCollisionEnter2D (Collision2D collision)
	{
		PlayHitSound ();

		if (collision.gameObject.tag == "Paddle") {
		// #TODO take this module into paddle
			if (stickToPaddle) {
				Stick ();
			} else {
				ChangeDirectionRelativelyTo (collision.gameObject, 0.02f);
			}
		}
	}



	public void Destroy ()
	{
		Stop ();
		animator.SetTrigger ("Explosion");
	}

	//Changes direction of the ball when it hits the paddle depending on the point of hit of the paddle
	public void ChangeDirectionRelativelyTo (GameObject targetPaddle, float paddleInteractiveHeight)
	{
	// #TODO take except of the IF body into the paddle
		Vector2 direction = transform.position - targetPaddle.transform.position;
		if (direction.y > paddleInteractiveHeight)	   //Preventing the ball from stucking in boring loop
			ChangeDirection (direction);
	}


	//Play hit sound (if appropriate time offset passed since the start of the previous playback)
	void PlayHitSound ()
	{
		if (Time.time - previousAudioStartTime >= minPlaybackOffset) {
			previousAudioStartTime = Time.time;
			soundController.PlaySound ();
		}
	}
	
	//#Call Launch from Paddle Launch.
	//#TODO This method should be called only after ChangeDirectionRelativelyTo in Paddle class. 
	public void Launch ()
	{
		collider2d.enabled = true;
		isStuckToPaddle = false;
		rg.velocity = new Vector2 (0f, velocity);
		//#TODO Call ChangeDirecionRelativelyTo() from Paddle.
		ChangeDirectionRelativelyTo (paddle.gameObject, 0.02f);
		animator.SetBool ("isLaunched", true);
	}

	void Stop ()
	{
		animator.SetBool ("isLaunched", false);		
		collider2d.enabled = false;
		ChangeVelocity (0f);
	}
	//#TODO Take this method into Ball
	void Stick ()
	{
		stuckPosition = transform.position - paddle.transform.position;
		if (stuckPosition.y > 0.02f) {							//#TODO change 0.02f to PaddleInteractiveHeight after refactoring
			isStuckToPaddle = true;
			Stop ();
		}
	}

	void LockToPaddle ()
	{
		if (isStuckToPaddle) {
			transform.position = paddle.transform.position + stuckPosition;
		} else {
			transform.position = paddle.transform.position + ballDefaultPosition;
		}
	}

	void ChangeVelocity (float targetVelocity)
	{
		if (rg.velocity.magnitude != targetVelocity) {
			rg.velocity = rg.velocity.normalized * targetVelocity;
		}
	}

	void ChangeDirection (Vector2 direction)
	{
		rg.velocity = direction.normalized * rg.velocity.magnitude;
	}

	//randomly changes velocity of the ball a bit to prevent stucking / inifinite loop
	void RandomTweak ()
	{
		Vector2 tweakVelocity = new Vector2 (Random.Range (0, 0.1f), Random.Range (0, 0.1f));
		rg.velocity += tweakVelocity;
	}


}
