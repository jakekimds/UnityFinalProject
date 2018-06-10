using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boxes : InteractableController {
	public GameObject audioS;

	public override void InteractAction(GameObject player){
		Instantiate(audioS, transform.position, transform.rotation);
		gameObject.SetActive (false);
	}
}
