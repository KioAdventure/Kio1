﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ChangeSavingPath : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
//		ES2GlobalSettings.defaultPCDataPath;
		try{
		ES2.Init();
		string path=System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
		path+="\\Spacelight\\Kio1\\";
			ES2GlobalSettings.defaultPCDataPath = path;
		}catch(Exception e){
			
		}
//		Debug.Log(ES2GlobalSettings.defaultMacDataPath+" "+ES2GlobalSettings.defaultPCDataPath);

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
