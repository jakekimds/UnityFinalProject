using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CactusDialogPortal : DialogReactor {

	public GameObject portal;
	public InteractionGUI worshipGoogol;

	public override void dialogEnd(){
		PlayerController.instance.transform.position = new Vector3 (1, 0.92f, 10f);
		portal.SetActive(true);
		worshipGoogol.SetActive (true);
	}

}
