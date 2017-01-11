using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PaddleMover : MonoBehaviour {

	public float VerticalOffset;	//Vertical offset above which there will be no  movement in pixels
	public float accelerationForce;
	public bool controlledByAI;
	public GameObject ball;
	Camera mainCamera;

	Rigidbody2D rigid;
	float maximumMouseHeight;

	void Start ()
	{
		rigid = GetComponent<Rigidbody2D>();
		mainCamera = Camera.main;
		//Calculating the line above which LaunchBall method will be invoked and below which MovePaddle will be invoked.
		Vector3 position = mainCamera.WorldToScreenPoint(transform.position);
		maximumMouseHeight =  position.y + VerticalOffset;
	}

	void Update ()
	{
		if (controlledByAI) {
			MoveToThePoint (ball.transform.position);
		} else if (Input.GetMouseButton (0)) {
			MovePaddle();
		}
	}

	void MovePaddle ()
	{
		if (Input.mousePosition.y <= maximumMouseHeight) 
			MoveToThePoint (mainCamera.ScreenToWorldPoint(Input.mousePosition));
	}


	void MoveToThePoint (Vector3 point)
	{
		// Find direction from  to ball
		Vector3 direction = point - transform.position;

		// Move  with force if ball is further than 0.5 metre
		if (Mathf.Abs(direction.x) > 0.5f) {
			direction = direction.x * Vector3.right;
			rigid.AddForce (direction * accelerationForce, ForceMode2D.Impulse);
		}
	}

	void LaunchBall ()
	{
//		if (Input.mousePosition.y > maximumMouseHeight && Input.GetMouseButtonUp(0)) 
			
	}
}

