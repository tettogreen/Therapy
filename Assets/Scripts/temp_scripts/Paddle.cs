using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(AnimationController))]
public class Paddle : MonoBehaviour {

	public float accelerationForce, maxAcceleration; 

	private AnimationController animationController;
	Rigidbody2D rigid;

	// Use this for initialization
	void Start () {
		animationController = GetComponent<AnimationController>();
		rigid = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		if (Input.GetKey (KeyCode.LeftArrow))
			rigid.AddForce (Vector3.left * accelerationForce, ForceMode2D.Impulse);

		if (Input.GetKey (KeyCode.RightArrow))
			rigid.AddForce (Vector3.right * accelerationForce, ForceMode2D.Impulse);
	}

	public void Destroy ()
	{	
		animationController.PlayAnimation("PaddleDestroy");
	}


}
