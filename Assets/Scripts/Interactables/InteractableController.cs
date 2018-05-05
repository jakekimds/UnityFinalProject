using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractableController : MonoBehaviour {

	[SerializeField] private bool onlyOnce = false;
	[SerializeField] private bool canUse = true;
	
	public void Interact(GameObject player) {
		if (canUse) {
			InteractAction(player);
			if (onlyOnce) {
				canUse = false;
			}
		}
	}

	public void SetActive(bool active) {
		canUse = active;
	}

	public bool GetActive() {
		return canUse;
	}

	public abstract void InteractAction(GameObject player);
}