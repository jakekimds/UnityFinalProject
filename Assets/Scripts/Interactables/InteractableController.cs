using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractableController : MonoBehaviour {

	[SerializeField] private bool onlyOnce = false;
	private bool used = false;
	
	public void Interact(GameObject player) {
		if (!onlyOnce || !used) {
			InteractAction(player);
			used = true;
		}
	}

	public abstract void InteractAction(GameObject player);
}