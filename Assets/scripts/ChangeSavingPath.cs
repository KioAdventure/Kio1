using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ChangeSavingPath : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
//		ES2GlobalSettings.defaultPCDataPath;
		string path=System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
		path+="\\Spacelight\\Kio1\\";
		ES2GlobalSettings.defaultPCDataPath = path;
		Debug.Log(path+" "+ES2GlobalSettings.defaultPCDataPath);

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
