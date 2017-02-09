using UnityEngine;
using I2.Loc;
using System.Collections.Generic;


namespace HutongGames.PlayMaker.Actions
{

[ActionCategory("I2 Localization")]
public class I2GetTermText : FsmStateAction
{
		
		public FsmString category;

		[RequiredField]
		public FsmString term;
		[UIHint(UIHint.Variable)]
		[RequiredField]
		public FsmString text;
	// Code that runs on entering the state.

		public override void Reset ()
		{
			base.Reset ();
			category = null;
			term = null;
			text = null;
		}
	public override void OnEnter()
	{
			string combineTerm = "";
			if (category.Value.Length > 0) {
				combineTerm = category.Value + "/";
			}
			combineTerm += term.Value;
			text.Value = ScriptLocalization.Get (combineTerm);
		Finish();
	}


}

}
