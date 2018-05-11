using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperScorcher: InteractableController {

	public GameObject enemy;
	public Transform spawn;

	public override void InteractAction(GameObject player) {
		PlayerFlashlightController flController = player.GetComponent<PlayerFlashlightController>();
		flController.coneEnabled = true;
		flController.flashlight.spotAngle = 20;
		flController.flashlight.intensity = 10;
		//flController.buLight.enabled = true;
		GUIManager.instance.dialog("What's that?");
		GUIManager.instance.directions("(F to turn on)", 0);
		Instantiate(enemy, spawn.position, enemy.transform.rotation).GetComponent<EnemyController>().detectPlayer() ;
		gameObject.SetActive(false);
	}
}
