using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {

	public float speed = 10;
	public float max = 10;
	public bool moving = true;
	private Vector3 initialPos;

	// Use this for initialization
	void Start () {
		initialPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (moving) {
			Vector3 pos = transform.position;
			pos.x += speed * Time.deltaTime;
			if (Mathf.Abs(initialPos.x - pos.x) >= max) {
				speed *= -1;
			} else {
				transform.position = pos;
			}
		}
	}

	void OnCollisionEnter(Collision collision){
		if (collision.gameObject.CompareTag ("Player")) {
			moving = false;
		}
	}

	void OnCollisionExit(Collision collision){
		if (collision.gameObject.CompareTag ("Player")) {
			moving = true;
		}
	}
}
