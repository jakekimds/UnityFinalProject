using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GController : MonoBehaviour {

	public float speed = 10;
	public float max = 10;
	private Vector3 initialPos;
	public AudioSource swoosh;

	// Use this for initialization
	void Start() {
		initialPos = transform.localPosition;
	}

	// Update is called once per frame
	void Update() {
		Vector3 pos = transform.localPosition;
		pos.y += speed * Time.deltaTime;
		if (Mathf.Abs(initialPos.y - pos.y) >= max) {
			speed *= -1;
			swoosh.Play ();
		} else {
			transform.localPosition = pos;
		}
	}
}
