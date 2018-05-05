﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour {

	private KeyCode[] code;
	private int codeIndex;
	private bool codeUsed;
	public GameObject winstonHead;
	public GameObject cactus;

	// Use this for initialization
	void Start () {
		Cursor.lockState = CursorLockMode.None;
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
		if (codeEntered()) {
			if (!codeUsed) {
				cactus.SetActive(true);
				winstonHead.SetActive(false);
				codeUsed = true;
			} else {
				cactus.SetActive(false);
				winstonHead.SetActive(true);
				codeUsed = false;
			}
		}
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

	public void QuitGame(){
		Debug.LogWarning ("You can't quit!!!");
	}
}
