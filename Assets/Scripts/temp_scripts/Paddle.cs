using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class Paddle : MonoBehaviour {

	public float accelerationForce, maxAcceleration; 

	ScreenBorders screenBorders;
	Rigidbody2D rigid;

	// Use this for initialization
	void Start () {
		screenBorders = new ScreenBorders();
		rigid = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKey (KeyCode.LeftArrow)) {
			
			rigid.AddForce (Vector3.left * accelerationForce, ForceMode2D.Force);
		}
		if (Input.GetKey (KeyCode.RightArrow)) {
			rigid.AddForce (Vector3.right * accelerationForce, ForceMode2D.Force);
		}
//		float mousePosInBlocks = Input.mousePosition.x / Screen.width * 16;
//		float newPositionX = Mathf.Clamp (mousePosInBlocks, 0.5f, 15.5f);
//		transform.position = new Vector3 (newPositionX, transform.position.y, 0f);
	}

	void CalculateScreenBorders ()
	{
		float veticalScreenHalfSize = Camera.main.orthographicSize;    
		float horizontalScreenHalfSize = veticalScreenHalfSize * Screen.width / Screen.height;
 
		// Calculations assume map is position at the origin
		Vector3 screenCenter = Camera.main.gameObject.transform.position;
		screenBorders.minX = screenCenter.x - veticalScreenHalfSize;
		screenBorders.maxX = screenCenter.x + veticalScreenHalfSize;
		screenBorders.minY = screenCenter.y - horizontalScreenHalfSize;
		screenBorders.maxY = screenCenter.y + horizontalScreenHalfSize;
	}

	private class ScreenBorders {
		public float minX, maxX, minY, maxY;
	}
}
