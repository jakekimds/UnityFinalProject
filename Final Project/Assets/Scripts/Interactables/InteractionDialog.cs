using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InteractionDialog : InteractableController {

	[TextArea(3, 10)]
	public string[] text;

	private int currentIndex;
	private bool isActive;

	private GUIManager gui;
	private DialogReactor dialogReactor;

	public override void InteractStart(){
		gui = GUIManager.instance;
		dialogReactor = GetComponent<DialogReactor> ();
	}

	public override void InteractAction(GameObject player) {
		currentIndex = 0;
		isActive = true;
		PlayerController.instance.fpController.enabled = false;
		PlayerController.instance.fpLook.enabled = true;
		PlayerController.instance.interactionController.canInteract = false;
		gui.showDirections (false, 0);
		gui.showDialog (false);
		gui.dialog (text[currentIndex]);
		gui.directions("Press Enter to Continue", 0);
		if (dialogReactor != null) {
			dialogReactor.dialogStart ();
			dialogReactor.dialogChanged (currentIndex);
		}
	}

	public override void InteractUpdate() {
		if (isActive) {
			doEnter ();
		}
	}

	void doEnter() {
		if (Input.GetKeyDown(KeyCode.Return)) {
			gui.showDirections(false, 0);
			currentIndex++;
			if (currentIndex >= text.Length) {
				isActive = false;
				gui.showDirections (false, 0);
				gui.showDialog (false);
				PlayerController.instance.fpController.enabled = true;
				PlayerController.instance.fpLook.enabled = false;
				PlayerController.instance.interactionController.canInteract = true;
				if (dialogReactor != null) {
					dialogReactor.dialogEnd ();
				}
			} else {
				gui.dialog (text[currentIndex]);
				gui.directions("Press Enter to Continue", 0);
				if (dialogReactor != null) {
					dialogReactor.dialogChanged (currentIndex);
				}
			}
			return;
		}
	}
}
