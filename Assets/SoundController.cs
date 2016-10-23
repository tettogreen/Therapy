using UnityEngine;
using System.Collections;

public class SoundController : MonoBehaviour {

	float minPlaybackOffset	= 0.2f;					//Offset between the starting time of the previous and the current audio playback.
	private static float previousAudioStartTime = 0f;	//The time the previous explosion sound started the playback 


	//Play AudioSource sound (if appropriate time offset passed since the start of the previous playback)
	public void PlaySound() {
		if (Time.time - previousAudioStartTime >= minPlaybackOffset) 
		{
			previousAudioStartTime = Time.time;
			GetComponentInChildren<AudioSource> ().Play();
		}
	}
}
