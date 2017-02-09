using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SetUICamera : MonoBehaviour {
	Canvas canvas;
	public int sortingOrder = 0;
	// Use this for initialization
	void Start () {
		canvas=GetComponent<Canvas>();
		if(canvas!=null){
			canvas.worldCamera=Camera.main;
			canvas.sortingOrder = sortingOrder;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
