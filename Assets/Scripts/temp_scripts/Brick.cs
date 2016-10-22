using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(AnimationController))]
public class Brick : MonoBehaviour {

	public bool isUnbreakable;
	public Sprite[] sprites;
	public LevelManager levelManager;
	public static int bricksLeft = 0;		// Count of bricks left unbroken

	private SpriteRenderer spriteRenderer;
	private int timesHit;					//Counts times of hits by ball. Also it works as an index of the current sprite
	private float minPlaybackOffset	= 0.2f;
	//Offset between the starting time of the previous and the current audio playback.
	private static float previousAudioStartTime;	//The time the previous explosion sound started the playback 
	private AnimationController animationController;

	// Use this for initialization
	void Start ()
	{
		animationController = GetComponent<AnimationController>();

		timesHit = 0;
		previousAudioStartTime = 0;
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
				PlayExplosionSound();
				animationController.PlayAnimation("Explosion");
				DestroyBrick();
			} else {
				//change sprite to the sprite of the next "hit" state
				LoadNextSprite();
			}
		}
	}

	void DestroyBrick ()
	{
		bricksLeft--;
		levelManager.BrickDestroyed();
		Destroy (gameObject, 0.5f);
		GetComponent<BoxCollider2D>().enabled = false;	//disabling collider to avoid double-hit of a brick during animation
	}

	void LoadNextSprite ()
	{
		if (sprites [timesHit]) {
			spriteRenderer.sprite = sprites [timesHit];
		}
	}


	void PlayExplosionSound() {
		//Play explosion sound (if appropriate time offset passed since the start of the previous playback)
		if (Time.time - previousAudioStartTime >= minPlaybackOffset) 
		{
			previousAudioStartTime = Time.time;
			//AudioSource.PlayClipAtPoint (GetComponent<AudioSource> ().clip, transform.position, 0.5f);
			GetComponent<AudioSource> ().Play();
		}
	}
}
