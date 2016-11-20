using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(AnimationController))]
[RequireComponent(typeof(SoundController))]
[RequireComponent(typeof(BoxCollider2D))]
public class Brick : MonoBehaviour {

	public bool isUnbreakable;
	public Sprite[] sprites;
	public static int bricksLeft = 0;		// Count of bricks left unbroken

	private SpriteRenderer spriteRenderer;
	private int timesHit;					//Counts times of hits by ball. Also it works as an index of the current sprite


	private Animator animator;
	private SoundController soundController;
	private GameConroller gameController;
	private BoxCollider2D brickCollider;

	// Use this for initialization
	void Awake ()
	{
		animator = GetComponent<Animator>();
		spriteRenderer = gameObject.GetComponent<SpriteRenderer> ();
		brickCollider = GetComponent<BoxCollider2D>();
		soundController = GetComponent<SoundController>();
		gameController = FindObjectOfType<GameConroller>();
		timesHit = 0;
		spriteRenderer.sprite = sprites [0];
	}

	void Start ()
	{
		if (CompareTag("DestructibleBrick")) {
			bricksLeft++;
		}
	}


	void OnCollisionEnter2D (Collision2D collision)
	{
			
		Debug.Log (Time.time + "BRICK HIT!");

		//handling hits if the block is breakable, otherwise ignore it
		if (!CompareTag("UnbreakableBrick")) {
			timesHit++;
			int maxHits = sprites.Length;
			if (timesHit >= maxHits) {
				soundController.PlaySound();
				brickCollider.enabled = false;	//disabling collider to avoid double-hit of a brick during animation
				this.Destroy();
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

	void Destroy ()
	{
		if (CompareTag ("DestructibleBrick")) {
			bricksLeft--;
			gameController.BrickDestroyed ();
		}
		animator.SetTrigger("Explosion");
	}

	void OnEnable ()
	{
		brickCollider.enabled = true;
	}
}
