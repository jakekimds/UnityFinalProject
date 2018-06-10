using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StartGUI : MonoBehaviour {

	public float Delay = 0f;
	public string directions;
	public float directionsFadeTime = 0;
	[TextArea(3, 10)]
	public string text;
	public float fadeTime = 0;

	private float countdown;
	private bool triged;

	void Start(){
		countdown = Delay;
		triged = false;
	}

	void Update(){
		countdown -= Time.deltaTime;
		if (countdown <= 0 && !triged) {
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
			triged = true;
		}
	}
}
