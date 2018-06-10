using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OutroSceneController : MonoBehaviour {

	public GameObject fire;
	public GameObject WinSkel;
	public GameObject WinScale;
	public GameObject SkelScale;
	public Animator Winston;
	public Animator Skel;
	public Transform skelTarget;
	public Transform door;
	public Transform player;
	public PlayerFlashlightController flController;
	public Transform flashlight;
	public GameObject QPanel;
	public Transform FSTarget;
	public GameObject cacti;
	public AudioSource doorCreak;
	public AudioClip outroMusic;
	public AudioSource music;
	public Transform money;
	public Transform moneyTarget;

	private int currentStage;
	private float countDown;
	private bool countingDown;
	private bool waitingForEnter;
	private bool stagePlaying;
	private bool stageStart;
	private float directionCountDown;
	private GUIManager gui;
	private bool qAnswered = false;
	private bool raiseGiven;
	private float doorSpeed;

	void stages() {
		if (!stagePlaying) {
			return;
		}
		int stage = 0;
		if (++stage == currentStage) {
			Vector3 rot = door.eulerAngles;
			if (stageStart) {
				doorCreak.Play ();
			}
			if (rot.y >= 270) {
				door.rotation = Quaternion.Euler(new Vector3(0, 270, 0));
				nextStage();
				return;
			} else {
				door.rotation = Quaternion.Euler(new Vector3(0, rot.y + doorSpeed * Time.deltaTime, 0));
			}
		} else if (++stage == currentStage) {
			if (stageStart) {
				Skel.SetBool("Walking", true);
			}
			if (!moveTowards(WinSkel.transform, skelTarget.transform, 2f, true)) {
				Skel.SetBool("Walking", false);
				nextStage();
			}
		} else if (++stage == currentStage) {
			GUIManager.instance.dialog("Skeleton: That's my flashlight!");
			Skel.SetTrigger("Spell");
			nextStage();
		} else if (++stage == currentStage) {
			if (stageStart) {
				flController.lightEnabled = false;
				flController.flashlight.enabled = false;
				PlayerFlashlightController.on = false;
				flashlight.SetParent (null, true);
			}
			if (!moveTowards(flashlight, FSTarget, 2f, false)) {
				flashlight.gameObject.SetActive(false);
				waitForEnter();
			}
		} else if (++stage == currentStage) {
			gui.dialog("Me: Who are you?");
			waitForEnter();
		} else if (++stage == currentStage) {
			gui.dialog("Skeleton: You don't recognize me?");
			waitForEnter();
		} else if (++stage == currentStage) {
			gui.dialog("Me: No.");
			waitForEnter();
		} else if (++stage == currentStage) {
			if (stageStart) {
				gui.dialog("Skeleton: How about now?");
				WinScale.SetActive(true);
				Winston.Play("Idle", 0, 0);
				Skel.Play("Idle", 0, 0);
			}
			waitForEnter();
		} else if (++stage == currentStage) {
			gui.dialog("Me: IT Guy? Why are you doing this?");
			waitForEnter();
		} else if (++stage == currentStage) {
			gui.dialog("IT Guy: You didn't give me a raise. I'll give you a second chance.");
			waitForEnter();
		} else if (++stage == currentStage) {
			gui.dialog("IT Guy: Will you give me a raise?");
			waitForEnter();
		} else if (++stage == currentStage) {
			if (stageStart) {
				Cursor.lockState = CursorLockMode.None;
				Cursor.visible = true;
				Time.timeScale = 0;
				gui.showDialog(false);
				gui.showDirections(false, 0);
				QPanel.SetActive(true);
				qAnswered = false;
			} else if (qAnswered) {
				Time.timeScale = 1;
				Cursor.lockState = CursorLockMode.Locked;
				Cursor.visible = false;
				nextStage();
			}
		} else if (++stage == currentStage) {
			if (raiseGiven) {
				if (stageStart) {
					music.clip = outroMusic;
					music.Play ();
					money.gameObject.SetActive(true);
					SkelScale.SetActive (false);
				}
				if (!moveTowards(money, moneyTarget, 1f, false)) {
					gui.dialog("IT Guy: YAY!");
					waitForEnter();
				}
			} else {
				fire.SetActive(true);
				if (GameData.cactusMode) {
					cacti.SetActive(true);
				}
				WinSkel.SetActive(false);
				gui.dialog("IT Guy: I'm sorry boss.");
				waitForEnter();
			}
		} else if (++stage == currentStage) {
			gui.fadeOut(1f);
			waitForSeconds(1f);
		} else if (++stage == currentStage) {
			SceneManager.LoadScene("MainMenu");
		}
		if (stage == currentStage) {
			stageStart = false;
		}
	}

	public void giveRaise(bool raise) {
		raiseGiven = raise;
		qAnswered = true;
		QPanel.SetActive(false);
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
		doorSpeed = 90/doorCreak.clip.length;
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
