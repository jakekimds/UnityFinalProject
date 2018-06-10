using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableSetActive : Callback {

	public InteractableController Controller;
	public bool SetActive;

	public override void CallStart() {
		if (Controller == null) {
			Controller = GetComponent<InteractableController>();
		}
	}

	public override void OnCall() {
		if (Controller != null) {
			Controller.SetActive(SetActive);
		}
	}
}

