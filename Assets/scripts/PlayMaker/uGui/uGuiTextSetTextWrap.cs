using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{

[ActionCategory("uGui")]
public class uGuiTextSetTextWrap : FsmStateAction
{

		[RequiredField]
		[CheckForComponent(typeof(UnityEngine.UI.Text))]
		[Tooltip("The GameObject with the text ui component.")]
		public FsmOwnerDefault gameObject;

		[RequiredField]
		[UIHint(UIHint.TextArea)]
		[Tooltip("The text of the UGui Text component.")]
		public FsmString text;

//		[Tooltip("Reset when exiting this state.")]
//		public FsmBool resetOnExit;

		[Tooltip("Repeats every frame")]
		public bool everyFrame;

		private UnityEngine.UI.Text _text;
		string _originalString;
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
				_text.text += "\n\r";
				_text.text += text.Value;
			}

		Finish();
	}

		public override void Reset()
		{
			gameObject = null;
			text = null;
//			resetOnExit = null;
			everyFrame = false;
		}

}

}
