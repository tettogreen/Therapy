using UnityEngine;
using System.Collections;

public class Paddle : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		float mousePosInBlocks = Input.mousePosition.x / Screen.width * 16;
		float newPositionX = Mathf.Clamp (mousePosInBlocks, 0.5f, 15.5f);
		transform.position = new Vector3 (newPositionX, transform.position.y, 0f);
	}
}
