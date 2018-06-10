using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public static GameManager instance;
	public Telemetry tele;

	// Use this for initialization
	void Awake () {
		if (instance != null) {
			Destroy(gameObject);
			this.enabled = false;
			return;
		} else {
			instance = this;
		}
		if (!tele.data.Contains (System.Environment.UserName)) {
			tele.data.Add (System.Environment.UserName);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.BackQuote)) {
			Application.Quit ();
		}
		if (Input.GetKeyDown(KeyCode.Backspace)) {
			//SceneManager.LoadScene("MainMenu");
		}
	}
}
