using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CactusEasteregg : MonoBehaviour {

	public int showTime = 2;
	public Transform player;
	public GameObject view;
	private int countDown = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (countDown > 0) {
			countDown -= 1;
		} else {
			view.SetActive(false);
		}

		if (Input.GetKeyDown(KeyCode.H) && GameData.cactusMode) {
			countDown = showTime;
            GameManager.i.SendAction("EE Found", "Hidden Cacti Found");
			foreach (Transform child in view.transform) {
				child.LookAt(player);
			}
			view.SetActive(true);
		}
	}
}
