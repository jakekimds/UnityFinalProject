using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsDisplay : MonoBehaviour {

	public static StatsDisplay instance;

	public GameObject ssBox;
	public RectTransform FLBar;
	public RectTransform FLBG;

	public GameObject redFlash;
	private float countdown;

	void Awake() {
		if (instance != null) {
			Destroy(gameObject);
			this.enabled = false;
			return;
		} else {
			instance = this;
		}
	}

	// Use this for initialization


	public void updateFLCool(float percent) {
		ssBox.SetActive(true);
		float width = percent / 100 * FLBG.sizeDelta.x;
		FLBar.sizeDelta = new Vector2(width, FLBG.sizeDelta.y);
	}

	public void showSSBox(bool show) {
		ssBox.SetActive(show);
	}

	public void healthFlash() {
		countdown = 0.1f;
	}
	
	// Update is called once per frame
	void Update () {
		if (countdown <= 0) {
			redFlash.SetActive(false);
		} else {
			countdown -= Time.deltaTime;
			redFlash.SetActive(true);
		}
	}
}
