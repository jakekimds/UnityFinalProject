using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUICallBack : Callback {
	public string directions;
	public float directionsFadeTime = 2;
	[TextArea(3, 10)]
	public string text;
	public float fadeTime = 2;

	public override void OnCall() {
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
