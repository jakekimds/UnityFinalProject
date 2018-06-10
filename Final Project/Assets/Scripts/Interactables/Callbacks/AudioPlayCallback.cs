using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayCallback : Callback {

	public AudioSource audioSource;

	public override void OnCall() {
		audioSource.Play();
	}
}
