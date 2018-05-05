using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUITrigger : MonoBehaviour {

	[TextArea(3, 10)]
	public string text;
	public bool onlyOnce;
	public float fadeTime = 0;
	private bool used;

	void Start(){
		used = false;
	}

	void OnTriggerEnter(Collider other){
		if (!used) {
			if (other.CompareTag ("Player")) {
				GUIManager.instance.setDialogText (text);
				GUIManager.instance.showDialog (true, fadeTime);
				if (onlyOnce) {
					used = true;
				}
			}
		}
	}
}
