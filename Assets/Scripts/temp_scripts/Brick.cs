using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(AnimationController))]
[RequireComponent(typeof(SoundController))]
public class Brick : MonoBehaviour {

	public bool isUnbreakable;
	public Sprite[] sprites;
	public GameConroller levelManager;
	public static int bricksLeft = 0;		// Count of bricks left unbroken

	private SpriteRenderer spriteRenderer;
	private int timesHit;					//Counts times of hits by ball. Also it works as an index of the current sprite


	private AnimationController animationController;
	private SoundController soundController;

	// Use this for initialization
	void Start ()
	{
		
		animationController = GetComponent<AnimationController>();
		soundController = GetComponent<SoundController>();
		timesHit = 0;
		spriteRenderer = gameObject.GetComponent<SpriteRenderer> ();
		spriteRenderer.sprite = sprites [0];
		if (!isUnbreakable) {
			bricksLeft++;
		}
	}


	void OnCollisionEnter2D (Collision2D collision)
	{
			
		Debug.Log (Time.time + "BRICK HIT!");

		//handling hits if the block is breakable, otherwise ignore it
		if (!isUnbreakable) {
			timesHit++;
			int maxHits = sprites.Length;
			if (timesHit >= maxHits) {
				soundController.PlaySound();
				animationController.PlayAnimation("Explosion");
				GetComponent<BoxCollider2D>().enabled = false;	//disabling collider to avoid double-hit of a brick during animation
				Destroy(gameObject, 0.5f);
			} else {
				//change sprite to the sprite of the next "hit" state
				LoadNextSprite();
			}
		}
	}


	void LoadNextSprite ()
	{
		if (sprites [timesHit]) {
			spriteRenderer.sprite = sprites [timesHit];
		}
	}

	void OnDestroy () {
	bricksLeft--;
	levelManager.BrickDestroyed();
	}
}
