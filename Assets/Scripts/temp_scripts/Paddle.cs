using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class Paddle : MonoBehaviour {

	public float accelerationForce, maxAcceleration; 

	Rigidbody2D rigid;

	// Use this for initialization
	void Start () {
		rigid = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKey (KeyCode.LeftArrow))
			rigid.AddForce (Vector3.left * accelerationForce, ForceMode2D.Force);

		if (Input.GetKey (KeyCode.RightArrow))
			rigid.AddForce (Vector3.right * accelerationForce, ForceMode2D.Force);
	}

}
