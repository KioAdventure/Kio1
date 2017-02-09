using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using I2.Loc;

public class TestProperty : MonoBehaviour {
	public List<string> category;
	public List<string> terms;

	// Use this for initialization
	void Start () {
		category = LocalizationManager.GetCategories ();
		terms = LocalizationManager.GetTermsList ();
		LanguageSource source=LocalizationManager.Sources[0];
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
