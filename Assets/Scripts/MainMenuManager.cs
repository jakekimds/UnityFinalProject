using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour {

	private KeyCode[] code;
	private int codeIndex;
	private bool codeUsed;
	public GameObject winstonHead;
	public GameObject cactus;
	public GameObject credits;
	public GameObject clippy;

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
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown(KeyCode.H)) {
			clippy.SetActive(!clippy.activeInHierarchy);
		}	

		if (codeEntered()) {
			if (!codeUsed) {
				cactus.SetActive(true);
				GameData.cactusMode = true;
				winstonHead.SetActive(false);
				codeUsed = true;
			} else {
				cactus.SetActive(false);
				GameData.cactusMode = false;
				winstonHead.SetActive(true);
				codeUsed = false;
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
		SceneManager.LoadScene("Intro");
	}

	public void PlayMiniGame() {
		SceneManager.LoadScene("MiniGame");
	}
}
