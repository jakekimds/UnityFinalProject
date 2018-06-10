using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Logo2Mover : MonoBehaviour {

	public float speed = 1;
	public GameObject Loading;

	// Use this for initialization
	void Start () {
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.x <= -1) {
			//Loading.SetActive(true);
			//SceneManager.LoadScene("Logo3");
			GUIManager.instance.fadeToScene("Logo3", 1f);
		}
		transform.position += -Vector3.right * speed * Time.deltaTime;
	}
}
