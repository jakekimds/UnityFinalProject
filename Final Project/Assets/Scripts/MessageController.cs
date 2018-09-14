using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class MessageController : MonoBehaviour {

	public AudioClip[] sounds;

	int oldMessageCount = int.MaxValue;

	// Use this for initialization
	void Start () {
		StartCoroutine(GetMessages());
	}

	IEnumerator GetMessages() {
		while (GameData.networkOn) {
			if (GUIManager.instance != null) {
				using (UnityWebRequest www = UnityWebRequest.Get("https://docs.google.com/document/d/1uPfVsMdi3f6MvAjuEYy-3CJacGvrmcJ5E5iKtA1IDmI/edit")) {
					yield return www.Send();
					if (www.isError) {
						Debug.Log(www.error);
					} else {
						parseDoc(www.downloadHandler.text);
					}
				}
			}
			yield return new WaitForSeconds(1);
		}
	}


	void parseDoc(string gDocHTML) {
		string startIdentifier = "START\\n(60c1cc7f-72a2-4b45-96cb-94f17aab1cfd)\\n//\\n//\\n";
		string endIdentifier = "\\n//\\n//\\n(e102f448-ec45-4f94-9fbf-e6da9b09a251)\\nEND";

		int startIndex = gDocHTML.IndexOf(startIdentifier) + startIdentifier.Length;
		int endIndex = gDocHTML.IndexOf(endIdentifier, startIndex);
		int length = endIndex - startIndex;

		string rawMessageString = gDocHTML.Substring(startIndex, length);
		rawMessageString = rawMessageString.Replace("\\t", "\t");
		rawMessageString = rawMessageString.Replace("\\n", "\n");
		rawMessageString = rawMessageString.Trim();

		string[] messages = rawMessageString.Split(new string[] { "\n\n" }, StringSplitOptions.None);

		if (messages.Length == 1 && messages[0] == "") {
			messages = new string[0];
		}
		
		checkForNewMessages(messages);
	}

	void checkForNewMessages(string[] messages) {
		int totalMessageCount = messages.Length;
		int newMessageCount = totalMessageCount - oldMessageCount;
		if (newMessageCount > 0) {
			handleNewMessages(messages, newMessageCount);
		}
		oldMessageCount = totalMessageCount;
	}

	bool atMatchesMe(MessageObject message) {
		string at = message.At.ToLower().Trim();
		string username = GameData.UserName.ToLower().Trim();
		string sessionID = GameData.SessionID.ToLower().Trim();
		if (at == "") {
			return false;
		}
		if (username.StartsWith(at)) {
			return true;
		}
		if (sessionID.StartsWith(at)) {
			return true;
		}
		if (at == "everyone") {
			return true;
		}
		return false;
	}

	void handleNewMessages(string[] messages, int newMessageCount) {
		for (int i = messages.Length - newMessageCount; i < messages.Length; i++){
			MessageObject message = parseMessage(messages[i]);
			if (atMatchesMe(message) && message.Message != "") {
				displayMessage(message);
			}
		}
	}

	void displayMessage(MessageObject messageObj) {
		string[] messages = messageObj.Message.Split('\n');
		foreach (string message in messages) {
			if (message[0] == '%') {
				GUIManager.instance.directions(message.Substring(1), 0);
				GameManager.i.SendAction("Direction Displayed", message);
			} else if (message[0] == '[' && message[message.Length - 1] == ']') {
				string command = message.Substring(1, message.Length - 2);
				executeCommand(command);
				GameManager.i.SendAction("Command Executed", message);
			} else {
				GUIManager.instance.dialog(message);
				GameManager.i.SendAction("Message Displayed", message);
			}
		}
	}

	void executeCommand(string command) {

		int keywordIndex = command.IndexOf(" ");
		string keyword = command.Substring(0, keywordIndex).Trim().ToLower();
		string[] parameters = command.Substring(keywordIndex + 1).Split(';');
		//Debug.Log(command);

		for (int i = 0; i < parameters.Length; i++) {
			parameters[i] = parameters[i].Trim();
		}

		if (keyword == "sound") {
			string song = parameters[0].ToLower();
			if (PlayerTracker.instance != null && PlayerTracker.instance.audiosource != null) {
				foreach (AudioClip clip in sounds) {
					if (clip.name.ToLower().Contains(song)) {
						PlayerTracker.instance.audiosource.clip = clip;
						PlayerTracker.instance.audiosource.loop = true;
						PlayerTracker.instance.audiosource.Play();
						break;
					}
				}
			}
		} else if (keyword == "scene") {
			string sceneName = parameters[0];
			UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
		} else if (keyword == "message") {
			string message = parameters[0];
			GUIManager.instance.dialog(message);
		} else if (keyword == "direction") {
			string direction = parameters[0];
			GUIManager.instance.directions(direction, 0);
		} else if (keyword == "setVar") {
			string variable = parameters[0];
			if (variable == "cactusMode") {
				GameData.cactusMode = stringToBool(parameters[1]);
			}
		} else if (keyword == "setdict") {
			string variable = parameters[0];
			string key = parameters[1];
			string value = parameters[2];
			if (variable == "InteractionFlags") {
				if (GameData.InteractionFlags != null) {
					if (GameData.InteractionFlags.ContainsKey(key)) {
						GameData.InteractionFlags[key] = stringToBool(value);
					} else {
						GameData.InteractionFlags.Add(key, stringToBool(value));
					}
				}
			} else if (variable == "InteractionCounters") {
				int flagValue;
				bool success = int.TryParse(value, out flagValue);
				if (success) {
					if (GameData.InteractionCounters != null) {
						if (GameData.InteractionCounters.ContainsKey(key)) {
							GameData.InteractionCounters[key] = flagValue;
						} else {
							GameData.InteractionCounters.Add(key, flagValue);
						}
					}
				}
			}
		} else if (keyword == "show") {
			string variable = parameters[0];
			string title = "Show: " + variable;
			if (variable == "cactusMode") {
				SendResponse(title, GameData.cactusMode.ToString());
			} else if (variable == "InteractionFlags") {
				string output = "Key - Value";
				if (GameData.InteractionFlags != null) {
					foreach (KeyValuePair<string, bool> entry in GameData.InteractionFlags) {
						output += "\n" + entry.Key + " - " + entry.Value;
					}
				}
				SendResponse(title, output);
			} else if (variable == "InteractionCounters") {
				string output = "Key - Value";
				if (GameData.InteractionCounters != null) {
					foreach (KeyValuePair<string, int> entry in GameData.InteractionCounters) {
						output += "\n" + entry.Key + " - " + entry.Value;
					}
				}
				SendResponse(title, output);
			} else if (variable == "InteractableObjects") {
				string output = "Object Names:";
				if (GameData.InteractableObjects != null) {
					foreach (string name in GameData.InteractableObjects) {
						output += "\n" + name;
					}
				}
				SendResponse(title, output);
			}
		} else if (keyword == "interact") {
			string name = parameters[0];
			GameObject obj = GameObject.Find(name);
			if (obj != null) {
				InteractableController[] interacts = obj.GetComponents<InteractableController>();
				foreach (InteractableController interactable in interacts) {
					interactable.Interact(PlayerTracker.instance.gameObject);
				} 
			}
		} else if (keyword == "die") {
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		} else if (keyword == "quit") {
			Application.Quit();
		}
	}

	bool stringToBool(string str) {
		if (str.Trim().ToLower()[0] == 't') {
			return true;
		}
		return false;
	}

	MessageObject parseMessage(string message) {
		int firstNewline = message.IndexOf("\n");
		if (firstNewline < 0) {
			return new MessageObject("", "");
		}
		string firstLine = message.Substring(0, firstNewline);
		string at = "";
		if (firstLine[0] == '@') {
			at = firstLine.Substring(1);
			message = message.Substring(firstNewline + 1);
		}
		return new MessageObject(at, message);
	}

	public void SendResponse(string command, string response) {
		string scene = SceneManager.GetActiveScene().name;

		Dictionary<string, string> parameters = new Dictionary<string, string>{
			{ "446525886", GameData.UserName },
			{ "1285306095", GameData.SessionID },
			{ "1193729513", scene },
			{ "804872919", command },
			{ "27380576", response },
		};

		GameManager.instance.SendGoogleForm("1FAIpQLSe1HC_Gn4eTWTm9UxJTq5vqZkX8MK9yDwT36Pkm38NlFcFh9g", parameters);
	}

	void OnEnable() {
		Application.logMessageReceived += HandleLog;
	}

	void OnDisable() {
		Application.logMessageReceived -= HandleLog;
	}

	void HandleLog(string logString, string stackTrace, LogType type) {
		string title = type.ToString("F") + " : " + logString;
		GameManager.instance.SendAction(title, stackTrace);

		if (type == LogType.Error || type == LogType.Exception) {
			SendResponse("Error", title + "\n" + stackTrace);
		}
	}
}

class MessageObject {
	private string at;
	private string message;

	public MessageObject(string at, string message) {
		this.at = at.Trim();
		this.message = message.Trim();
	}

	public string Message {
		get {
			return message;
		}
	}

	public string At {
		get {
			return at;
		}
	}
}
