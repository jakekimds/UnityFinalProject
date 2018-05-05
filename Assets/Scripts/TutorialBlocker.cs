using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class TutorialBlocker : MonoBehaviour {
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
				GUIManager.instance.dialog(text, fadeTime);
			}
		}
	}

	public abstract bool isReadyToPass(GameObject player);
}