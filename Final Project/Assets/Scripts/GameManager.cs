using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class GameManager : MonoBehaviour {

	public static GameManager instance;
    public static GameManager i;

    private bool trueGM = false;

	// Use this for initialization
	void Awake () {
		if (instance != null) {
			Destroy(gameObject);
			this.enabled = false;
			return;
		} else {
			instance = this;
            i = this;
            DontDestroyOnLoad(gameObject);
            trueGM = true;
		}
	}

	private void OnEnable()
	{
        if (trueGM)
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
	}

	private void OnDisable()
	{
        if (trueGM)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
	}

	void OnSceneLoaded(Scene scene, LoadSceneMode mode){
        if (trueGM)
        {
			SendAction("Scene Entered", scene.name);
        }
    }
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.BackQuote)) {
			Application.Quit ();
		}
		if (Input.GetKeyDown(KeyCode.Backspace)) {
			//SceneManager.LoadScene("MainMenu");
		}
	}

    IEnumerator Upload(string title, string action)
    {
        string scene = SceneManager.GetActiveScene().name;
        string username = System.Environment.UserName;
        string sceneTime = ""+Time.timeSinceLevelLoad;

        action = WWW.EscapeURL(action);
        scene = WWW.EscapeURL(scene);
        username = WWW.EscapeURL(username);
        sceneTime = WWW.EscapeURL(sceneTime);
		title = WWW.EscapeURL (title);

		string query = "entry.2128686995="+sceneTime
			+ "&entry.446525886=" + username
            + "&entry.1193729513=" + scene
			+ "&entry.804872919=" + title
            + "&entry.27380576=" + action;

        UnityWebRequest www = UnityWebRequest.Post("https://docs.google.com/forms/d/e/1FAIpQLScbXmUq5S0XiohDPyJ0YdWkSgQ6ssGl9Who9uFkw-oyv8X-Ag/formResponse?" + query, "");


        yield return www.Send();
    }

	public void SendAction(string title, string action, bool ovr = false){
        if (PlayerPrefs.GetInt("Tracking", 1) != 0 || ovr)
        {
            StartCoroutine(Upload(title, action));
        }
    }
}
