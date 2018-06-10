using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetFlagCallback : Callback {

	public string FlagName;
	public bool NewValue;

	public override void CallStart() {
		GameData.InitializeFlagsDict();
	}

	public override void OnCall() {
		if (GameData.InteractionFlags.ContainsKey(FlagName)) {
			GameData.InteractionFlags[FlagName] = NewValue;
		} else {
			GameData.InteractionFlags.Add(FlagName, NewValue);
		}
	}
}
