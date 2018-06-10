using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultipleCallbacks : Callback {

	public Callback[] Callbacks;

	public override void OnCall(){
		for (int i = 0; i < Callbacks.Length; i++) {
			Callback.Call (Callbacks[i]);
		}
	}

}
