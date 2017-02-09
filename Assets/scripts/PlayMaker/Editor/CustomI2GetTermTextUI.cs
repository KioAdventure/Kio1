using UnityEngine;
using System.Collections;
using UnityEditor;
using HutongGames.PlayMaker.Actions;
using HutongGames.PlayMakerEditor;
using I2.Loc;
using System.Collections.Generic;

[CustomActionEditor(typeof(I2GetTermText))]
public class CustomI2GetTermTextUI : CustomActionEditor {
	bool mAllowEditKeyName = false;
	string mNewKeyName = "";
	string[] mTermsArray = null;
	int GUI_SelectedTerm = 0;
	public override bool OnGUI ()
	{
		var action = target as I2GetTermText;
		EditField ("category");
		EditField ("term");
		string t = action.term.Value;
		if (OnGUI_SelectKey (ref t, false))
			action.term.Value = t;
		
		EditField ("text");
		return GUI.changed;
	}
	void UpdateTermsList( string currentTerm )
	{
		List<string> Terms = LocalizationManager.GetTermsList();

		// If there is a filter, remove all terms not matching that filter
		if (mAllowEditKeyName && !string.IsNullOrEmpty(mNewKeyName)) 
		{
			string Filter = mNewKeyName.ToUpper();
			for (int i=Terms.Count-1; i>=0; --i)
				if (!Terms[i].ToUpper().Contains(Filter) && Terms[i]!=currentTerm)
					Terms.RemoveAt(i);

		}

		if (!string.IsNullOrEmpty(currentTerm) && !Terms.Contains(currentTerm))
			Terms.Add (currentTerm);

		Terms.Sort(System.StringComparer.OrdinalIgnoreCase);
		Terms.Add ("<none>");
		mTermsArray = Terms.ToArray();
	}
	bool OnGUI_SelectKey( ref string Term, bool Inherited )  // Inherited==true means that the mTerm is empty and we are using the Label.text instead
	{
		GUILayout.Space (3);
		GUILayout.BeginHorizontal();

		bool bChanged = false;
		mAllowEditKeyName = GUILayout.Toggle(mAllowEditKeyName, "Term:", EditorStyles.foldout, GUILayout.ExpandWidth(false));
		if (bChanged && mAllowEditKeyName)
			mNewKeyName = Term;

		bChanged = false;

		if (mTermsArray==null || System.Array.IndexOf(mTermsArray, Term)<0)
			UpdateTermsList(Term);

		if (Inherited)
			GUI.contentColor = Color.yellow*0.8f;

		int Index = System.Array.IndexOf( mTermsArray, Term );

		GUI.changed = false;

		int newIndex = EditorGUILayout.Popup( Index, mTermsArray);

		GUI.contentColor = Color.white;
		if (/*newIndex != Index && newIndex>=0*/GUI.changed)
		{
			GUI.changed = false;
			mNewKeyName = Term = (newIndex==(mTermsArray.Length - 1)) ? string.Empty : mTermsArray[newIndex];
//			if (GUI_SelectedTerm==0)
//				mLocalize.SetTerm (mNewKeyName);
//			else
//				mLocalize.SetTerm (null, mNewKeyName);
			mAllowEditKeyName = false;
			bChanged = true;
		}
		LanguageSource source =  LocalizationManager.GetSourceContaining(Term);
		TermData termData = source.GetTermData(Term);
		if (termData!=null)
		{
			if (Inherited)
				bChanged = true; // if the term its inferred and a matching term its found, then use that
			eTermType NewType = (eTermType)EditorGUILayout.EnumPopup(termData.TermType, GUILayout.Width(90));
			if (termData.TermType != NewType)
				termData.TermType = NewType;
		}

		GUILayout.EndHorizontal();

		if (mAllowEditKeyName)
		{
			GUILayout.BeginHorizontal(GUILayout.Height (1));
			GUILayout.BeginHorizontal(EditorStyles.toolbar);
			if(mNewKeyName==null) mNewKeyName = string.Empty;

			GUI.changed = false;
			mNewKeyName = EditorGUILayout.TextField(mNewKeyName, new GUIStyle("ToolbarSeachTextField"), GUILayout.ExpandWidth(true));
			if (GUI.changed)
			{
				mTermsArray = null;	// regenerate this array to apply filtering
				GUI.changed = false;
			}

			if (GUILayout.Button (string.Empty, string.IsNullOrEmpty(mNewKeyName) ? "ToolbarSeachCancelButtonEmpty" : "ToolbarSeachCancelButton", GUILayout.ExpandWidth(false)))
			{
				mTermsArray = null;	// regenerate this array to apply filtering
				mNewKeyName = string.Empty;
			}

			GUILayout.EndHorizontal();

			string ValidatedName = mNewKeyName;
			LanguageSource.ValidateFullTerm( ref ValidatedName );

			bool CanUseNewName = (source.GetTermData(ValidatedName)==null);
			GUI.enabled = (!string.IsNullOrEmpty(mNewKeyName) && CanUseNewName);
			if (GUILayout.Button ("Create", EditorStyles.toolbarButton, GUILayout.ExpandWidth(false)))
			{
				mNewKeyName = ValidatedName;
				Term = mNewKeyName;
				mTermsArray=null;	// this recreates that terms array

				LanguageSource Source = null;
				#if UNITY_EDITOR
//				if (mLocalize.Source!=null)
//					Source = mLocalize.Source;
				#endif

				if (Source==null)
					Source = LocalizationManager.Sources[0];

				Source.AddTerm( mNewKeyName, eTermType.Text );
				mAllowEditKeyName = false;
				bChanged = true;
				GUIUtility.keyboardControl = 0;
			}
			GUI.enabled = (termData!=null && !string.IsNullOrEmpty(mNewKeyName) && CanUseNewName);
			if (GUILayout.Button (new GUIContent("Rename","Renames the term in the source and updates every object using it in the current scene"), EditorStyles.toolbarButton, GUILayout.ExpandWidth(false)))
			{
				mNewKeyName = ValidatedName;
				Term = mNewKeyName;
				mTermsArray=null;     // this recreates that terms array
				mAllowEditKeyName = false;
				bChanged = true;
				LocalizationEditor.TermReplacements = new Dictionary<string, string>();
				LocalizationEditor.TermReplacements[ termData.Term ] = mNewKeyName;
				termData.Term = mNewKeyName;
				source.UpdateDictionary(true);
				LocalizationEditor.ReplaceTermsInCurrentScene();
				GUIUtility.keyboardControl = 0;
				EditorApplication.update += LocalizationEditor.DoParseTermsInCurrentScene;
			}
			GUI.enabled = true;
			GUILayout.EndHorizontal();

			bChanged |= OnGUI_SelectKey_PreviewTerms ( ref Term);
		}

		GUILayout.Space (5);
		return bChanged;
	}
	bool OnGUI_SelectKey_PreviewTerms ( ref string Term)
	{
		if (mTermsArray==null)
			UpdateTermsList(Term);

		int nTerms = mTermsArray.Length;
		if (nTerms<=0)
			return false;

		if (nTerms==1 && mTermsArray[0]==Term)
			return false;

		bool bChanged = false;
		GUI.backgroundColor = Color.gray;
		GUILayout.BeginVertical ("AS TextArea");
		for (int i = 0, imax = Mathf.Min (nTerms, 3); i < imax; ++i) 
		{
			ParsedTerm parsedTerm;
			int nUses = -1;
			if (LocalizationEditor.mParsedTerms.TryGetValue (mTermsArray [i], out parsedTerm))
				nUses = parsedTerm.Usage;

			string FoundText = mTermsArray [i];
			if (nUses > 0)
				FoundText = string.Concat ("(", nUses, ") ", FoundText);

			if (GUILayout.Button (FoundText, EditorStyles.miniLabel)) 
			{
				mNewKeyName = Term = mTermsArray [i];
				GUIUtility.keyboardControl = 0;
				mAllowEditKeyName = false;
				bChanged = true;
			}
		}
		if (nTerms > 3)
			GUILayout.Label ("...");
		GUILayout.EndVertical ();
		GUI.backgroundColor = Color.white;

		return bChanged;
	}
}
