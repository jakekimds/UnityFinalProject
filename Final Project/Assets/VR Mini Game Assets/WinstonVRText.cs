using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinstonVRText : MonoBehaviour {
	public Text OuchText;
	public int num;
	// Use this for initialization
	void Start () {
		num = Random.Range (1, 6);
		if (num == 1) {
			OuchText.text = "Please don't hurt me.";
		}
		if (num == 2) {
			OuchText.text = "I just wanted a raise.";
		}
		if (num == 3) {
			OuchText.text = "Boss. Please. No.";
		}
		if (num == 4) {
			OuchText.text = "I'm just an I.T. guy.";
		}
		if (num == 5) {
			OuchText.text = "You don't have to do this.";
		}

	}
}