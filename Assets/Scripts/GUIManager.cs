using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour {

	public static GUIManager instance;

	public Text dialogText;
	public GameObject dialogBackground;
	public GameObject dot;

	public GameObject interaction;

	private float dialogShowCountdown;
	private bool dialogShowCountingdown;

	// Use this for initialization
	void Awake () {
		dialogShowCountdown = 0;
		dialogShowCountingdown = false;
		if (instance != null) {
			Destroy (gameObject);
			this.enabled = false;
			return;
		} else {
			instance = this;
		}
	}

	private void Update() {
		if (dialogShowCountingdown) {
			dialogShowCountdown -= Time.deltaTime;
			if (dialogShowCountdown <= 0) {
				showDialog(false);
			}
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
