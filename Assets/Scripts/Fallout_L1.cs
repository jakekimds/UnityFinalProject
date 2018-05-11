using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Fallout_L1 : MonoBehaviour {
	void OnTriggerEnter(Collider other) {
		SceneManager.LoadScene("Level1");
	}
}