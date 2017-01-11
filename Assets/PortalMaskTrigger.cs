using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(MeshRenderer))]
public class PortalMaskTrigger : MonoBehaviour
{

	public GameObject otherMask;

	MeshRenderer mesh;


	void Start ()
	{
		mesh = GetComponent<MeshRenderer> ();
	}

	void OnTriggerEnter2D (Collider2D collider)
	{
		if (collider.tag == "Ball") {
			otherMask.SetActive (false);
			mesh.enabled = true;
		}
	}

	void OnTriggerExit2D (Collider2D collider)
	{
		if (collider.tag == "Ball") {
//			collider.gameObject.SetActive(false);
			otherMask.SetActive (true);
			mesh.enabled = false;
		}
	}
}
