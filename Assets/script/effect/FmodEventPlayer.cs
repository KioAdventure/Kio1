using UnityEngine;
using System.Collections;
using FMODUnity;

public class FmodEventPlayer : MonoBehaviour {
	public StudioEventEmitter[] events;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void play(int index){
		int length = events.Length;
		if (index >= length)
			return;
		StudioEventEmitter se = events [index];
		if (se == null)
			return;
		se.Play ();
	}
}
