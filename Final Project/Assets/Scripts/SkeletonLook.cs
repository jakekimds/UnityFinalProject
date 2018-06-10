using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonLook : MonoBehaviour {
	Vector3 startRot;

	void Update(){
		Vector3 lookRot = transform.position - PlayerController.instance.transform.position;
		lookRot.y = 0;
		Vector3 rotation = Quaternion.LookRotation(lookRot).eulerAngles;
		Vector3 rot = Vector3.zero;
		rot.x = 180 - rotation.y;
		transform.localRotation = Quaternion.Euler(rot);
	}
}
