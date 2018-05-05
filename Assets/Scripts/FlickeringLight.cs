using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickeringLight : MonoBehaviour {

	public int minFrames;
	public int maxFrames;

	Light light;
	bool lightOn;
	int framesToToggle;

	// Use this for initialization
	void Start () {
		light = GetComponent<Light> ();
		lightOn = true;
		framesToToggle = Random.Range (minFrames, maxFrames);
	}
	
	// Update is called once per frame
	void Update () {
		if (framesToToggle <= 0) {
			framesToToggle = Random.Range (minFrames, maxFrames);
			lightOn = !lightOn;
			light.enabled = lightOn;
		} else {
			framesToToggle--;
		}
	}
}
