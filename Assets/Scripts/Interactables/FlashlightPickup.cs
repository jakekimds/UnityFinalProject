using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightPickup : InteractableController {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public override void InteractAction(GameObject player) {
		player.GetComponent<PlayerFlashlightController>().lightEnabled = true;
		GUIManager.instance.dialog("How do I use this thing? (F to turn on)");
		Destroy(gameObject);
	}
}
