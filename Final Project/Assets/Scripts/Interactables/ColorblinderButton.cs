using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorblinderButton : InteractableController {

	public GameObject Object;
	public GameObject Object1;
	public GameObject Object2;
	public GameObject Object3;
	public GameObject Object4;
	public Material Material1;
	public override void InteractAction (GameObject player)
	{
		GUIManager.instance.dialog ("Great.", 3f);
		Object.GetComponent<MeshRenderer> ().material = Material1;
		Object1.GetComponent<MeshRenderer> ().material = Material1;
		Object2.GetComponent<MeshRenderer> ().material = Material1;
		Object3.GetComponent<MeshRenderer> ().material = Material1;
		Object4.GetComponent<MeshRenderer> ().material = Material1;
	}
}
