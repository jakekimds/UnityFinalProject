using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;

public class PlayerTracker : MonoBehaviour {

	public static PlayerTracker instance;
	public Rigidbody rb;
	public AudioSource audiosource;

	public RigidbodyFirstPersonController fpController;
	public FirstPersonLook fpLook;
	public InteractionController interactionController;

	// Use this for initialization
	private void Awake() {
		if (instance != null) {
			Destroy(gameObject);
			this.enabled = false;
			return;
		} else {
			instance = this;
		}

		rb = GetComponent<Rigidbody>();
		fpController = GetComponent<RigidbodyFirstPersonController> ();
		fpLook = GetComponent<FirstPersonLook> ();
		interactionController = GetComponent<InteractionController> ();
	}

	public void Start(){
		if (GUIManager.instance == null) {
			Debug.LogError ("GUIManager Required");
		}

		if (GameManager.instance == null) {
			Debug.LogError ("GameManager Required");
		}
	}

	public void Die() {
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
}
