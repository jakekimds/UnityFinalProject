using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Callback : MonoBehaviour {

	public Callback nextCallBack;
	[HideInInspector]public bool OnlyOnce;

	private bool triggered;

	private void Start() {
		CallStart();
		triggered = false;
	}

	private void Update() {
		CallUpdate();
	}

	public void Call() {
		if (!(OnlyOnce && triggered)) {
			OnCall();
			triggered = true;
			Callback.Call(nextCallBack);
		}
	}

	public abstract void OnCall();

	public virtual void CallStart() {
		
	}

	public virtual void CallUpdate() {

	}

	public static void Call(Callback callback) {
		if (callback != null) {
			callback.Call();
		}
	}
}
