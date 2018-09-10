using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

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
			yield return new WaitForSeconds(3);
		}
	}


	void parseDoc(string gDocHTML) {
		string startIdentifier = "START\\n(60c1cc7f-72a2-4b45-96cb-94f17aab1cfd)\\n//\\n//\\n";
		string endIdentifier = "\\n//\\n//\\n(e102f448-ec45-4f94-9fbf-e6da9b09a251)\\nEND";

		int startIndex = gDocHTML.IndexOf(startIdentifier) + startIdentifier.Length;
		int endIndex = gDocHTML.IndexOf(endIdentifier);
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
		for (int i = messages.Length - 1; i >= messages.Length - newMessageCount; i--){
			MessageObject message = parseMessage(messages[i]);
			if (atMatchesMe(message) && message.Message != "") {
				displayMessage(message);
				break;
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
		string keyword = "sound";
		if (command.ToLower().Substring(0, keyword.Length) == keyword) {
			string parameter = command.Substring(keyword.Length + 1).ToLower();
			if (PlayerTracker.instance != null && PlayerTracker.instance.audiosource != null) {
				foreach (AudioClip clip in sounds) {
					if (clip.name.ToLower().EndsWith(parameter) || clip.name.ToLower().StartsWith(parameter)) {
						PlayerTracker.instance.audiosource.clip = clip;
						PlayerTracker.instance.audiosource.loop = true;
						PlayerTracker.instance.audiosource.Play();
						break;
					}
				}
			}
		}
		command = "scene";
		if (command.ToLower().Substring(0, keyword.Length) == keyword) {
			string parameter = command.Substring(keyword.Length + 1);
			UnityEngine.SceneManagement.SceneManager.LoadScene(parameter);
		}
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
