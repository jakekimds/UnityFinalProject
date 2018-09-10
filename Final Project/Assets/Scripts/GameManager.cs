using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using System.Text;

public class GameManager : MonoBehaviour {

	public static GameManager instance;
	public static GameManager i;
	public static string SessionID;
	public static string UserName;

	private bool trueGM = false;


	// Use this for initialization
	void Awake() {
		if (instance != null) {
			Destroy(gameObject);
			this.enabled = false;
			return;
		} else {
			instance = this;
			i = this;
			DontDestroyOnLoad(gameObject);
			trueGM = true;
			SessionID = System.Guid.NewGuid().ToString();
			UserName = System.Environment.UserName;
			GameData.SessionID = System.Guid.NewGuid().ToString();
			GameData.UserName = System.Environment.UserName;
		}
	}

	private void OnEnable() {
		if (trueGM) {
			SceneManager.sceneLoaded += OnSceneLoaded;
		}
	}

	private void OnDisable() {
		if (trueGM) {
			SceneManager.sceneLoaded -= OnSceneLoaded;
		}
	}

	void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
		if (trueGM) {
			SendAction("Scene Entered", scene.name);
		}
	}

	// Update is called once per frame
	void Update() {
		if (Input.GetKeyDown(KeyCode.BackQuote)) {
			Application.Quit();
		}
		if (Input.GetKeyDown(KeyCode.Backspace)) {
			if (Time.timeScale > 0) {
				SceneManager.LoadScene("MainMenu");
			}
		}
	}

	IEnumerator SendPostRequest(string url) {
		if (GameData.networkOn) {
			UnityWebRequest www = UnityWebRequest.Post(url, "");
			yield return www.Send();
			if (www.isError) {
				GameData.networkOn = false;
			}
		}
	}

	public void SendGoogleForm(string formID, Dictionary<string, string> parameters) {
		StringBuilder queryBuilder = new StringBuilder("https://docs.google.com/forms/d/e/");
		queryBuilder.Append(formID);
		queryBuilder.Append("/formResponse?");
		foreach (KeyValuePair<string, string> parameter in parameters) {
			queryBuilder.Append("entry.");
			queryBuilder.Append(parameter.Key);
			queryBuilder.Append("=");
			queryBuilder.Append(WWW.EscapeURL(parameter.Value));
			queryBuilder.Append("&");
		}
		string query = queryBuilder.ToString();
		StartCoroutine(SendPostRequest(query));
	}

	public void SendAction(string title, string action, bool ovr = false) {
		if (PlayerPrefs.GetInt("Tracking", 1) != 0 || ovr) {
			string scene = SceneManager.GetActiveScene().name;
			string username = System.Environment.UserName;
			string sceneTime = "" + Time.timeSinceLevelLoad;

			Dictionary<string, string> parameters = new Dictionary<string, string>{
				{ "2128686995", sceneTime },
				{ "446525886", username },
				{ "1193729513", scene },
				{ "804872919", title },
				{ "27380576", action },
				{ "1285306095", SessionID }
			};

			SendGoogleForm("1FAIpQLScbXmUq5S0XiohDPyJ0YdWkSgQ6ssGl9Who9uFkw-oyv8X-Ag", parameters);
        }
    }
}
