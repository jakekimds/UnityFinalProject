using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoulderScript : MonoBehaviour {

	public GameObject stone;

	// Use this for initialization
	void Start () {
		Instantiate (stone, transform.position, transform.rotation);
	}
	
	void OnTriggerExit(Collider other){
		if (other.CompareTag ("Stone")) {
			Instantiate (stone, transform.position, transform.rotation);
		}
	}
}
