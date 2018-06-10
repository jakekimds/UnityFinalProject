using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTouchDeath : MonoBehaviour {

	public bool KillOnCollision = false;
	public bool KillOnTrigger = false;

	private void OnTriggerEnter(Collider other) {
		if (KillOnTrigger && other.CompareTag("Player")) {
			PlayerController.instance.Die();
		}
	}
	private void OnCollisionEnter(Collision collision) {
		if (KillOnTrigger && collision.gameObject.CompareTag("Player")) {
			PlayerController.instance.Die();
		}
	}

}
