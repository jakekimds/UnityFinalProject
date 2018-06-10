using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.VR;

public class MainMenuManager : MonoBehaviour {

	private KeyCode[] code;
	private int codeIndex;
	private bool codeUsed;
	public GameObject winstonHead;
	public GameObject cactus;
	public GameObject credits;
	public GameObject clippy;
	public GameObject loadScreen;
	public AudioSource whispers;

	// Use this for initialization
	void Start () {
		clippy.SetActive(false);
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
		code = new KeyCode[]{
			KeyCode.UpArrow,
			KeyCode.UpArrow,
			KeyCode.DownArrow,
			KeyCode.DownArrow,
			KeyCode.LeftArrow,
			KeyCode.RightArrow,
			KeyCode.LeftArrow,
			KeyCode.RightArrow,
			KeyCode.B,
			KeyCode.A,
		};
		codeUsed = false;

		if (GameData.cactusMode) {
			cactus.SetActive(true);
			GameData.cactusMode = true;
			winstonHead.SetActive(false);
			codeUsed = true;
			whispers.Play();
		}
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown(KeyCode.H)) {
			clippy.SetActive(!clippy.activeInHierarchy);
		}	

		if (Input.GetKeyDown (KeyCode.F1)) {
			PlayerPrefs.SetInt ("EasterEgg", 0);
			PlayerPrefs.Save ();
		}

		if (codeEntered()) {
			if (!codeUsed) {
				cactus.SetActive(true);
				GameData.cactusMode = true;
				winstonHead.SetActive(false);
				codeUsed = true;
				whispers.Play();

			} else {
				cactus.SetActive(false);
				GameData.cactusMode = false;
				winstonHead.SetActive(true);
				codeUsed = false;
				whispers.Stop();
			}
		}
	}

	public void showQuitScreen() {
		credits.SetActive(true);
	}

	public void hideQuitScreen() {
		credits.SetActive(false);
	}

	public bool codeEntered() {
		if (codeIndex >= code.Length) {
			codeIndex = 0;
			return true;
		} else {
			if (Input.GetKeyDown(code[codeIndex])) {
				codeIndex++;
			} else if (Input.anyKeyDown) {
				codeIndex = 0;
			}
			return false;
		}
	}

	public void StartGame() {
		loadScreen.SetActive (true);
		SceneManager.LoadScene("THX");
	}

	public void PlayMiniGame() {
		loadScreen.SetActive (true);
		if (VRDevice.isPresent) {
			SceneManager.LoadScene("MiniGame");
		} else {
			SceneManager.LoadScene("MiniGameNoVR");
		}
	}
}
