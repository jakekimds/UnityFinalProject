using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GUITrigger : MonoBehaviour {

	public string directions;
	public float directionsFadeTime = 0;
	[TextArea(3, 10)]
	public string text;
	public bool onlyOnce;
	public float fadeTime = 0;
	private bool triggered;

	void Start(){
		triggered = false;
	}

	void OnTriggerEnter(Collider other){
		if (!(onlyOnce && triggered)) {
			if (other.CompareTag("Player")) {
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
				triggered = true;
			}
		}
	}

	public bool getTriggered() {
		return triggered;
	}
}
