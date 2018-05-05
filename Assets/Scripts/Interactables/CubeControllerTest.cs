using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CubeControllerTest : InteractableController {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public override void InteractAction(GameObject player) {
		SceneManager.LoadScene("MainMenu");
	}
}
