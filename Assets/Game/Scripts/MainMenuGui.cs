using UnityEngine;
using System.Collections;

//******************************
//	MainMenuGUI handles buttons
//	displayed on the main menu
//******************************
[ExecuteInEditMode]
public class MainMenuGui : MonoBehaviour {
	public GUISkin skin;
	public Texture2D titleImage;
	public Rect titleRect;
	public Rect playRect;
	public Rect quitRect;
	
	// Update is called once per frame
	void OnGUI () {
		GUI.skin = skin;
		
		GUI.Label(titleRect, titleImage);
		
		if(GUI.Button(playRect, "Play!")) {
			Application.LoadLevel("test01");
		}
		if(GUI.Button(quitRect, "Quit")) {
			Application.Quit();
		}
	}
}
