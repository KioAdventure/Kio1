﻿using UnityEngine;
using System.Collections;

public class TrapTrigger : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnTriggerStay2D(Collider2D other){
		print ("trigger");
	}
}
