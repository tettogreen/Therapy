using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class Paddle : MonoBehaviour {

	public float accelerationForce, maxAcceleration; 
	public bool ControlledByAI;

	private Rigidbody2D rigid;
	private GameObject ball;
	private Animator animator;


	// Use this for initialization
	void Start ()
	{
		animator = GetComponent<Animator> ();
		rigid = GetComponent<Rigidbody2D> ();

		if (ControlledByAI) {
			ball = GameObject.FindWithTag ("Ball");
		}
	}


	// Update is called once per frame
	void FixedUpdate ()
	{
		if (ControlledByAI) {
			MoveWithAI();
		} else {
			MoveWithKeyboard ();
		}
	}


	public void Destroy ()
	{	
		animator.SetTrigger("Lose");
	}


	void MoveWithKeyboard () 
	{
			if (Input.GetKey (KeyCode.LeftArrow))
				rigid.AddForce (Vector3.left * accelerationForce, ForceMode2D.Impulse);

			if (Input.GetKey (KeyCode.RightArrow))
				rigid.AddForce (Vector3.right * accelerationForce, ForceMode2D.Impulse);
	}

	void MoveWithAI ()
	{
		// Find direction from paddle to ball
		Vector3 direction = ball.transform.position - transform.position;

		// Move paddle with force if ball is further than 0.5 metre
		if (Mathf.Abs(direction.x) > 0.5f) {
			direction = direction.x * Vector3.right;
			rigid.AddForce (direction * accelerationForce, ForceMode2D.Impulse);
		}
	}
}
