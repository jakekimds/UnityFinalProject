﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFlashlightController: MonoBehaviour {

	public Light flashlight;
	bool on = false;

	public bool lightEnabled = false;

	private void Start() {
		flashlight.enabled = on;
		lightEnabled = false;
	}

	void Update()
	{
		if (lightEnabled) {
			if (Input.GetKeyDown(KeyCode.F)) {
				on = !on;
				flashlight.enabled = on;
			}
		}
	}
}