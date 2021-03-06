﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour {

	public string level = "Level4";
    public Callback callback;

	void OnCollisionEnter(Collision collision){
		if (collision.gameObject.CompareTag ("Player") && level != "") {
			SceneManager.LoadScene (level);
            Callback.Call(callback);
		}
	}

	void OnTriggerEnter(Collider other){
		if (other.CompareTag ("Player") && level != "") {
            SceneManager.LoadScene (level);
            Callback.Call(callback);
		}
	}

	void Update(){
		Vector3 newAngle = transform.eulerAngles + new Vector3 (0,0,-180) * Time.deltaTime;
		transform.rotation = Quaternion.Euler (newAngle);
	}
}
