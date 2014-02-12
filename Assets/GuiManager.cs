using UnityEngine;
using System.Collections;

public class GuiManager : MonoBehaviour {
	MyGui[] guis;
	
	void Start () {
		guis = Component.FindObjectsOfType<MyGui>();
	}
	
	void OnGUI () {
		foreach(MyGui script in guis){
			script.Draw();
		}
	}
}
