using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Steamworks;

public class RestAllAchievement : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void reset(){
		if (SteamManager.Initialized) {
			SteamUserStats.ResetAllStats (true);
		}
	}
}
