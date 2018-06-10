using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionController : MonoBehaviour {

	public float range = 10;
	public bool canInteract = true;
	//public float radius = 1;
	public Transform originObject;
	private InteractableController[] interactingObjects;
	private bool interacting;

	// Use this for initialization
	void Start () {
		if (GUIManager.instance == null) {
			Debug.LogError("Put the Canvas prefab onto this scene");
		}
		interacting = false;
		GUIManager.instance.showDot(true);
	}
	
	// Update is called once per frame
	void Update () {
		interactingObjects = getInterationObjects();
		interacting = false;
		if (interactingObjects != null && canInteract) {
			for (int i = 0; i < interactingObjects.Length; i++) {
				if (interactingObjects[i].GetActive()) {
					interacting = true;
					GUIManager.instance.showInteraction(true);
					string instructions = interactingObjects[i].GetInstructions();
					if (instructions != null) {
						if (instructions.Equals ("")) {
							instructions = "Press E";
						}
					}
					GUIManager.instance.SetInteractionDirections(instructions);
					if ((Input.GetKeyDown(KeyCode.E) || Input.GetButtonDown("Fire1")) && !(Mathf.Abs(Time.timeScale) < float.Epsilon)) {
						interactingObjects[i].Interact(gameObject);
					}
				}
			}
		} 
		if(!interacting){
			GUIManager.instance.showInteraction(false);
		}
	}

	InteractableController[] getInterationObjects() {
		RaycastHit hit;
		if (Physics.Raycast(originObject.position, originObject.forward, out hit, range, 1 << 9, QueryTriggerInteraction.Collide)) {
			InteractableController[] interactionScripts = hit.collider.GetComponents<InteractableController>();
			if (interactionScripts != null && interactionScripts.Length > 0) {
				return interactionScripts;
			}
		}
		return null;
	}

	private void OnDrawGizmosSelected() {
		Gizmos.DrawLine(originObject.position, originObject.position+originObject.forward* (range));
		//Gizmos.DrawWireSphere(originObject.position + originObject.forward * (range), radius);
	}
}
