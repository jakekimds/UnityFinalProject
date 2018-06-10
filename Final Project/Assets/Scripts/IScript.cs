using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IScript : MonoBehaviour {

	public Transform winston;
	public Transform top;
	

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		float scale = (winston.transform.position.y - transform.position.y) / (top.transform.position.y - transform.position.y);
		scale = Mathf.Min(scale, 1);
		if (scale > 0) {
			transform.localScale = new Vector3(100, 100 * scale, 100);
		} else {
			gameObject.SetActive(false);
		}
	}
}
