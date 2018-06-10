using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(AudioSource))]
public class SwingWithSound : MonoBehaviour {

	public float targetAngle = 90f;

	private Rigidbody rb;
	private float startTime;
	private AudioSource audioSource;
	private float period = 5f;
	private float lastAngle;
	private float lastSpeed;

	// Use this for initialization
	void Start() {
		rb = GetComponent<Rigidbody>();
		audioSource = GetComponent<AudioSource>();
		startTime = Time.time;
		period = audioSource.clip.length;
	}

	// Update is called once per frame
	void Update() {
		float angle = getAngle();
		float speed = angle - lastAngle;

		if (Mathf.Sign(speed) != Mathf.Sign(lastSpeed)) {
			audioSource.Play();
		}

		lastSpeed = speed;
		lastAngle = angle;
		rb.rotation = Quaternion.AngleAxis(angle, Vector3.right);
	}

	float getAngle() {
		float time = Time.time - startTime;
		return Mathf.Sin(time * 2 * Mathf.PI / period) * targetAngle;
	}
}
