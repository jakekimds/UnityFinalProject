﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class ConditionalCollision : MonoBehaviour {
	[TextArea(3, 10)]
	public string text;
	public float fadeTime = 0;
	public Transform telePoint;
	public bool copyRotation;

    public Callback PassCallback;
    public Callback FailCallback;

	void Start() {
	}

	void OnCollisionEnter(Collision collision) {
		GameObject other = collision.gameObject;

		if (other.CompareTag("Player")) {
			if (isReadyToPass(other)) {
				gameObject.SetActive(false);
                Callback.Call(PassCallback);
			} else {
                Callback.Call(FailCallback);
				GUIManager.instance.directions(text, fadeTime);
				if (telePoint != null) {
					other.transform.position = telePoint.position;
					if (copyRotation) {
						other.transform.rotation = telePoint.rotation;
					}
				}
			}
		}
	}

	public abstract bool isReadyToPass(GameObject player);
}