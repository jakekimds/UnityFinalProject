using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InteractionGUI : InteractableController {

	public string directions;
	public float directionsFadeTime = 0;
	[TextArea(3, 10)]
	public string text;
	public float fadeTime = 0;

	public override void InteractAction(GameObject player) {
		if (text != "") {
			GUIManager.instance.dialog(text, fadeTime);
		} else {
			GUIManager.instance.showDialog(false, 0);
		}
		if (directions != "") {
			GUIManager.instance.directions(directions, directionsFadeTime);
		} else {
			GUIManager.instance.showDirections(false, 0);
		}
	}
}
