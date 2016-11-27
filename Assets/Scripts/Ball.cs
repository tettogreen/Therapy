using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AnimationController))]
[RequireComponent(typeof(SoundController))]
[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Ball : MonoBehaviour {

	public float velocity;

	private Paddle paddle; 
	private Vector3 paddleToBallVector;

	private Rigidbody2D rg;
	private Collider2D collider2d;

	private Animator animator;
	private SoundController soundController;

	private float minPlaybackOffset	= 0.1f;	//Offset between the starting time of the previous and the current audio playback.
	private static float previousAudioStartTime;	//The time the previous explosion sound started the playback 


	void Awake() {
		animator = GetComponent<Animator>();
		soundController = GetComponent<SoundController>();
		rg = GetComponent<Rigidbody2D>();
		collider2d = GetComponent<Collider2D>();
	}

	// Use this for initialization
	void Start () {

		paddle = GameObject.FindObjectOfType<Paddle>();
		paddleToBallVector = transform.position - paddle.transform.position;
	}

	// Update is called once per frame
	void Update ()
	{
		
		if (!animator.GetBool("isLaunched")) {

			//lock the ball to the paddle
			transform.position = paddle.transform.position + paddleToBallVector;
			if (Input.GetMouseButtonDown (0)) {
				Launch();
			}

		} else {
			ChangeVelocity(velocity);
//			RandomTweak();
		}


	}

	void OnCollisionEnter2D (Collision2D collision)
	{
		//Play hit sound (if appropriate time offset passed since the start of the previous playback)
		if (Time.time - previousAudioStartTime >= minPlaybackOffset) {
			previousAudioStartTime = Time.time;
			soundController.PlaySound();
		}
	}




	public void Destroy ()
	{
		collider2d.enabled = false;
		ChangeVelocity(0f);
		animator.SetTrigger("Explosion");
	}



	void Launch ()
	{
		collider2d.enabled = true;
		rg.velocity = new Vector2 (0f, velocity);
		animator.SetBool("isLaunched", true);
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
