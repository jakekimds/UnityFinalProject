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
	public string tutorialSceneName;
	private bool enterToAdvance;

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
		if (Input.GetKeyDown (KeyCode.Return)) {
			nextStage ();
		}
		int currentStage = 1;
		if (!stagePlaying) {
			if (!enterToAdvance) {
				if (delay < 0) {
					nextStage();
				}
				delay -= Time.deltaTime;
			} else {
				if (Input.GetKeyDown(KeyCode.Return)) {
					nextStage();
				}
			}
			return;
		}
		if (stage == currentStage++) {
			Cursor.lockState = CursorLockMode.Locked;
			if (!moveTowards(winston, target)) {
				enterToAdvance = true;
				stagePlaying = false;
				anim.SetBool("moving", false);
				messageBox.SetActive(true);
				text.text = "Me: What do you want?";
				winston.transform.position = target.position;
				LookAtXY (winston, player.transform);
				return;
			}
			anim.SetBool("moving", true);
		} else if (stage == currentStage++) {
			text.text = "IT Guy: I want a raise.";
			enterToAdvance = true;
			stagePlaying = false;
		} else if (stage == currentStage++) {
			text.text = "Me: No";
			enterToAdvance = true;
			stagePlaying = false;
		} else if (stage == currentStage++) {
			text.text = "IT Guy: Please...";
			enterToAdvance = true;
			stagePlaying = false;
		} else if (stage == currentStage++) {
			text.text = "Me: Get out of here";
			projectile.GetComponent<Rigidbody>().AddForce((head.position - projectile.transform.position).normalized * force, ForceMode.VelocityChange);
			enterToAdvance = true;
			stagePlaying = false;
		} else if (stage == currentStage++) {
			text.text = "IT Guy: You'll regret this";
			anim.SetBool("angry", true);
			delay = 1f;
			stagePlaying = false;
		} else if (stage == currentStage++) {
			winston.SetActive(false);
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
			messageBox.SetActive(false);
			delay = 1f;
			stagePlaying = false;
		}else if (stage == currentStage++) {
			foreach (Light light in lights) {
				light.intensity = 0f;
			}
			delay = .5f;
			stagePlaying = false;
		}else if (stage == currentStage++) {
			foreach (Light light in lights) {
				light.intensity = 0.4f;
				light.color = color;
			}
			spotLight.enabled = true;
			winstonHead.SetActive(true);
			delay = .5f;
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
