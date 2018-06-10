using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCInteraction : InteractableController {
	public GameObject PalletStackRemove;
	public override void InteractAction (GameObject player){
		PalletStackRemove.SetActive (false);
		GUIManager.instance.dialog ("What was that, I think I heard something outside?", 3f);
	}
}
