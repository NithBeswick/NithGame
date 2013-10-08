using UnityEngine;
using System.Collections;

//************************
//	SpaceGui covers gui
//	onscreen, and related
//	methods / actions
//************************

//ExecuteInEditMode allows the script
//to run while we are in edit mode of
//unity, usefull for seeing GUI while
//we are working.
[ExecuteInEditMode]
public class SpaceGui : MonoBehaviour {
	public GUISkin Skin;
	public Rect GuiPosition;
	public Rect GuiPosition2;
	public GameObject inventory;
	
	private bool inv;
	
	// Use this for initialization
	void Awake () {
		//we only want to load player preferences if the game is running (we have ExecuteInEditMode)
		if(Application.isPlaying) {
			float x = SaveLoadManager.instance.LoadPrefFloat("GUIpos_X", GuiPosition.x);
			float y = SaveLoadManager.instance.LoadPrefFloat("GUIpos_Y", GuiPosition.y);
			
			GuiPosition = new Rect(x, y, GuiPosition.width, GuiPosition.height);
		}
	}
	
	// Update is called once per frame
	void OnGUI () {
		GUI.skin = Skin;
		
		//GuiPosition is affected by window
		//windows parameters are ID, position, method, titleText
		//position is "set twice" because the window may change it,
		//but it also uses it
		GuiPosition = GUI.Window(0, GuiPosition, HealthBox, "Health");
		
		if(inv)
			GuiPosition2 = GUI.Window(1, GuiPosition2, Inven, "Inventory");
	}
	
	void Update() {
		//again, only save if the game is playing, not in edit mode
		if(Application.isPlaying) {
			SaveLoadManager.instance.SavePref("GUIpos_X", GuiPosition.x);
			SaveLoadManager.instance.SavePref("GUIpos_Y", GuiPosition.y);
		}
	}
	
	//A check to see if the mouse is over the gui, usefull so the player
	//doesnt shoot or interact while he is clicking GUI buttons
	public bool IsOverGui(Vector2 mousePos) {
		return GuiPosition.Contains(mousePos) || GuiPosition2.Contains(mousePos);
	}
	
	//Healthbox window, could possibly also display Ammo and/or score
	void HealthBox (int id)
	{
		//space is only nessesary if the gui graphics demand it,
		//in this case there is a titlebar area that we need to skip
		GUILayout.Space(30);
		
		GUILayout.BeginHorizontal();
		
		GUILayout.BeginVertical();
		GUILayout.Label("Hello World");
		GUILayout.Label("This is my GUI", (GUIStyle)"ShortLabel");
		GUILayout.EndVertical();
		
		if(GUILayout.Button("I", GUILayout.Width(50), GUILayout.Height(50))) {
			inv = !inv;
		}
		
		GUILayout.EndHorizontal();
		
		//DragWindow allows us to move the window around the screen
		//the Rect parameter sets the limit, useful if the window can
		//only be moved around one part of the screen
		GUI.DragWindow(new Rect(0, 0, 10000, 10000));
	}
	
	void Inven (int id)
	{
		GUILayout.Space(30);
		
		int numberOfButtons = 0;
		for(int i = 0; i < 25; i++) {			
			if(numberOfButtons == 0)
				GUILayout.BeginHorizontal();
			
			if(GUILayout.Button("B"+i, GUILayout.Width(50), GUILayout.Height(50))) {
				Debug.Log("Clicked button: " + i);
			}
			
			numberOfButtons++;
			
			//if we have reached 5 buttons, begin a new line
			//the 5 here should probably be a variable, so we
			//can easier alter the number of rows / columns
			if(numberOfButtons == 5) {
				GUILayout.EndHorizontal();
				numberOfButtons = 0;
			}
		}
		if(numberOfButtons != 0)
			GUILayout.EndHorizontal();
		
		GUI.DragWindow(new Rect(0, 0, 10000, 10000));
	}
}
