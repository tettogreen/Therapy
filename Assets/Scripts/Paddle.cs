using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class Paddle : MonoBehaviour {


	public float VerticalOffset;	//Vertical offset above which there will be no movement in pixels
	public float accelerationForce;
	public bool controlledByAI;
	public bool stickToPaddle;
	public Ball playerBall;


	Camera mainCamera;
	Rigidbody2D rigid;
	float maximumMouseHeight;

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


		//Calculating the line above which LaunchBall method will be invoked and below which MovePaddle will be invoked.
		Vector3 position = mainCamera.WorldToScreenPoint(transform.position);
		maximumMouseHeight =  position.y + VerticalOffset;

		lockedPosition = CalculateRelativeDirection(playerBall.gameObject);		//default position of the ball
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

		if (playerBall.isLocked && Input.GetMouseButtonUp (0))
			Launch (playerBall);
	}

	void OnCollisionEnter2D (Collision2D collision)
	{
		if (collision.gameObject.tag == "Ball") {
			Ball collidedBall = collision.gameObject.GetComponent<Ball>();
			Vector2 direction = CalculateRelativeDirection(collision.gameObject);
			if (stickToPaddle)
				Lock (collidedBall);
			else if (direction.y > InteractiveHeight) //Preventing the ball from stucking in boring loop
				collidedBall.ChangeDirection (direction);
		}
	}

	void MoveWithTouch ()
	{
		if (Input.GetMouseButton (0) && Input.mousePosition.y <= maximumMouseHeight) 
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
//		if (Input.mousePosition.y > maximumMouseHeight && Input.GetMouseButtonUp(0)) 
			// Calculating bounce 
			Vector2 direction = CalculateRelativeDirection(ball.gameObject);
			ball.Launch (direction);
	}

	void Lock (Ball ball)
	{
		Vector2 relativePosition = CalculateRelativeDirection (ball.gameObject);
		if (relativePosition.y > InteractiveHeight)	
			lockedPosition = relativePosition;
			ball.Stop ();
	}


	Vector2 CalculateRelativeDirection (GameObject target) {
		return target.transform.position - transform.position;
	}

	public void Destroy ()
	{	
		animator.SetTrigger("Lose");
	}
}
