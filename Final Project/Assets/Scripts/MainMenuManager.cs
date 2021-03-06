﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.VR;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour {

	private KeyCode[] code;
	private int codeIndex;
	private bool codeUsed;
	public GameObject winstonHead;
	public GameObject cactus;
	public GameObject feedback;
	public GameObject clippy;
	public GameObject loadScreen;
	public Text feedbackField;
	public AudioSource whispers;
	float timescale;
    private bool clippyReleased = false;

	// Use this for initialization
	void Start () {
        PlayerPrefs.SetInt("Tracking", 1);
		GameManager.i.SendAction("Is Tracking", "" + (PlayerPrefs.GetInt("Tracking", 1)>0), true);
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

        if (Input.GetKey(KeyCode.T) && Input.GetKey(KeyCode.R) && Input.GetKeyDown(KeyCode.K) && clippyReleased) {
            if(PlayerPrefs.GetInt("Tracking", 1) == 0){
                PlayerPrefs.SetInt("Tracking", 1);
            }else{
                PlayerPrefs.SetInt("Tracking", 0);
            }
            clippy.SetActive(PlayerPrefs.GetInt("Tracking", 1) == 0);
			GameManager.i.SendAction("Is Tracking",  ""+ (PlayerPrefs.GetInt("Tracking", 1)>0), true);
            clippyReleased = false;
        }else{
            clippyReleased = true;
        }

		if (codeEntered()) {
			if (!codeUsed) {
				cactus.SetActive(true);
				GameData.cactusMode = true;
				winstonHead.SetActive(false);
				codeUsed = true;
				whispers.Play();
                GameManager.i.SendAction("EE Found", "Cactus Mode set to true");

			} else {
				cactus.SetActive(false);
				GameData.cactusMode = false;
				winstonHead.SetActive(true);
				codeUsed = false;
				whispers.Stop();
                GameManager.i.SendAction("EE Found", "Cactus Mode set to false");
			}
		}
	}

	public void showFeedback() {
        GameManager.i.SendAction("Menu Option", "Credits opened.");
		feedback.SetActive(true);
		timescale = Time.timeScale;
		Time.timeScale = 0;
		GameManager.instance.SendAction("Feedback button clicked", "Feedback button clicked");
	}

	public void hideFeedback() {
		feedback.SetActive(false);
		Time.timeScale = timescale;
	}

	public void submitFeedback() {
		GameManager.instance.SendGoogleForm("1FAIpQLSewFLVRYT_3D_4qJSZJKd8sHSUn18i1n5qocqxbOCj9S8Bh5Q", new Dictionary<string, string> {
			{ "1538973523", feedbackField.text},
			{ "68037904", GameManager.SessionID },
			{ "916105115", GameManager.UserName }
		});
		GameManager.instance.SendAction("Feedback Submitted", feedbackField.text);
		hideFeedback();
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
        GameManager.i.SendAction("Menu Option", "Play button pressed");
	}

	public void PlayMiniGame() {
        GameManager.i.SendAction("Menu Option", "Play MiniGame Button Pressed");
		loadScreen.SetActive (true);
		if (VRDevice.isPresent) {
			SceneManager.LoadScene("MiniGame");
		} else {
			SceneManager.LoadScene("MiniGameNoVR");
		}
	}
}
