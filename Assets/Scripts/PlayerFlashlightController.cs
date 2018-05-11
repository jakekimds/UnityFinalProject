using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFlashlightController: MonoBehaviour {

	public Light flashlight;
	public Light buLight;
	public bool on = false;

	public GameObject cone;

	public bool lightEnabled = false;
	public bool coneEnabled = false;
	public float coolTime = 1f;
	public float onTime = 2f;

	private bool cooling = false;
	private float counter;

	private void Start() {
		flashlight.enabled = on;
		cone.SetActive(coneEnabled && on);
		counter = onTime;
	}

	void Update()
	{
		if (coneEnabled) {
			if (cooling) {
				if (StatsDisplay.instance != null) {
					StatsDisplay.instance.updateFLCool((coolTime - counter) * 100f / coolTime);
				}
				counter -= Time.deltaTime;
			} else if (on) {
				if (StatsDisplay.instance != null) {
					StatsDisplay.instance.updateFLCool(counter * 100f / onTime);
				}
				counter -= Time.deltaTime;
			} else {
				if (StatsDisplay.instance != null) {
					StatsDisplay.instance.updateFLCool(counter * 100f / onTime);
				}
			}
			if (counter <= 0) {
				if (cooling) {
					counter = onTime;
				} else {
					counter = coolTime;
					setFL(false);
				}
				cooling = !cooling;
			}
		} else {
			if (StatsDisplay.instance != null) {
				StatsDisplay.instance.showSSBox(false);
			}
		}
		if (Input.GetKeyDown(KeyCode.F)) {
			setFL(!on);
		}
		if (coneEnabled) {
			cone.SetActive(coneEnabled && on);
		}
	}

	void setFL(bool on) {
		if (lightEnabled && !cooling) {
			this.on = on;
			flashlight.enabled = on;
			cone.SetActive(coneEnabled && on);
		} else {
			this.on = false;
			flashlight.enabled = false;
			cone.SetActive(false);
		}
	}
}