using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalDoors : MonoBehaviour {

	public GameObject otherDoors;
	public  ObjectPool balls;
	public MeshRenderer[] masks;

	private Collider2D otherCollider;

	// Use this for initialization
	void Start ()
	{
		otherCollider = otherDoors.GetComponent<Collider2D> ();
	}
	
	void OnTriggerEnter2D (Collider2D collider)
	{
		if (collider.tag == "Ball") {
			GameObject newBall = balls.PoolObject();
			otherCollider.enabled = false;
			InitializeBall(newBall, collider.gameObject);
		}
	}

	void OnTriggerExit2D (Collider2D collider)
	{
		if (collider.tag == "Ball")
			collider.gameObject.SetActive(false);
			otherCollider.enabled = true;
			TurnOffMasks();
	}

	void InitializeBall (GameObject teleportedBall, GameObject originalBall)
	{
		// Attach original ball to original doors local system. Get objects' RigidBodies.
		originalBall.transform.parent = gameObject.transform;
		Rigidbody2D teleportedRigid = teleportedBall.GetComponent<Rigidbody2D>();
		Rigidbody2D originalRigid = originalBall.GetComponent<Rigidbody2D>();


		if (teleportedBall) {
			teleportedBall.transform.parent = otherDoors.gameObject.transform;
			teleportedBall.transform.localPosition = originalBall.transform.localPosition;
			teleportedBall.transform.localRotation = originalBall.transform.localRotation;

			teleportedBall.SetActive(true);

			//Set teleported ball as launched
			Animator teleportedAnim = teleportedBall.GetComponent<Animator>();
			if (teleportedAnim)
				teleportedAnim.SetBool("isLaunched", true);

			//Set velocity: World > Local of original doors, Local  > World through retranslation of velocity direction
			// relatively to other doors
			Vector3 originalVelocity = transform.InverseTransformDirection(originalRigid.velocity);
			teleportedRigid.velocity = otherDoors.transform.TransformVector(originalVelocity);
		}
	}

	void TurnOffMasks ()
	{
		foreach (var mask in masks) {
			mask.enabled = false;
		}
	}
}
