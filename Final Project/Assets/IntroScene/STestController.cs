using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.SceneManagement;

public class STestController : MonoBehaviour {

	public Text IntroStoryTitle;
	public Text IntroStoryText;
	public CanvasGroup IntroStoryGroup;
	public Light[] Lights;
	public GameObject cactus;
	public Renderer cactusRender;
	public Animator cactusAnim;
	public AudioSource auds;
	public AudioClip dadada;

	private int currentStage;
	private float countDown;
	private bool countingDown;
	private bool waitingForEnter;
	private bool stagePlaying;
	private bool stageStart;
	private float directionCountDown;
	private GUIManager gui;

	private float cactColor = 1f;

	void stages() {
		if (!stagePlaying) {
			return;
		}
		int stage = 0;
		if (++stage == currentStage) {
			IntroStoryText.text = "";
			IntroStoryTitle.text = "One year later...";
			IntroStoryGroup.alpha = 1;
			IntroStoryGroup.gameObject.SetActive (true);
			waitForEnter ();
		} else if (++stage == currentStage) {
			if (IntroStoryGroup.alpha <= 0) {
				IntroStoryGroup.alpha = 0;
				IntroStoryGroup.gameObject.SetActive (false);
				waitForSeconds (1);
			} else {
				IntroStoryGroup.alpha -= Time.deltaTime;
			}
		} else if (++stage == currentStage) {
			if (stageStart) {
				for (int i = 0; i < Lights.Length; i++) {
					Lights [i].enabled = false;
				}
			}
			waitForSeconds (.5f);
		} else if (++stage == currentStage) {
			cactus.SetActive (true);
			for (int i = 0; i < Lights.Length; i++) {
				Lights [i].enabled = true;
			}
			waitForSeconds (.5f);
		} else if (++stage == currentStage) {
			gui.dialog ("Me: What the.... I thought it was over!");
			waitForEnter ();
		} else if (++stage == currentStage) {
			gui.dialog ("Cactus: We have a dire problem. We need your skills.");
			waitForEnter ();
		} else if (++stage == currentStage) {
			gui.dialog ("Me: What?");
			waitForEnter ();
		} else if (++stage == currentStage) {
			gui.dialog ("Cactus: A disease.");
			waitForEnter ();
		} else if (++stage == currentStage) {
			gui.dialog ("Cactus: AHHH!");
			nextStage ();
		} else if (++stage == currentStage) {
			cactColor -= Time.deltaTime;
			cactColor = Mathf.Clamp (cactColor, 0, 1);
			Color col = new Color (1, cactColor, cactColor);
			if (cactColor > 0) {
				cactusRender.material.color = col;
			} else {
				cactusAnim.SetTrigger ("Die");
				nextStage ();
			}
		} else if (++stage == currentStage) {
			gui.dialog ("Cactus: Help us!");
			auds.clip = dadada;
			auds.Play ();
			auds.loop = false;
			waitForSeconds (1f);
		} else if (++stage == currentStage) {
			if (stageStart) {
				IntroStoryGroup.gameObject.SetActive (true);
				IntroStoryTitle.text = "To be continued...";
			}
			if (IntroStoryGroup.alpha >= 1) {
				IntroStoryGroup.alpha = 1;
				waitForEnter ();
			} else {
				IntroStoryGroup.alpha += Time.deltaTime / 3;
			}
		} else if (++stage == currentStage) {
			gui.setFade (1);
			nextStage ();
		} else if (++stage == currentStage) {
            GameManager.i.SendAction("Scene Change", "To MainMenu");
			SceneManager.LoadScene ("MainMenu");
		}

		if (stage == currentStage) {
			stageStart = false;
		}
	}

	bool moveTowards(Transform moving, Transform target, float speed, bool face) {
		if (stageStart && face) {
			moving.LookAt(target);
		}
		Vector3 oldPos = moving.position;
		Vector3 newPos = oldPos + (target.position - moving.position).normalized * speed * Time.deltaTime;
		if (Vector3.Distance(oldPos, target.position) < Vector3.Distance(newPos, target.position)) {
			moving.transform.position = target.position;
			return false;
		} else {
			moving.transform.position = newPos;
			return true;
		}
	}

	void Start () {
		currentStage = 0;
		countDown = 0;
		nextStage();
		gui = GUIManager.instance;

	}

	void waitForSeconds(float seconds) {
		countDown = seconds;
		countingDown = true;
		stagePlaying = false;
	}

	void nextStage() {
		currentStage++;
		countingDown = false;
		waitingForEnter = false;
		stagePlaying = true;
		stageStart = true;
	}

	void doCountDown() {
		if (countingDown) {
			if (countDown <= 0) {
				nextStage();
				return;
			}
			countDown -= Time.deltaTime;
		}
	}

	void waitForEnter() {
		waitingForEnter = true;
		stagePlaying = false;
	}

	void doEnter() {
		if (waitingForEnter) {
			if (Input.GetKeyDown(KeyCode.Return)) {
				gui.showDirections(false, 0);
				directionCountDown = 1f;
				nextStage();
				return;
			}
			directionCountDown -= Time.deltaTime;
			if (directionCountDown <= 0) {
				gui.directions("Press Enter to Continue", 0);
			}
		}
	}

	void Update () {
		doEnter();
		doCountDown();
		stages();
	}
}
