using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour {

	public static GUIManager instance;

	public Text dialogText;
	public GameObject dialogBackground;

	public Text directionText;
	public GameObject directionBackground;
	public float directionPulsePeriod = 1;
	public CanvasGroup directiongroup;

	public RectTransform staminaBarBG;
	public RectTransform staminaBar;

	public GameObject dot;

	public Image fade;
	public GameObject fadeObj;
	public GameObject interaction;
	public Text interactionDirections;

	public GameObject quiz;
	public Text quizQuestion;
	public Text quizAnswer;

	private float dialogShowCountdown;
	private bool dialogShowCountingdown;


	private float directionShowCountdown;
	private bool directionShowCountingdown;

	private float fadeOpacity;
	private float fadeDelta;

	private bool fadingToScene;
	private string fadeScene;

	// Use this for initialization
	void Awake () {
		dialogShowCountdown = 0;
		directionPulsePeriod = Mathf.PI /directionPulsePeriod;
		dialogShowCountingdown = false;
		directionShowCountdown = 0;
		directionShowCountingdown = false;
		if (instance != null) {
			Destroy (gameObject);
			this.enabled = false;
			return;
		} else {
			instance = this;
		}
		fadingToScene = false;
	}

	private void Update() {

		if (directionShowCountingdown) {
			directionShowCountdown -= Time.deltaTime;
			if (directionShowCountdown <= 0) {
				showDirections(false, 0);
			}
		}

		if (directionBackground.activeInHierarchy) {
			directiongroup.alpha = .4f * Mathf.Cos(Time.time * directionPulsePeriod) + .8f;
		}

		if (dialogShowCountingdown) {
			dialogShowCountdown -= Time.deltaTime;
			if (dialogShowCountdown <= 0) {
				showDialog(false);
			}
		}
		fadeOpacity += Time.deltaTime * fadeDelta;
		fadeOpacity = Mathf.Clamp(fadeOpacity, 0, 1);
		if (fadeOpacity <= 0) {
			fade.enabled = false;
			fadeObj.SetActive(false);
		} else {
			fade.enabled = true;
			fadeObj.SetActive(true);
			Color color = fade.color;
			color.a = fadeOpacity;
			fade.color = color;
			if (fadeOpacity >= 1f - float.Epsilon && fadingToScene) {
				UnityEngine.SceneManagement.SceneManager.LoadScene (fadeScene);
			}
		}

	}

	public void directions(string newText, float time) {
		directionText.text = newText;
		showDirections(true, time);
	}


	public void showDirections(bool show, float time) {
		directionBackground.SetActive(show);
		if (time > 0) {
			directionShowCountingdown = true;
			directionShowCountdown = time;
		} else {
			directionShowCountingdown = false;
		}
	}

	public void dialog(string newText) {
		dialog(newText, 0);
	}

	public void dialog(string newText, float time) {
		setDialogText(newText);
		showDialog(true, time);
	}

	public void setDialogText(string newText){
		dialogText.text = newText;
	}

	public void showDialog(bool show) {
		showDialog(show, 0);
	}

	public void showDialog(bool show, float time) {
		dialogBackground.SetActive(show);
		if (time > 0) {
			dialogShowCountingdown = true;
			dialogShowCountdown = time;
		} else {
			dialogShowCountingdown = false;
		}
	}

	public void showInteraction(bool show) {
		interaction.SetActive(show);
	}

	public void showDot(bool show) {
		dot.SetActive(show);
	}

	public void setFade(float alpha) {
		fade.enabled = alpha >= 0;
		fadeOpacity = alpha;
	}

	public void fadeIn(float time) {
		fadeDelta = -1 / time;
	}

	public void fadeOut(float time) {
		fadeDelta = 1 / time;
	}

	public void setQuizAnswer(string value) {
		quizAnswer.text = value;
	}

	public void setQuizQuestion(string value) {
		quizQuestion.text = value;
	}

	public void showQuiz(bool show) {
		quiz.SetActive(show);
	}

	public void showStamina(bool show){
		staminaBarBG.gameObject.SetActive (show);
	}

	public void setStaminaColor(Color color){
		staminaBar.GetComponent<Image> ().color = color;
	}
	public void setStamina(float stamina){
		Vector2 origSize = staminaBarBG.sizeDelta;
		staminaBar.sizeDelta = new Vector2(stamina * origSize.x, origSize.y);
	}

	public void SetInteractionDirections(string instructions) {
		interactionDirections.text = instructions;
	}

	public void fadeToScene(string scene, float time){
		fadeOut (time);
		fadeScene = scene;
		fadingToScene = true;
	}
}
