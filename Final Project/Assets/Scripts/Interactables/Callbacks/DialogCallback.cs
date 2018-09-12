using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogAction {
	[TextArea(3, 10)]
	public string text;
	public Callback callback;
	public bool EasterEgg = false;
}

[System.Serializable]
public class DialogCallback : Callback {
	public DialogAction[] text;
	public Callback EndCallback;

	private int currentIndex;
	private bool isActive;

	private GUIManager gui;
	private DialogReactor dialogReactor;

	public override void CallStart() {
		gui = GUIManager.instance;
		dialogReactor = GetComponent<DialogReactor>();
	}

	public override void OnCall() {
		currentIndex = 0;
		while ((currentIndex < text.Length && !GameData.cactusMode && text[currentIndex].EasterEgg)) {
			currentIndex++;
		}
		isActive = true;
		PlayerTracker.instance.fpController.enabled = false;
		PlayerTracker.instance.fpLook.enabled = true;
		PlayerTracker.instance.interactionController.canInteract = false;
		PlayerTracker.instance.rb.velocity = Vector3.zero;
		gui.showDirections(false, 0);
		gui.showDialog(false);
		gui.dialog(text[currentIndex].text);
		Callback.Call(text[currentIndex].callback);
		gui.directions("Press Enter to Continue", 0);
		if (dialogReactor != null) {
			dialogReactor.dialogStart();
			dialogReactor.dialogChanged(currentIndex);
		}
	}

	public override void CallUpdate() {
        if (isActive && !(Mathf.Abs(Time.timeScale) < float.Epsilon)) {
			doEnter();
		}
	}

	void doEnter() {
		if (Input.GetKeyDown(KeyCode.Return)) {
			gui.showDirections(false, 0);
			currentIndex++;
			while ((currentIndex < text.Length && !GameData.cactusMode && text[currentIndex].EasterEgg)) {
				currentIndex++;
			}
			if (currentIndex >= text.Length) {
				isActive = false;
				gui.showDirections(false, 0);
				gui.showDialog(false);
				PlayerTracker.instance.fpController.enabled = true;
				PlayerTracker.instance.fpLook.enabled = false;
				PlayerTracker.instance.interactionController.canInteract = true;
				if (dialogReactor != null) {
					dialogReactor.dialogEnd();
				}
				Callback.Call(EndCallback);
			} else {
				Callback.Call(text[currentIndex].callback);
				gui.dialog(text[currentIndex].text);
				gui.directions("Press Enter to Continue", 0);
				if (dialogReactor != null) {
					dialogReactor.dialogChanged(currentIndex);
				}
			}
			return;
		}
	}
}
