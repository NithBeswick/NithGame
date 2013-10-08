using UnityEngine;
using UnityEditor;
using System.Collections;

//*********************************************
//	DoEditorStuff doesnt do anything usefull
//	its just an example on how to make custom
//	Unity Editor Windows
//************************
public class DoEditorStuff : EditorWindow {
	private GameObject obj;

	[MenuItem ("NITH/Lens Flare Manager")]
	static void Init () {
		// Get existing open window or if none, make a new one:
		DoEditorStuff window = (DoEditorStuff)EditorWindow.GetWindow (typeof (DoEditorStuff));
	}
	
	void OnGUI () {
		GUILayout.Label ("Lens Flares! Yay", EditorStyles.boldLabel);
		obj = (GameObject)EditorGUILayout.ObjectField(obj, typeof(GameObject));
		
		if(GUILayout.Button("Do Stuff")) {
			GameObject p = GameObject.Find("Player");
			
			GameObject lf = (GameObject)Instantiate(obj);
			lf.name = "my new " + lf.name;
			lf.transform.parent = p.transform;
		}
	}

}
