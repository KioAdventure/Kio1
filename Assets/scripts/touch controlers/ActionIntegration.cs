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
public class ActionIntegration : TouchIntegration {
	string spaceEvent="hand gun";
	string REvent="go reload";
	protected override void Start ()
	{
		base.Start ();
		JEvent = "fire";
		KEvent = "hide gun";
	}
	protected override void sendEvent ()
	{
		base.sendEvent ();
		foreach (PlayMakerFSM fsm in controlers) {
			string stateName = fsm.ActiveStateName;

			if (stateName.IndexOf ("control listener") >= 0) {
				//				print (x + " " + y);
				if (CnInputManager.GetButtonDown ("Jump")) {
					fsm.SendEvent (spaceEvent);
					print ("space");
				}
				if (CnInputManager.GetButtonDown ("Fire2")) {
					fsm.SendEvent (REvent);
					print ("r");
				}
			}
		}
	}
}
