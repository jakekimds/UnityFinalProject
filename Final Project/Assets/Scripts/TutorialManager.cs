using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		PauseController.instance.Pause();
		PauseController.instance.ShowControls(true);
	}
	
}
