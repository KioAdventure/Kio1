using UnityEngine;
using System.Collections;

public class MenuPadIntegration : PadIntegration {
	int reseting=30;
	int defaultRest=10;
	void OnEnable(){
		reseting = 0;
	}
	protected override void Update ()
	{
		if (reseting == 0) {
			sendEvent ();
			reseting = defaultRest;
		} else {
			reseting--;
		}
	}
}
