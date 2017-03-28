using UnityEngine;
using Steamworks;
namespace HutongGames.PlayMaker.Actions
{

[ActionCategory("Steam")]
public class SubmitAchievement : FsmStateAction
{
		public AchieviementID achievementID;
	// Code that runs on entering the state.
		public override void Reset ()
		{
			achievementID = AchieviementID.NONE;
		}
	public override void OnEnter()
	{
			if (achievementID != AchieviementID.NONE) {
				Debug.Log (achievementID.ToString ());
				if (SteamManager.Initialized) {
					SteamUserStats.SetAchievement (achievementID.ToString ());
					SteamUserStats.StoreStats ();
				}
			}
		Finish();
	}


}

}
