using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeAudioClipCallback : Callback {

	public AudioSource audioSource;
	public AudioClip newClip;
	public bool play;
	public bool changeVolume;
	[Range(0, 1)] public float newVolume;

	public override void OnCall() {
		if (newClip != null) {
			audioSource.clip = newClip;
		}
		if (changeVolume) {
			audioSource.volume = newVolume;
		}
		if (play) {
			audioSource.time = 0;
			audioSource.Play();
		}
	}
}
