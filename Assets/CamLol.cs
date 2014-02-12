using UnityEngine;
using System.Collections;

public class CamLol : MonoBehaviour {
	Material mat;
	string deviceName;
	WebCamTexture tex;

	// Use this for initialization
	void Start () {
		if(WebCamTexture.devices.Length == 0)
			return;

		mat = renderer.material;
		deviceName = WebCamTexture.devices[0].name;
		tex = new WebCamTexture(deviceName);
		tex.Play();
		mat.mainTexture = tex;
	}
	
	// Update is called once per frame
	void OnGUI () {
		if(GUILayout.Button("Fix Cam")) {
			Start ();
		}
	}
}
