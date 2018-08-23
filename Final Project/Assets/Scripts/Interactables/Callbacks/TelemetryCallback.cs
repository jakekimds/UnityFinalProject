using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class TelemetryCallback : Callback {

	public string Title = "Title";
    public string Action = "Action";

	public override void CallStart ()
	{
		if (Title.Equals ("Title")) {
			Debug.LogError ("Error on "+gameObject.name);
		}
	}

    public override void OnCall()
    {
		if (Title.Equals ("Title")) {
			Debug.LogError ("Error on "+gameObject.name);
		}
        GameManager.i.SendAction(Title, Action);
    }


}
