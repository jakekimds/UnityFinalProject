using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogoController : MonoBehaviour {

	public string NextLevel;

	void Update () {
		if (Input.GetKeyDown (KeyCode.Return)) {
			UnityEngine.SceneManagement.SceneManager.LoadScene (NextLevel);
		}
	}
}
