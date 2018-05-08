using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightTutorialBlocker : ConditionalCollision {

	public override bool isReadyToPass(GameObject player) {
		if (player.GetComponent<PlayerFlashlightController>().lightEnabled) {
			return true;
		}
		return false;
	}

}