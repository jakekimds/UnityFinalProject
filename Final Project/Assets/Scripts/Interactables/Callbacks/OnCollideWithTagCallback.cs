using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCollideWithTagCallback : MonoBehaviour {

	public string Tag;
	public Callback callback;

	void OnCollisionEnter(Collision collision){
		if (collision.gameObject.CompareTag (Tag)) {
			Callback.Call (callback);
		}
	}

	void OnTriggerEnter(Collider other){
		if (other.CompareTag (Tag)) {
			Callback.Call (callback);
		}
	}

}
