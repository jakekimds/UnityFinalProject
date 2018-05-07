using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.SceneManagement;

public class TestController : MonoBehaviour {

	public GameObject winston;
	public float winstonSpeed;
	public GameObject player;
	public Text text;
	private bool stagePlaying = false;
	public Transform target;
	private Animator anim;
	private int stage = 0;
	private float delay = 0;
	public GameObject projectile;
	public Transform head;
	public float force;
	public Light[] lights;
	public Light spotLight;
	public Color color;
	public GameObject messageBox;
	public int currentTarget;
	public GameObject winstonHead;
	public GameObject cactus;
	public string tutorialSceneName;
	private bool enterToAdvance;
	public CanvasGroup introStory;
	public float introStoryFadeTime;

	// Use this for initialization
	void Start () {
		target.position = new Vector3(target.position.x, 0, target.position.z);
		stagePlaying = false;
		anim = winston.GetComponent<Animator>();
		messageBox.SetActive(false);
		text.text = "";
		stage = 0;
		delay = 0;
		currentTarget = 0;
		enterToAdvance = false;

		introStory.gameObject.SetActive(true);
		introStory.alpha = 1;

		Cursor.lockState = CursorLockMode.Locked;
	}

	public void nextStage(){
		enterToAdvance = false;
		stagePlaying = true;
		stage += 1;
	}

	public bool moveTowards(GameObject obj, Transform target){
		float distanceToTarget = Vector3.Distance(obj.transform.position, target.position);
		if (distanceToTarget < winstonSpeed * Time.deltaTime * 5) {
			return false;
		}
		LookAtXY (obj, target);
		obj.transform.position += winston.transform.forward * winstonSpeed * Time.deltaTime;
		return true;
	}

	public void LookAtXY(GameObject obj, Transform target){
		obj.transform.LookAt(new Vector3(target.position.x, obj.transform.position.y, target.position.z));
	}
	
	// Update is called once per frame
	void Update () {
		GUIManager.instance.showDirections(false, 0);
		int currentStage = 1;
		if (!stagePlaying) {

			if (delay < 0) {
				if (!enterToAdvance) {
					nextStage();
				} else {
					GUIManager.instance.directions("Press enter to continue", 0);
				}
			} else {
				delay -= Time.deltaTime;
			}
			if (enterToAdvance) {
				if (Input.GetKeyDown(KeyCode.Return)) {
					nextStage();
				}
			}
			return;
		}
		if (stage == currentStage++) {
			introStory.gameObject.SetActive (true);
			introStory.alpha = 1;
			enterToAdvance = true;
			stagePlaying = false;
		} else if (stage == currentStage++) {
			if (introStory.alpha <= 0) {
				introStory.alpha = 0;
				introStory.gameObject.SetActive (false);
				stagePlaying = false;
				enterToAdvance = false;
				delay = 0;
			} else {
				introStory.alpha -= (1 / introStoryFadeTime) * Time.deltaTime;
			}
		} else if (stage == currentStage++) {
			if (!moveTowards (winston, target)) {
				stagePlaying = false;
				anim.SetBool ("moving", false);
				messageBox.SetActive (true);
				text.text = "Me: What do you want?";
				winston.transform.position = target.position;
				LookAtXY (winston, player.transform);
				return;
			}
			anim.SetBool ("moving", true);
		} else if (stage == currentStage++) {
			text.text = "IT Guy: I want a raise.";
			enterToAdvance = true;
			delay = 1;
			stagePlaying = false;
		} else if (stage == currentStage++) {
			text.text = "Me: No";
			enterToAdvance = true;
			delay = 1;
			stagePlaying = false;
		} else if (stage == currentStage++) {
			text.text = "IT Guy: Please...";
			enterToAdvance = true;
			delay = 1;
			stagePlaying = false;
		} else if (stage == currentStage++) {
			text.text = "Me: Get out of here";
			projectile.GetComponent<Rigidbody> ().AddForce ((head.position - projectile.transform.position).normalized * force, ForceMode.VelocityChange);
			enterToAdvance = true;
			delay = 1;
			stagePlaying = false;
		} else if (stage == currentStage++) {
			text.text = "IT Guy: You'll regret this";
			anim.SetBool ("angry", true);
			delay = 1f;
			stagePlaying = false;
		} else if (stage == currentStage++) {
			winston.SetActive (false);
			foreach (Light light in lights) {
				light.intensity = 0f;
			}
			delay = .5f;
			stagePlaying = false;
		} else if (stage == currentStage++) {
			foreach (Light light in lights) {
				light.intensity = 0.4f;
				light.color = color;
			}
			messageBox.SetActive (false);
			delay = 1f;
			stagePlaying = false;
		} else if (stage == currentStage++) {
			foreach (Light light in lights) {
				light.intensity = 0f;
			}
			delay = .5f;
			stagePlaying = false;
		} else if (GameData.cactusMode) {
			if (stage == currentStage++) {
				foreach (Light light in lights) {
					light.intensity = 0.4f;
					light.color = color;
				}
				spotLight.enabled = true;
				cactus.SetActive(true);
				delay = .1f;
				stagePlaying = false;
			} else if (stage == currentStage++) {
				messageBox.SetActive(true);
				text.text = "Me: What the...";
				delay = 1f;
				stagePlaying = false;
			} else if (stage == currentStage++) {
				cactus.SetActive(false);
				foreach (Light light in lights) {
					light.intensity = 0f;
				}
				spotLight.enabled = false;
				delay = .5f;
				stagePlaying = false;
			} 
		}
		else if (stage == currentStage++) {
			foreach (Light light in lights) {
				light.intensity = 0.4f;
				light.color = color;
			}
			spotLight.enabled = true;
			winstonHead.SetActive(true);
			delay = .1f;
			stagePlaying = false;
		} else if (stage == currentStage++) {
			messageBox.SetActive(true);
			text.text = "Me: What the...";
			delay = 1f;
			stagePlaying = false;
		} else if (stage == currentStage++) {
			messageBox.SetActive(false);
			messageBox.SetActive(false);
			foreach (Light light in lights) {
				light.intensity = 0f;
			}
			spotLight.enabled = false;
			Cursor.lockState = CursorLockMode.None;
			SceneManager.LoadScene(tutorialSceneName);
			delay = .5f;
			stagePlaying = false;
		}
	}
}
