using UnityEngine;
using UnityEditor;
using HutongGames.PlayMaker.Actions;
using HutongGames.PlayMakerEditor;

[CustomActionEditor(typeof(DebugFloat))]
public class CustomActionEditorTest : CustomActionEditor
{
	public override void OnEnable()
	{
		// Do any expensive initialization stuff here.
		// This is called when the editor is created.
	}

	public override bool OnGUI()
	{
		// If you need to reference the action directly:
		var action = target as DebugFloat;

		// You can draw the full default inspector.

		GUILayout.Label("Default Inspector:", EditorStyles.boldLabel);

		var isDirty = DrawDefaultInspector();

		// Or draw individual controls

		GUILayout.Label("Field Controls:", EditorStyles.boldLabel);

		EditField("logLevel");
		EditField("floatVariable");

		// Or add your own controls using any GUILayout method

		GUILayout.Label("Your Controls:", EditorStyles.boldLabel);

		if (GUILayout.Button("Press Me"))
		{
			EditorUtility.DisplayDialog("My Dialog", "Hello", "OK");
			isDirty = true; // e.g., if you change action data
		}

		// OnGUI should return true if you change action data!

		return isDirty || GUI.changed;
	}
}

[ObjectPropertyDrawer(typeof(AudioClip))]
public class ObjectPropertyDrawerTest : ObjectPropertyDrawer 
{
	public override Object OnGUI(GUIContent label, Object obj, bool isSceneObject, params object[] attributes)
	{
		GUILayout.BeginVertical();

		obj = EditorGUILayout.ObjectField(label, obj, typeof(AudioClip), isSceneObject);

		GUILayout.Label("This is a custom object property drawer!");

		GUILayout.EndVertical();

		return obj;
	}
}