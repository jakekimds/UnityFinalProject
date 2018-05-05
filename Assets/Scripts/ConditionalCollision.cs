using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class ConditionalCollision : MonoBehaviour {
	[TextArea(3, 10)]
	public string text;
	public float fadeTime = 0;

	void Start() {
	}

	void OnCollisionEnter(Collision collision) {
		GameObject other = collision.gameObject;

		if (other.CompareTag("Player")) {
			if (isReadyToPass(other)) {
				gameObject.SetActive(false);
			} else {
				GUIManager.instance.directions(text, fadeTime);
			}
		}
	}

	public abstract bool isReadyToPass(GameObject player);
}