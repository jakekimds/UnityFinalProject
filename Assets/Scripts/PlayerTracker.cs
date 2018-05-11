using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTracker : MonoBehaviour {

	public static Transform playerTransform;

	// Use this for initialization
	private void Awake() {
		playerTransform = transform;
	}
}
