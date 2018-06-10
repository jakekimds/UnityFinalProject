using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IfAaronComputerCallback : Callback {

	public string UserName = "glicka0192";
	public Callback IfTrue;
	public Callback IfFalse;

	public override void OnCall(){
		if (System.Environment.UserName.Trim ().Equals (UserName)) {
			Callback.Call (IfTrue);
		} else {
			Callback.Call (IfFalse);
		}
	}

}
