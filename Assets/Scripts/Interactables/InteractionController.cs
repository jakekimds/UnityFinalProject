using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionController : MonoBehaviour {

	public float range = 10;
	//public float radius = 1;
	public Transform originObject;
	private InteractableController interactingObject;

	// Use this for initialization
	void Start () {
		GUIManager.instance.showDot(true);
	}
	
	// Update is called once per frame
	void Update () {
		interactingObject = getInterationObject();
		GUIManager.instance.showInteraction(interactingObject != null);

		if (Input.GetKeyDown(KeyCode.E) && interactingObject != null) {
			interactingObject.Interact(gameObject);
		}
	}

	InteractableController getInterationObject() {
		RaycastHit hit;
		if (Physics.Raycast(originObject.position, originObject.forward, out hit, range, 1 << 9, QueryTriggerInteraction.Collide)) {
			InteractableController interactionScript = hit.collider.GetComponent<InteractableController>();
			if (interactionScript != null) {
				return interactionScript;
			}
		}
		return null;
	}

	private void OnDrawGizmosSelected() {
		Gizmos.DrawLine(originObject.position, originObject.position+originObject.forward* (range));
		//Gizmos.DrawWireSphere(originObject.position + originObject.forward * (range), radius);
	}
}
