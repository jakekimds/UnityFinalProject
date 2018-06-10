using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountdownCallback : Callback {

	public float CountDownTime;
	public Callback CallBack;
	public bool Loop;

	private bool countingdown;
	private float countdown;

	public override void CallStart(){
		countingdown = false;
	}

	public override void OnCall(){
		countingdown = true;
		countdown = CountDownTime;
	}

	public override void CallUpdate(){
		if (countingdown) {
			countdown -= Time.deltaTime;
			if (countdown <= 0) {
				countingdown = false;
				Callback.Call (CallBack);
				if (Loop) {
					countingdown = true;
					countdown = CountDownTime;
				}
			}
		}
	}
}
