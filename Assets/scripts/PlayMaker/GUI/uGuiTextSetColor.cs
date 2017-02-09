using UnityEngine;
using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{

	[ActionCategory("uGui")]
public class uGuiTextSetColor : FsmStateAction
{
		[RequiredField]
		[CheckForComponent(typeof(UnityEngine.UI.Text))]
		[Tooltip("The GameObject with the text ui component.")]
		public FsmOwnerDefault gameObject;

		public FsmColor color;

		Text _text;

	// Code that runs on entering the state.
	public override void OnEnter()
	{
			GameObject _go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (_go!=null)
			{
				_text = _go.GetComponent<UnityEngine.UI.Text>();
			}
			if (_text!=null)
			{
				_text.color=color.Value;
			}
		Finish();
	}
		public override void Reset()
		{
			gameObject = null;
			color = null;
		}


}

}
