using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour {

	public static bool cactusMode = false;
	public static bool networkOn = true;
	public static string SessionID = "";
	public static string UserName = "";
	public static Dictionary<string, bool> InteractionFlags;
	public static Dictionary<string, int> InteractionCounters;
	public static string nullString;

	public static List<string> InteractableObjects;

	public static void InitializeFlagsDict() {
		if (InteractionFlags == null || InteractionFlags.Count > 0) {
			InteractionFlags = new Dictionary<string, bool>();
			InteractionFlags.Add("CactusMode", cactusMode);
		}
	}

	public static void InitializeCountersDict() {
		if (InteractionCounters == null || InteractionCounters.Count > 0) {
			InteractionCounters = new Dictionary<string, int>();
		}
	}
}
