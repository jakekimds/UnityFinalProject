using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Logo1Controller : MonoBehaviour {

	public Rigidbody winstonRB;
	public Animator winstonAnim;
	public GameObject I;
	public Collider Icol;
	public AudioSource aud;

	private int jumpTimes;
	private bool shrinking;
	private float oPos;

	// Use this for initialization
	void Start () {
		shrinking = false;
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}

	private void Update() {
		if (shrinking) {
			float scale = (transform.position.y - I.transform.position.y) / (oPos - I.transform.position.y);
			if (scale > 0) {
				I.transform.localScale = new Vector3(100, 100 * scale, 100);
			} else {
				I.SetActive(false);
				shrinking = false;
			}
		}
	}

	private void OnCollisionEnter(Collision collision) {
		jumpTimes++;
		if (jumpTimes >= 7) {
			winstonAnim.SetBool ("jumping", false);
			SceneManager.LoadScene ("Logo2");
		} else if (jumpTimes >= 6) {
			shrinking = true;
			oPos = transform.position.y;
			Icol.enabled = false;
			return;
		} else {
			aud.Play ();
		}
		winstonRB.velocity = Vector3.zero;
		winstonRB.AddForce(winstonRB.transform.up * 2f, ForceMode.VelocityChange);
		winstonAnim.SetTrigger("jump");
	}
}
