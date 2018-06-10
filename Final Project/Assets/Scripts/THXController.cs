using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Runtime.InteropServices;

public class THXController : MonoBehaviour {
	
	[DllImport("user32.dll")] 
	static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, int dwExtraInfo);

	public AudioSource sound;
	float countdown = .5f;
	float length;
	bool isFading;
	// Use this for initialization\

	void Start(){
		length = sound.clip.length;
		isFading = false;
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}

	void Update () {
		//keybd_event((byte)175, 0, 0, 0);
		countdown -= Time.deltaTime;
		if (countdown <= 0) {
			if (!isFading && length - sound.time <= .5f) {
				GUIManager.instance.fadeOut (.5f);
				isFading = true;
			}
			if (!sound.isPlaying) {
				SceneManager.LoadScene ("Logo1");
			}
		}
	}
}


