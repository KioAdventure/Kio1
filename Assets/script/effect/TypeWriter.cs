﻿using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;
using HutongGames.PlayMaker.Actions;
using UnityEngine.UI;
using FMODUnity;
using System;
	
public class TypeWriter : MonoBehaviour
	{
	string storeText;
	MatchCollection mc;
	string[] typingText;
	Text label;
	public PlayMakerFSM fsm;
	float defaultDelay=0.04f;
	StudioEventEmitter fmod;
	void Awake(){
		label=GetComponent<Text>();
		fmod=fsm.GetComponent<StudioEventEmitter>();
	}
	void Start(){
		
	}
	public void typeText(string text,float delay)
		{
		if(label==null){
			sendFinishEvent();
			return;
		}
		storeText = text;
		label.text="";
		if(delay>0)
			defaultDelay=delay;
		
		string t=text;
//		t="asdb\\nasdg\\1.5hjkh\\nhjkhg";
		Regex reg=new Regex(@"\\n|\\\d{1,5}\.\d{0,2}|\\\d{1,5}");
		mc=reg.Matches(t);
		typingText=reg.Split(t);
		StopAllCoroutines();
		StartCoroutine(typing());
		}
	public void stopType(){
		StopAllCoroutines ();
		Regex reg=new Regex(@"\\\d{1,5}\.\d{0,2}|\\\d{1,5}");

		storeText = reg.Replace (storeText, "");
		reg = new Regex (@"\\n");
		storeText = reg.Replace (storeText, "\r\n");
		label.text = storeText;
		sendFinishEvent ();
	}
	IEnumerator typing(){
		int i=0;
		foreach(string text in typingText){
			char[] chars=text.ToCharArray();
			if(chars.Length<=0)
				continue;
			foreach(char c in chars){
				label.text+=c;

//				fsm.Fsm.Event("typing");
				fmod.Play();
				yield return new WaitForSeconds(defaultDelay);
			}
			if(i>=mc.Count)
				continue;
			string r=mc[i].Value;
			if(r=="\\n"){
				label.text+="\r\n";
//				yield return new WaitForSeconds(defaultDelay);
			}else{
				r=r.Replace("\\","");
				float ti=float.Parse(r);
				if(ti>0){
					yield return new WaitForSeconds(ti);
				}
			}
			i++;
		}
		yield return null;
		sendFinishEvent();
	}
	void sendFinishEvent(){
//		if(fsms.Length>0){
//			foreach(PlayMakerFSM fsm in fsms){
				fsm.Fsm.Event("type done");
//			}
//		}
	}
	}


