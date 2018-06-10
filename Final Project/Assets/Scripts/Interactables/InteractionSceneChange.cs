using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InteractionSceneChange : InteractableController {

	public string sceneName = "MainMenu";

	public override void InteractAction(GameObject player) {
		SceneManager.LoadScene(sceneName);
	}
}
