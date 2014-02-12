using UnityEngine;
using System.Collections;

public class ColorMe : MonoBehaviour {
	public GameObject arm;
	public bool doArm;
	public GameObject[] objectToColor;
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyUp(KeyCode.C)) {
			if(doArm)
				arm.renderer.material.color = Color.green;
			
			foreach(GameObject go in objectToColor)
				go.renderer.material.color = Color.red;
		}
	}
}
