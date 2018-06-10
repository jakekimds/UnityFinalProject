using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseController : MonoBehaviour {

	public static PauseController instance;

	public GameObject PauseMenu;
	public GameObject Controls;
	public GameObject PauseControls;
	public bool AllowPausing = true;

	private float timeScaleBeforePause;
	private CursorLockMode lockModeBeforePause;
	private bool cursorVisible;

	// Use this for initialization
	void Awake () {
		if (instance != null) {
			Destroy (gameObject);
			this.enabled = false;
			return;
		} else {
			instance = this;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (Mathf.Abs(Time.timeScale) < float.Epsilon) return;
		if (Input.GetKeyDown (KeyCode.P) && AllowPausing) {
			Pause ();
		}
	}

	public void Pause(){
		PauseMenu.SetActive (true);
		PauseControls.SetActive (true);
		Controls.SetActive (false);
		timeScaleBeforePause = Time.timeScale;
		Time.timeScale = 0;
		lockModeBeforePause = Cursor.lockState;
		Cursor.lockState = CursorLockMode.None;
		cursorVisible = Cursor.visible;
		Cursor.visible = true;
	}

	public void Unpause(){
		PauseMenu.SetActive (false);
		PauseControls.SetActive (false);
		Controls.SetActive (false);
		Time.timeScale = timeScaleBeforePause;
		Cursor.lockState = lockModeBeforePause;
		Cursor.visible = cursorVisible;
	}

	public void ShowControls(bool show){
		PauseMenu.SetActive (true);
		PauseControls.SetActive (!show);
		Controls.SetActive (show);
	}

	public void ToMainMenu(){
		Unpause ();
		UnityEngine.SceneManagement.SceneManager.LoadScene ("MainMenu");
	}
}
