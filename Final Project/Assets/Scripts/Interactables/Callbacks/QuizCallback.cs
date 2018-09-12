using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizCallback : Callback {

	public string Question;
	public string Answer;
	public bool LengthHint;
	public Callback IfCorrect;
	public Callback IfWrong;

	private float originalTimeScale;
	private bool showingQuiz;
	private string currentGuess;
	private float countdown = 0;

	public override void CallStart() {
		showingQuiz = false;
		originalTimeScale = Time.timeScale;
		currentGuess = "";

		Answer = Answer.ToUpper();
		GameData.InitializeCountersDict();
	}

	string getRawString() {
		string rawDisplay = "";
		if (LengthHint) {
			for (int i = 0; i < Answer.Length; i++) {
				if (i < currentGuess.Length) {
					rawDisplay += currentGuess[i];
				} else {
					rawDisplay += "_";
				}
				if (i < Answer.Length - 1) {
					rawDisplay += " ";
				}
			}
		} else {
			for (int i = 0; i < 18; i++) {
				if (i < currentGuess.Length) {
					rawDisplay += currentGuess[i];
				} else {
					rawDisplay += "_";
				}
			}
		}
		return rawDisplay;
	}
	

	public override void CallUpdate() {
		if (showingQuiz && countdown <= 0) {
			foreach (char chr in Input.inputString) {
				if (char.IsLetterOrDigit(chr) || chr == ' ') {
					int limit;
					if (LengthHint) {
						limit = Answer.Length;
					} else {
						limit = 18;
					}
					if (currentGuess.Length < limit) {
						currentGuess += char.ToUpper(chr);
					}
					GUIManager.instance.setQuizAnswer(getRawString());
				} else if (chr == '\n' || chr == '\r') {
					if (currentGuess == Answer) {
						Callback.Call(IfCorrect);
					} else {
						Callback.Call(IfWrong);
                        GameManager.i.SendAction("Quiz Answered", "Given: "+currentGuess + " / Correct: "+Answer);
					}

					Time.timeScale = originalTimeScale;
					GUIManager.instance.showQuiz(false);
					showingQuiz = false;
				} else if (chr == '\b') {
					if (currentGuess.Length > 0) {
						currentGuess = currentGuess.Substring(0, currentGuess.Length-1);
					}
					GUIManager.instance.setQuizAnswer(getRawString());
				}
			}
		}
		countdown -= Time.unscaledDeltaTime;
	}

	public override void OnCall() {
		int count = 0;
		string flagName = "QuizFreebies";
		if (GameData.InteractionCounters.ContainsKey(flagName)) {
			count = GameData.InteractionCounters[flagName];
			GameData.InteractionCounters[flagName]--;
		} else {
			GameData.InteractionCounters.Add(flagName, 0);
		}
		if (!(count > 0)) {
			currentGuess = "";
			originalTimeScale = Time.timeScale;
			GUIManager.instance.setQuizQuestion(Question);
			GUIManager.instance.setQuizAnswer(getRawString());
			GUIManager.instance.showQuiz(true);
			showingQuiz = true;
			countdown = .2f;
			Time.timeScale = 0;
		}
	}
}
