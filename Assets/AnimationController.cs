using UnityEngine;
using System.Collections;

public class AnimationController : MonoBehaviour {

	private Animator animator;

	void Start ()
	{
		if (transform.childCount != 0) {
			animator = GetComponentInChildren<Animator> ();
		}
	}

	public void PlayAnimation (string animation)
	{
		//Play explosion animation
		if (animator) {
			animator.Play (animation);
			animator.transform.SetParent (null);
		}
	}
}
