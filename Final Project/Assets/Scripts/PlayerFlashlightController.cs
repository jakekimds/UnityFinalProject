using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFlashlightController: MonoBehaviour {

	public Light flashlight;
	public static bool on = false;

	public bool lightEnabled = false;

	private void Start() {
		flashlight.enabled = on;
	}

	void Update()
	{
		if (Mathf.Abs(Time.timeScale) < float.Epsilon) return;
		if (lightEnabled) {
			if (Input.GetKeyDown (KeyCode.F)) {
				on = !on;
				flashlight.enabled = on;
			}
		} else {
			on = false;
			flashlight.enabled = on;
		}
	}
}