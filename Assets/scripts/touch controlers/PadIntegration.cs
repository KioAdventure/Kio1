using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using CnControls;
using PlayMaker;

public class PadIntegration : MonoBehaviour {
	string horizontal="Horizontal";
	string vertical="Vertical";
	public string controlFSMName="FSM";
	public PlayMakerFSM[] controlers = null;
	public bool includeChild = false;

	// Use this for initialization
	protected virtual void Start () {
	
		findControler ();
	}
	
	// Update is called once per frame
	protected virtual void Update () {
		sendEvent ();

	}
	protected virtual void sendEvent(){
		float x = CnInputManager.GetAxis (horizontal);
		float y = CnInputManager.GetAxis (vertical);
		foreach (PlayMakerFSM fsm in controlers) {
			string stateName = fsm.ActiveStateName;

			if (stateName.IndexOf ("control listener") >= 0) {
//				print (x + " " + y);
				float abx = Mathf.Abs (x);
				float aby = Mathf.Abs (y);
				float maxv = Mathf.Max (abx, aby);
				if (maxv==abx&&x < 0) {
					fsm.SendEvent ("left");
				} else if (maxv==abx&&x > 0) {
					fsm.SendEvent ("right");
				} else if (maxv==aby&&y < 0) {
					fsm.SendEvent ("down");
				} else if (maxv==aby&&y > 0) {
					fsm.SendEvent ("up");
				} else {
					fsm.SendEvent ("stand");
				}
			}
		}
	}
	void findControler(){
		if (controlers == null||controlers.Length==0) {
			List<PlayMakerFSM> fsl = new List<PlayMakerFSM> ();
			PlayMakerFSM[] fsms;
			if (includeChild) {
				fsms = GetComponentsInChildren<PlayMakerFSM> ();
			} else {
				fsms = GetComponents<PlayMakerFSM> ();
			}
			foreach (PlayMakerFSM fsm in fsms) {
				if (fsm.FsmName == controlFSMName) {
					fsl.Add (fsm);
				}
			}
			controlers = fsl.ToArray ();
		}
	}
}
