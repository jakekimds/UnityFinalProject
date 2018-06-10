using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class URLOpenerCallback : Callback {
	public string URL;
	public override void OnCall() {
		Process process = new Process();
		process.StartInfo.FileName = @"C:\Program Files (x86)\Google\Chrome\Application\chrome.exe";
		process.StartInfo.Arguments = URL;
		process.Start();
	}
}
