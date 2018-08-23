using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogoController : MonoBehaviour {

	public string NextLevel;

	void Update () {
        if (Input.GetKeyDown (KeyCode.Return)) {
            GameManager.i.SendAction("Logo Skipped", "");
			UnityEngine.SceneManagement.SceneManager.LoadScene (NextLevel);
		}
	}
}
