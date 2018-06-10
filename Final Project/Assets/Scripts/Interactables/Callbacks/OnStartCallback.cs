using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnStartCallback : MonoBehaviour {


    public Callback callback;

	private void Start()
	{
        Callback.Call(callback);
	}
}
