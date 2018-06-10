using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Logo2Chomp : MonoBehaviour {

	public AudioSource chmp;

	public void Chomp() {
		chmp.Play();
	}
}
