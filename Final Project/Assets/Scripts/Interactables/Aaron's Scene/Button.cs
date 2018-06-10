using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : InteractableController {

	// Use this for initialization
	public GameObject walls;
	public override void InteractAction(GameObject Player){
		walls.SetActive (false);
		GUIManager.instance.dialog ("Uh, sounds like a garage opening", 3f);
	}
}
