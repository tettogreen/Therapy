using UnityEngine;
using System.Collections;

public class WayPointMovement : MonoBehaviour {

	public Transform target;
    public float smoothTime = 0.3F;

    private Vector3 velocity = Vector3.zero;		//lerp factor of moving to position

	// Use this for initialization
	void Start () {
		//target.transform.parent = null;
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		if (transform.position != target.position) {
			MoveToTarget();
		}
	}

	void MoveToTarget ()
	{
		transform.position = Vector3.SmoothDamp(transform.position, target.position, ref velocity, smoothTime);
	}
}

