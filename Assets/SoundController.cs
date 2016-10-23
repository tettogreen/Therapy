using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundController : MonoBehaviour {

	float minPlaybackOffset	= 0.2f;					//Offset between the starting time of the previous and the current audio playback.
//	private static float previousAudioStartTime = 0f;
//	private static AudioSource previousPlayedSound;	//The time the previous explosion sound started the playback 
	private static Dictionary<string, float> previousAudioStartTimeOf = new Dictionary<string, float>();


	//Play AudioSource sound (if appropriate time offset passed since the start of the previous playback)
	public void PlaySound ()
	{
		AudioClip currentSound = GetComponentInChildren<AudioSource> ().clip;
		float clipPreviousStartTime = GetClipsPreviousStartTime (currentSound);
		if (TimeOffsetPassedSince(clipPreviousStartTime)) 
		{
			previousAudioStartTimeOf[currentSound.name] = Time.time;
			AudioSource.PlayClipAtPoint(currentSound, transform.position);
		}


//
//		if (currentSound != previousPlayedSound || TimeSinceLastPLaybackPassed()) 
//		{
//			previousAudioStartTime = Time.time;
//			currentSound.Play();
//			previousPlayedSound = currentSound;
//		}
	}



	bool TimeOffsetPassedSince (float lastTrackStartTime)
	{
		return Time.time - lastTrackStartTime >= minPlaybackOffset;
	}

	float GetClipsPreviousStartTime(AudioClip clip) {
		if(!previousAudioStartTimeOf.ContainsKey(clip.name))
			previousAudioStartTimeOf.Add(clip.name, 0f);
		return previousAudioStartTimeOf [clip.name];
	}
}
