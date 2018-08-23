using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTouchDeath : MonoBehaviour {

	public bool KillOnCollision = false;
	public bool KillOnTrigger = false;

    public Callback OnKill;

	private void OnTriggerEnter(Collider other) {
		if (KillOnTrigger && other.CompareTag("Player")) {
			PlayerTracker.instance.Die();
            Callback.Call(OnKill);
		}
	}
	private void OnCollisionEnter(Collision collision) {
		if (KillOnTrigger && collision.gameObject.CompareTag("Player")) {
            PlayerTracker.instance.Die();
            Callback.Call(OnKill);
		}
	}

}
