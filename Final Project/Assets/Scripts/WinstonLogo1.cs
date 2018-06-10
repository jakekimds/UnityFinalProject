using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinstonLogo1 : MonoBehaviour {

	public Animator jmp;
	public AudioSource squk;
	
	public void jump() {
		jmp.SetTrigger("Jump");
		squk.Play();
	}
	public void nextScene() {
		GUIManager.instance.fadeToScene ("Logo2", 1f);
	}
}
