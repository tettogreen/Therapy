using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Animator))]
[RequireComponent (typeof(SoundController))]
[RequireComponent (typeof(Collider2D))]
[RequireComponent (typeof(Rigidbody2D))]
public class Ball : MonoBehaviour
{

	public float velocity;
	public bool isLocked;		//Shows if ball is currently locked to the paddle
//	public bool stickToPaddle;

//	private Paddle paddle;
//	private Vector3 ballDefaultPosition;

	private Rigidbody2D rg;
	private Collider2D collider2d;

	private Animator animator;
	private SoundController soundController;

	private float minPlaybackOffset	= 0.1f;
	//Offset between the starting time of the previous and the current audio playback.
	private static float previousAudioStartTime;


	void Awake ()
	{
		animator = GetComponent<Animator> ();
		soundController = GetComponent<SoundController> ();
		rg = GetComponent<Rigidbody2D> ();
		collider2d = GetComponent<Collider2D> ();
	}

	// Update is called once per frame
	void FixedUpdate ()
	{
		if (animator.GetBool ("isLaunched")) {
			ChangeVelocity (velocity);
		} 
	}


	void OnCollisionEnter2D (Collision2D collision)
	{
		PlayHitSound ();
	}



	//#Call Launch from Paddle Launch.
	public void Launch (Vector2 direction)
	{
		collider2d.enabled = true;
		rg.velocity = new Vector2 (0f, velocity);
		ChangeDirection (direction);
		animator.SetBool ("isLaunched", true);
		isLocked = false;
	}


	public void Stop ()
	{
		animator.SetBool ("isLaunched", false);		
		collider2d.enabled = false;
		ChangeVelocity (0f);
		isLocked = true;
	}


	public void Follow (GameObject target, Vector3 relativePosition) {
		transform.position = target.transform.position + relativePosition;
	}


	public void ChangeDirection (Vector2 direction)
	{
		rg.velocity = direction.normalized * rg.velocity.magnitude;
	}



	void ChangeVelocity (float targetVelocity)
	{
		if (rg.velocity.magnitude != targetVelocity) {
			rg.velocity = rg.velocity.normalized * targetVelocity;
		}
	}


	//Play hit sound (if appropriate time offset passed since the start of the previous playback)
	void PlayHitSound ()
	{
		if (Time.time - previousAudioStartTime >= minPlaybackOffset) {
			previousAudioStartTime = Time.time;
			soundController.PlaySound ();
		}
	}

	//randomly changes velocity of the ball a bit to prevent stucking / inifinite loop
//	void RandomTweak ()
//	{
//		Vector2 tweakVelocity = new Vector2 (Random.Range (0, 0.1f), Random.Range (0, 0.1f));
//		rg.velocity += tweakVelocity;
//	}

	public void Destroy ()
	{
		Stop ();
		animator.SetTrigger ("Explosion");
	}

}
