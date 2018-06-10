using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionalCallback : Callback {

	public string FlagName;
	public bool DefaultMode;
	public Callback IfTrue;
	public Callback IfFalse;

	public override void CallStart() {
		GameData.InitializeFlagsDict();
	}

	public override void OnCall() {
		bool flag = DefaultMode;
		if (GameData.InteractionFlags.ContainsKey(FlagName)) {
			flag = GameData.InteractionFlags[FlagName];
		}
		if (flag) {
			IfTrue.Call();
		} else {
			IfFalse.Call();
		}
	}
}
