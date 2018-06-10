using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountCallback : Callback {

	public int Count;
	public Callback callback;

	private int count;

	public override void CallStart() {
		count = 0;
	}

	public override void OnCall() {
		count++;
		if (count >= Count) {
			Callback.Call(callback);
		}
	}
}
