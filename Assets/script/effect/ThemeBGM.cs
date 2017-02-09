using UnityEngine;
using System.Collections;
using FMOD;
using System;
using FMODUnity;
using FMOD.Studio;
public enum BGMEvent{
	play,
	stop,
	none
};
public class ThemeBGM : MonoBehaviour {
	[EventRef]
	public String Event;
	public BGMEvent action;
	static EventInstance instance;
	EventDescription discription;
	public bool AllowFadeout = true;
	public ParamRef[] Params;

	// Use this for initialization
	void Start () {
	
		switch (action) {
		case BGMEvent.play:
			{
				playBGM ();
				break;
			}
		case BGMEvent.stop:
			{
				stopBGM ();
				break;
			}
		default:
			{
				break;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void playBGM(){
		if (discription == null) {
			discription = RuntimeManager.GetEventDescription (Event);
		}
		bool is3D;
		discription.is3D(out is3D);
		bool isOneshot = false;
		if (!Event.StartsWith("snapshot", StringComparison.CurrentCultureIgnoreCase))
		{
			discription.isOneshot(out isOneshot);
		}
		if (instance == null)
		{
			discription.createInstance(out instance);

			// Only want to update if we need to set 3D attributes
			if (is3D)
			{
				var rigidBody = GetComponent<Rigidbody>();
				var transform = GetComponent<Transform>();

				instance.set3DAttributes(RuntimeUtils.To3DAttributes(gameObject, rigidBody));
				instance.setProperty(FMOD.Studio.EVENT_PROPERTY.MINIMUM_DISTANCE,200);
				instance.setProperty(FMOD.Studio.EVENT_PROPERTY.MAXIMUM_DISTANCE,210);
				RuntimeManager.AttachInstanceToGameObject(instance, transform, rigidBody);
			}
			foreach(var param in Params)
			{
				instance.setParameterValue(param.Name, param.Value);
			}
			instance.start ();
		}
	}
	public void stopBGM(){
		if (instance != null)
		{
			instance.stop(AllowFadeout ? FMOD.Studio.STOP_MODE.ALLOWFADEOUT : FMOD.Studio.STOP_MODE.IMMEDIATE);
			instance.release();
			instance = null;
		}
	}
	void OnApplicationQuit() {
		stopBGM ();
	}
}
