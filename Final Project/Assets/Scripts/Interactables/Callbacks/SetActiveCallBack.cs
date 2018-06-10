using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetActiveCallBack : Callback {

	public GameObject obj;
	public bool SetActive;

	public override void CallStart() {
		if (obj == null) {
			obj = gameObject;
		}
	}

	public override void OnCall() {
		obj.SetActive(SetActive);
	}
}
