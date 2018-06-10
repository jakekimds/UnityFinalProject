using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSpawnerCallback : Callback {

	public AudioClip clip;
	[Range(0,1)]
	public float Volume = 1f;
	public bool Loop = false;
	public Transform position;
	[HideInInspector] public GameObject audioSource;

	public override void OnCall() {
		if (position == null) {
			position = transform;
		}
		GameObject obj = Instantiate(audioSource, position.position, position.rotation);
		AudioSource audS = obj.GetComponent<AudioSource>();
		audS.clip = clip;
		audS.volume = Volume;
		audS.loop = Loop;
		audS.Play();
	}
}
