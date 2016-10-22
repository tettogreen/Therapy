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
			AnimatorStateInfo obj = animator.GetCurrentAnimatorStateInfo(0);
			animator.transform.SetParent (null);
		}
	}
}
