using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GControllerCallBack : Callback {

	public GameObject G;

	public override void OnCall() {
		G.SetActive(true);
	}

}
