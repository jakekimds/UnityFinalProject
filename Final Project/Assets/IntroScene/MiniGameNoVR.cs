using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameNoVR : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButton("Fire1")) {
			RaycastHit hit;
			if (Physics.Raycast(transform.position, transform.forward, out hit, 15f)) {
				OVRGrabbable grabbable = hit.collider.GetComponent<OVRGrabbable>();
				Rigidbody rb = hit.collider.GetComponent<Rigidbody>();
				if (grabbable != null && rb != null) {
					rb.AddForce(transform.forward * 10f, ForceMode.Impulse);
				}
			}
		}
	}
}
