using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookInteraction : InteractableController {
	public GameObject Book;
	public override void InteractAction(GameObject Player){
		GUIManager.instance.dialog ("Why am I even trying to read this, I don't know how to read", 5f);
	}
}
