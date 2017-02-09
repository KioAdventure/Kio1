using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{

[ActionCategory(ActionCategory.Logic)]
public class ActivateRender : FsmStateAction
{
		[RequiredField]
		public Renderer component;
		public FsmBool active;
	// Code that runs on entering the state.
		public override void Reset ()
		{
			base.Reset ();
			component=null;
			active=null;
		}
	public override void OnEnter()
		{
			(component as Renderer).enabled=active.Value;
		Finish();
	}


}

}
