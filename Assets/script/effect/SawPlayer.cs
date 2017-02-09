using UnityEngine;
using System.Collections;
using FMODUnity;

public class SawPlayer : MonoBehaviour {
	public StudioEventEmitter outSound;
	public StudioEventEmitter inSound;
	public StudioEventEmitter ingSound;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void playOut(){
		ingSound.Stop ();
		outSound.Play ();

	}
	public void playIn(){
		ingSound.Stop ();
		inSound.Play ();
	}
	public void playIng(){
		ingSound.Play ();
	}
}
