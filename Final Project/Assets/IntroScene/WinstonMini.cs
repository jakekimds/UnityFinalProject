using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinstonMini : MonoBehaviour {

	public float maxX = 1f;
	public float minX = -1f;

	private float x;
	private float deltaX;
	private int score = 0;
	
	// Update is called once per frame
	void Update () {
		deltaX = Mathf.Abs(transform.position.x - x);
		x = transform.position.x;
		if (x > maxX || x < minX) {
			Vector3 ang = transform.eulerAngles + new Vector3(0, 180, 0);
			transform.rotation = Quaternion.Euler(ang);
			transform.position += transform.forward * (deltaX + 0.01f);
		}
	}

	private void OnCollisionEnter(Collision collision) {
		if (collision.gameObject.GetComponent<OVRGrabbable>() != null) {
			if (collision.gameObject.GetComponent<Rigidbody>().velocity.magnitude >= 1f) {
				score++;
				GUIManager.instance.dialog("Hit!", 3);
				GUIManager.instance.directions("Score: " + score, 3);
			}
		}
	}
}
