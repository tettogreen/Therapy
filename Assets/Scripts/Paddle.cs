using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
public class Paddle : MonoBehaviour {


	private Animator animator;


	// Use this for initialization
	void Start ()
	{
		animator = GetComponent<Animator> ();
	}



	public void Destroy ()
	{	
		animator.SetTrigger("Lose");
	}
}
