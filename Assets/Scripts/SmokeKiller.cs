using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeKiller : MonoBehaviour {
	private float countdown;

	void Start () {
		countdown = 1;
	}

	private void Update() {
		countdown -= Time.deltaTime;
		if (countdown <= 0) {
			Destroy(gameObject);
		}
	}
}
