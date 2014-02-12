using UnityEngine;
using System.Collections;

public class MobileGUI : MyGui {
	public GuiElement playerHealth;
	public GuiElement opponentHealth;

	public override void Draw ()
	{
		//float relativeX = (Screen.width / 100 * guiPos.x) - (guiPos.width);
		//Rect realPos = new Rect(relativeX, guiPos.y, guiPos.width, guiPos.height);
		GUILayout.BeginArea(playerHealth.position.toRect(), (GUIStyle)"Box");
		GUILayout.Label(playerHealth.text);
		GUILayout.EndArea();

		GUILayout.BeginArea(opponentHealth.position.toRect(), (GUIStyle)"Box");
		GUILayout.Label(opponentHealth.text);
		GUILayout.EndArea();
	}
}

[System.Serializable]
public class GuiElement {
	public string text;
	public AnchoredRect position;

}
