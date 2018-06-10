using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class StaminaManager : MonoBehaviour {

	public float runTime;
	public float restTime;

	public Color recoveryColor;
	public Color canRunColor;

	private RigidbodyFirstPersonController controller;

	private bool canRun;
	private float sprintSpeed;
	private GUIManager gui;
	private float stamina;

	// Use this for initialization
	void Start () {
		controller = GetComponent<RigidbodyFirstPersonController> ();
		sprintSpeed = controller.movementSettings.RunMultiplier;
		gui = GUIManager.instance;
		AllowRunning ();
		stamina = 1;
		gui.showStamina (false);
	}
	
	// Update is called once per frame
	void Update () {
		if (controller.Running && canRun && controller.Velocity.sqrMagnitude > float.Epsilon) {
			//If running
			gui.showStamina (true);
			stamina -= Time.deltaTime/runTime;
			if (stamina <= 0) {
				BlockRunning ();
				stamina = 0;
			}
		} else {
			//Not running
			stamina += Time.deltaTime/restTime;
			if (stamina >= 1) {
				AllowRunning ();
				stamina = 1;
				gui.showStamina (false);
			}
		}

		stamina = Mathf.Clamp (stamina, 0, 1);
		gui.setStamina (stamina);
	}

	void AllowRunning(){
		canRun = true;
		controller.movementSettings.RunMultiplier = sprintSpeed;
		gui.setStaminaColor (canRunColor);
	}
	void BlockRunning(){
		canRun = false;
		controller.movementSettings.RunMultiplier = 1;
		gui.setStaminaColor (recoveryColor);
	}


}
