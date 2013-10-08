using UnityEngine;
using System.Collections;

//*********************************************
//	CloseInventoryButton turns off a gameObject
//	when the texture is clicked
//************************
public class CloseInventoryButton : MonoBehaviour {
	public GameObject inventory;
	private GUITexture texture;
	
	// Use this for initialization
	void Start () {
		texture = this.GetComponent<GUITexture>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonUp(0)) {
			if(texture.GetScreenRect().Contains(Input.mousePosition)) {
				inventory.SetActive(false);				
			}
		}
	}
}
