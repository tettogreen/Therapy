using UnityEngine;
using System.Collections;

public class PositionLocker : MonoBehaviour {

	public bool xIsLocked;
	public bool yIsLocked;

	private Vector3 initialPosition;

	// Use this for initialization
	void Start () {
		initialPosition = transform.position;		
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		if (xIsLocked && transform.position.x != initialPosition.x) {
			transform.position = new Vector2 (initialPosition.x, transform.position.y);
		}
		if (yIsLocked && transform.position.y != initialPosition.y) {
			transform.position = new Vector2 (transform.position.x, initialPosition.y);
		}
	}
}
