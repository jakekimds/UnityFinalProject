using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetActiveMultipleCallback : Callback {

	public GameObject[] Objects;
	public bool SetActive;

	public override void CallStart() {
		if (Objects == null || Objects.Length == 0) {
			Objects = new GameObject[] { gameObject };
		}
	}

	public override void OnCall() {
		foreach (GameObject obj in Objects) {
			obj.SetActive(SetActive);
		}
	}
}
