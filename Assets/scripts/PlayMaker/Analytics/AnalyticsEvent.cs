using UnityEngine;
using UnityEngine.Analytics;
using System.Collections.Generic;
using System;


namespace HutongGames.PlayMaker.Actions
{

[ActionCategory("Analytics")]
public class AnalyticsEvent : FsmStateAction
{
		[RequiredField]
		[Tooltip("The event submit to remote service.")]
		public FsmString eventName;
		[CompoundArray("Parameters","Names","values")]
		public FsmString[] paraName;
		public FsmVar[] paraValue;

		public override void Reset ()
		{
			base.Reset ();
			eventName = null;
			paraName = new FsmString[1];
			
			paraValue = new FsmVar[1];
		}
	// Code that runs on entering the state.
	public override void OnEnter()
	{
			submitData ();
		Finish();
	}
		void submitData(){
			Dictionary<string,object> d = new Dictionary<string,object> ();
			
			for (int i = 0; i < paraName.Length; i++) {
				FsmVar fsv = paraValue [i];
				object v = fsv.GetValue();
//				if (v.GetType () == typeof(string)) {
//					d.Add (paraName [i].Value, fsv.stringValue);
//				}
//				if (v.GetType () == typeof(int)) {
//					d.Add (paraName [i].Value, fsv.intValue);
//				}
				d.Add (paraName [i].Value, fsv.NamedVar.RawValue);
//				d.Add (paraName [i].Value, v);
			}
			Analytics.CustomEvent (eventName.Value, d);
		}

}

}
