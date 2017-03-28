using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
		#if UNITY_STANDALONE_WIN || UNITY_EDITOR
		Win32Help.SetImeEnable(false);
		print("no input");
		#endif
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
