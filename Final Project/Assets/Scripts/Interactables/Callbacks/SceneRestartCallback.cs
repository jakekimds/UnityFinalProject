using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneRestartCallback : Callback {

	public float FadeOut;

	private float countdown;
	private bool switching;

	public override void CallStart() {
		switching = false;
	}

	public override void OnCall() {
		countdown = FadeOut;
		switching = true;
		GUIManager.instance.fadeOut(FadeOut);
	}

	public override void CallUpdate() {
		if (switching) {
			countdown -= Time.deltaTime;
			if (countdown <= 0) {
				SceneManager.LoadScene(SceneManager.GetActiveScene().name);
			}
		}
	}


}
