using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class DebugRigid : MonoBehaviour {

	public Vector2 velocity;

		private Rigidbody2D rg;
	// Use this for initialization
	void Start () {
		rg = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		velocity = rg.velocity;
	}
}
