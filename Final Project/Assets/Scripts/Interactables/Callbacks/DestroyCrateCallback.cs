using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class DestroyCrateCallback : Callback {

	public GameObject Crate;
	[HideInInspector] public GameObject cracked;

	public override void CallStart(){
		if (Crate == null) {
			Crate = gameObject;
		}
	}

	public override void OnCall() {
		Instantiate (cracked, Crate.transform.position, Crate.transform.rotation);
		Crate.SetActive (false);
	}
}
