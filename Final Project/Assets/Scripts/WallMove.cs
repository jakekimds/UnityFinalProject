using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallMove : MonoBehaviour {

	public float targetX = 0f;

	bool isMoving;
	bool hasMoved;
	float offset;
	Transform other;
	public InteractableController interaction;
	private AudioSource audioSource;

	// Use this for initialization
	void Start () {
		isMoving = false;
		hasMoved = false;
		audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		if (isMoving) {
			if ((transform.position.x) < targetX) {
				transform.position = new Vector3 (targetX, transform.position.y, transform.position.z);
				isMoving = false;
				hasMoved = true;
				interaction.SetActive(true);
				audioSource.Stop();
			} else {
				if (other.position.x < transform.position.x + offset) {
					transform.position = new Vector3 (other.position.x - offset, transform.position.y, transform.position.z);
				}
			}
		}
	}

	void OnTriggerEnter(Collider otherGO){
		if (!isMoving && !hasMoved && otherGO.CompareTag ("Player")) {
			isMoving = true;
			other = otherGO.transform;
			offset = other.position.x - transform.position.x;
			audioSource.Play();
		}
	}
}
