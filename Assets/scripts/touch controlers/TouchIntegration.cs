using UnityEngine;
using System.Collections;
using CnControls;

/*
 *  j=Fire3
 *  k=Cancel
 *  space=Jump
 *  r=Fire2
 *  i=另计
 * */
public class TouchIntegration : PadIntegration {
	public string JEvent="check pass";
	public string KEvent="check fail";

	protected override void sendEvent ()
	{
		foreach (PlayMakerFSM fsm in controlers) {
			string stateName = fsm.ActiveStateName;

			if (stateName.IndexOf ("control listener") >= 0) {
				if (CnInputManager.GetButtonUp ("Cancel")) {
					fsm.SendEvent (KEvent);
					print ("k");
				}
				//				print (x + " " + y);
				bool b=CnInputManager.GetButtonDown ("Fire3");
				if (b) {
					fsm.SendEvent (JEvent);
					print ("j");
				}

			}
		}

	}
}
