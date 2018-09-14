using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public abstract class InteractableController : MonoBehaviour {

	[SerializeField] private bool onlyOnce = false;
	[SerializeField] private bool canUse = true;
	[SerializeField] private Callback callback;
	[SerializeField] private string Instructions;
	private float period = 1f;
	Material[] materials;

	void Awake() {
		gameObject.name = gameObject.name + gameObject.GetInstanceID();
		GameData.InteractableObjects = new List<string>();
	}

	void Start(){
		GameData.InteractableObjects.Add(gameObject.name);
		period = 2 * Mathf.PI / period;
		Renderer[] renderers = GetComponentsInChildren<Renderer> ();
		materials = new Material[renderers.Length];
		for (int i = 0; i < renderers.Length; i++) {
			materials [i] = renderers [i].material;
		}
		InteractStart ();
	}
	
	public void Interact(GameObject player) {
		if (canUse) {
			InteractAction (player);
			Callback.Call(callback);
			if (onlyOnce) {
				canUse = false;
			}
		}
	}

	void Update(){
		float brightness;
		brightness = 0;
		if (canUse) {
			brightness = .01f * Mathf.Sin (Time.time * period) + 0.01f;
		}
		for (int i = 0; i < materials.Length; i++) {
			Color baseColor = materials[i].color;

			Color finalColor = baseColor * Mathf.LinearToGammaSpace(brightness);
			materials[i].SetColor("_EmissionColor", finalColor);
		}
		InteractUpdate ();
	}

	public void SetActive(bool active) {
		canUse = active;
	}

	public bool GetActive() {
		return canUse;
	}

	private void OnDrawGizmos() {
		if (gameObject.layer != 9) {
			Debug.LogError(name + " has InteractableController but not on Interactable layer");
		}
	}

	public void SetCallback(Callback newCallback) {
		callback = newCallback;
	}

	public string GetInstructions() {
		return Instructions;
	}

	public abstract void InteractAction(GameObject player);
	public virtual void InteractStart(){}
	public virtual void InteractUpdate(){}
}