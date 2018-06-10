using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCounterCallback : Callback {

	public string FlagName;
	public int NewValue;

	public override void CallStart() {
		GameData.InitializeCountersDict();
	}

	public override void OnCall() {
		if (GameData.InteractionCounters.ContainsKey(FlagName)) {
			GameData.InteractionCounters[FlagName] = NewValue;
		} else {
			GameData.InteractionCounters.Add(FlagName, NewValue);
		}
	}
}
