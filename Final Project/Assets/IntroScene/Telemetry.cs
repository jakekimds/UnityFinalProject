using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Data", menuName = "Inventory/List", order = 1)]
public class Telemetry : ScriptableObject {
	public List<string> data = new List<string>();
}