using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableButton : InteractableController {
	public GameObject schlong;

	public override void InteractAction (GameObject player)
	{
		schlong.SetActive (false);
	}
}
