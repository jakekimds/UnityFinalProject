using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour {

	public static GUIManager instance;

	public Text dialogText;
	public GameObject dialogBackground;

	public Text directionText;
	public GameObject directionBackground;
	public float directionPulsePeriod = 1;
	public CanvasGroup directiongroup;

	public GameObject dot;

	public GameObject interaction;

	private float dialogShowCountdown;
	private bool dialogShowCountingdown;


	private float directionShowCountdown;
	private bool directionShowCountingdown;

	// Use this for initialization
	void Awake () {
		dialogShowCountdown = 0;
		directionPulsePeriod = Mathf.PI /directionPulsePeriod;
		dialogShowCountingdown = false;
		directionShowCountdown = 0;
		directionShowCountingdown = false;
		if (instance != null) {
			Destroy (gameObject);
			this.enabled = false;
			return;
		} else {
			instance = this;
		}
	}

	private void Update() {

		if (directionShowCountingdown) {
			directionShowCountdown -= Time.deltaTime;
			if (directionShowCountdown <= 0) {
				showDirections(false, 0);
			}
		}

		if (directionBackground.activeInHierarchy) {
			directiongroup.alpha = .4f * Mathf.Cos(Time.time * directionPulsePeriod) + .8f;
		}

		if (dialogShowCountingdown) {
			dialogShowCountdown -= Time.deltaTime;
			if (dialogShowCountdown <= 0) {
				showDialog(false);
			}
		}
	}

	public void directions(string newText, float time) {
		directionText.text = newText;
		showDirections(true, time);
	}


	public void showDirections(bool show, float time) {
		directionBackground.SetActive(show);
		if (time > 0) {
			directionShowCountingdown = true;
			directionShowCountdown = time;
		} else {
			directionShowCountingdown = false;
		}
	}

	public void dialog(string newText) {
		dialog(newText, 0);
	}

	public void dialog(string newText, float time) {
		setDialogText(newText);
		showDialog(true, time);
	}

	public void setDialogText(string newText){
		dialogText.text = newText;
	}

	public void showDialog(bool show) {
		showDialog(show, 0);
	}

	public void showDialog(bool show, float time) {
		dialogBackground.SetActive(show);
		if (time > 0) {
			dialogShowCountingdown = true;
			dialogShowCountdown = time;
		} else {
			dialogShowCountingdown = false;
		}
	}

	public void showInteraction(bool show) {
		interaction.SetActive(show);
	}

	public void showDot(bool show) {
		dot.SetActive(show);
	}
}
