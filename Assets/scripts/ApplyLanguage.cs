using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using I2.Loc;

public class ApplyLanguage : MonoBehaviour {
	SetLanguage sl;
	// Use this for initialization
	void Start () {
		sl = GetComponent<SetLanguage> ();
		sl.ApplyLanguage ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
