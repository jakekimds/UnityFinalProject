using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Logo2Mover : MonoBehaviour {

	public float speed = 1;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.x <= -1) {
			SceneManager.LoadScene("MainMenu");
		}
		transform.position += -Vector3.right * speed * Time.deltaTime;
	}
}
