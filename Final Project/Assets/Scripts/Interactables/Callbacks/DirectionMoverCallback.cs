using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionMoverCallback : Callback {

	public Transform ObjectToMove;
	public AudioSource sound;
	public Vector3 Direction;
	public float Distance;
	public float Speed;
	public bool UseTime;
	public float Duration;
	public bool UseSoundLength;

	private bool isMoving = false;
	private float sqrDist;
	private Vector3 initialPosition;

	public override void CallStart() {
		if (ObjectToMove == null) {
			ObjectToMove = transform;
		}

		if (UseTime) {
			Speed = Distance / Duration;
		}

		if (sound == null) {
			sound = GetComponent<AudioSource>();
		}

		if (UseSoundLength && sound != null) {
			Speed = Distance / sound.clip.length;
		}

		sqrDist = Distance * Distance;
		Direction = Direction.normalized;
	}

	public override void CallUpdate() {
		if (isMoving) {
			ObjectToMove.position += Direction * Speed * Time.deltaTime;
			if ((initialPosition - ObjectToMove.position).sqrMagnitude > sqrDist) {
				isMoving = false;
				if (sound != null) {
					sound.Stop();
				}
				ObjectToMove.position = initialPosition + Direction * Distance;
			}
		}
	}

	public override void OnCall() {
		isMoving = true;
		initialPosition = ObjectToMove.position;
		if (sound != null) {
			sound.Play();
		}
	}

}
