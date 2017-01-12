using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class Paddle : MonoBehaviour {


	public float AimingAreaOffset;	//Vertical offset between paddle and aiming area above which there will be no movement
	public float accelerationForce;
	public bool controlledByAI;
	public bool stickToPaddle;
	public bool aimedShootingMode;
	public Ball playerBall;


	Camera mainCamera;
	Rigidbody2D rigid;
	float aimingAreaLine; 		//height of the screen above which GetMouseButtonUp() will invoke aimed shooting

	private Animator animator;

//	private Vector3 ballDefaultPosition; //new
	private Vector3 lockedPosition; 
	private float InteractiveHeight = 0.02f;

	// Use this for initialization
	void Start ()
	{
		animator = GetComponent<Animator> ();
		rigid = GetComponent<Rigidbody2D>();
		mainCamera = Camera.main;


		//Calculating the line above which AimedLaunch() method will be invoked and below which MovePaddle will be invoked.
		Vector3 position = mainCamera.WorldToScreenPoint(transform.position);
		aimingAreaLine =  position.y + AimingAreaOffset;

		lockedPosition = CalculateRelativeDirection(playerBall.gameObject.transform.position);		//default position of the ball
	}

	void FixedUpdate ()
	{
		//Move paddle depending on who is controlling it (AI/Player)
		if (controlledByAI)
			MoveToThePoint (playerBall.transform.position, 0.5f);
		else
			MoveWithTouch ();

		// Lock the ball to the paddle
		if (playerBall.isLocked)
			playerBall.Follow (gameObject, lockedPosition);
		// Launch the ball
		if (playerBall.isLocked && Input.GetMouseButtonUp (0))
			Launch (playerBall);
	}

	void OnCollisionEnter2D (Collision2D collision)
	{
		if (collision.gameObject.tag == "Ball") {
			Ball collidedBall = collision.gameObject.GetComponent<Ball>();
			Vector2 direction = CalculateRelativeDirection(collision.gameObject.transform.position);
			if (stickToPaddle)
				Lock (collidedBall);
			else if (direction.y > InteractiveHeight) //Preventing the ball from stucking in boring loop
				collidedBall.ChangeDirection (direction);
		}
	}

	void MoveWithTouch ()
	{
		if (Input.GetMouseButton (0) && Input.mousePosition.y <= aimingAreaLine) 
			MoveToThePoint (mainCamera.ScreenToWorldPoint(Input.mousePosition), 0.1f);
	}


	void MoveToThePoint (Vector3 point, float sensitivity)
	{
		// Find direction from  to ball
		Vector3 direction = point - transform.position;

		// Move  with force if ball is further than 0.5 metre
		if (Mathf.Abs (direction.x) > sensitivity) {
			direction = direction.x * Vector3.right;
			rigid.AddForce (direction.normalized * accelerationForce, ForceMode2D.Impulse);
		} else {
			rigid.velocity = Vector2.zero;
		}
	}

	void Launch (Ball ball)
	{
		Vector2 direction;

		// Calculating bounce according to shooting mode
		if (aimedShootingMode && Input.mousePosition.y > aimingAreaLine) {
			Vector2 mousePosition = mainCamera.ScreenToWorldPoint (Input.mousePosition);
			direction = CalculateRelativeDirection (mousePosition);
		}
		else
			direction = CalculateRelativeDirection (ball.gameObject.transform.position);
		ball.Launch (direction);
	}

	void Lock (Ball ball)
	{
		Vector2 relativePosition = CalculateRelativeDirection (ball.gameObject.transform.position);
		if (relativePosition.y > InteractiveHeight) {
			lockedPosition = relativePosition;
			ball.Stop ();
		}
	}


	Vector2 CalculateRelativeDirection (Vector3 target) {
		return target - transform.position;
	}

	public void Destroy ()
	{	
		animator.SetTrigger("Lose");
	}
}
