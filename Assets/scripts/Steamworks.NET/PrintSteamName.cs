using System.Collections;
using System.Collections.Generic;
using Steamworks;
using UnityEngine;

public class PrintSteamName : MonoBehaviour {

	// Use this for initialization
	void Start () {
		if(SteamManager.Initialized) {
			string name="";

			CSteamID id = SteamUser.GetSteamID ();

			Debug.Log(id.m_SteamID);

		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
