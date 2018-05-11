using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableButton1 : InteractableController {
	public GameObject schlong1;

	public override void InteractAction (GameObject player)
	{
		schlong1.SetActive (false);
		GUIManager.instance.dialog ("E!");
	}
}
