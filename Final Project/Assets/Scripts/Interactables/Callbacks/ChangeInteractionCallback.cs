using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeInteractionCallback : Callback {

	public Callback NewCallback;
	public InteractableController Interactable;

	public override void CallStart() {
		if (Interactable == null) {
			Interactable = GetComponent<InteractableController>();
		}
	}

	public override void OnCall() {
		Interactable.SetCallback(NewCallback);
	}

}
