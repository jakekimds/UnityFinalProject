using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickeringLight : MonoBehaviour {

	public int minFrames;
	public int maxFrames;

	Light lightComp;
	bool lightOn;
	int framesToToggle;

	// Use this for initialization
	void Start () {
		lightComp = GetComponent<Light> ();
		lightOn = true;
		framesToToggle = Random.Range (minFrames, maxFrames);
	}
	
	// Update is called once per frame
	void Update () {
		if (framesToToggle <= 0) {
			framesToToggle = Random.Range (minFrames, maxFrames);
			lightOn = !lightOn;
			lightComp.enabled = lightOn;
		} else {
			framesToToggle--;
		}
	}
}
