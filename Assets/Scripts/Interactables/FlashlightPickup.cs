using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightPickup : InteractableController {
	public override void InteractAction(GameObject player) {
		player.GetComponent<PlayerFlashlightController>().lightEnabled = true;
		GUIManager.instance.dialog("How do I use this thing?");
		GUIManager.instance.directions("(F to turn on)", 0);
		gameObject.SetActive(false);
	}
}
