using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{

[ActionCategory("Physics2D")]
public class Rigibody2DFreeze : FsmStateAction
{
		[RequiredField]
		[CheckForComponent(typeof(UnityEngine.Rigidbody2D))]
		[Tooltip("The GameObject with the text ui component.")]
		public FsmOwnerDefault gameObject;
		public FsmBool x;
		public FsmBool y;
		public FsmBool z;
		Rigidbody2D rigibody2d;
		public override void Reset ()
		{
			base.Reset ();
			gameObject = null;
			x = null;
			y = null;
			z = null;
		}
	// Code that runs on entering the state.
	public override void OnEnter()
	{
			GameObject _go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (_go != null) {
				rigibody2d = _go.GetComponent<Rigidbody2D> ();
			}
			if (rigibody2d != null) {
				RigidbodyConstraints2D freeze = RigidbodyConstraints2D.None;
				if (x.Value)
					freeze = RigidbodyConstraints2D.FreezePositionX;
				if (y.Value)
					freeze = RigidbodyConstraints2D.FreezePositionY;
				if (z.Value)
					freeze = RigidbodyConstraints2D.FreezeRotation;
				if (x.Value && y.Value)
					freeze = RigidbodyConstraints2D.FreezePosition;
				if (x.Value && y.Value&&z.Value)
					freeze = RigidbodyConstraints2D.FreezeAll;
				rigibody2d.constraints = freeze;
				
			}
		Finish();
	}


}

}
