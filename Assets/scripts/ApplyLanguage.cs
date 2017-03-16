using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using I2.Loc;
public class ApplyLanguage : MonoBehaviour {
	public string defaultLanguage = "English";
	public bool autoChange=true;
	// Use this for initialization
	void Start () {
		if( LocalizationManager.HasLanguage(defaultLanguage))
		{
			LocalizationManager.CurrentLanguage = defaultLanguage;
		}
		string l = Application.systemLanguage.ToString();
		print (l);
		if(!autoChange)
		return;
		if( LocalizationManager.HasLanguage(l))
		{
			LocalizationManager.CurrentLanguage = l;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
