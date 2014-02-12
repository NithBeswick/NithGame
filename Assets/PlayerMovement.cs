using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// does everything related to player movement
// class by Thomas

//*************************
//	This class controls
//	player movement, and
//	anything related.
//*************************

public class PlayerMovement : MonoBehaviour {
	public float speed = 3;
	public float turnSpeed;
	public GameObject inventory;
	
	private Vector3 defaultPosition;
	
	// Use this for initialization
	void Start () {	
			//store the original position, for when we reset the save file
		defaultPosition = transform.position;
		
			//Use the default position from Unity Editor
		float x = transform.position.x;		
		float z = transform.position.z;
		
			//Load player position from 'PlayerPrefs' (no longer in use)
		//x = SaveLoadManager.instance.LoadPrefFloat("PlayerPos_X", x);
		//z = SaveLoadManager.instance.LoadPrefFloat("PlayerPos_Z", z);
		//transform.position = new Vector3(x, 1, z);
		
			//Load the SaveFile, if it succeeds set the position from the file
		if(SaveLoadManager.instance.LoadRealSavefile()) {
			Vector3 loadedPos = SaveLoadManager.instance.saveFile.playerPosition;
			loadedPos.y = transform.position.y;
			
			transform.position = loadedPos;
		}
	}
	
	// Update is called once per frame
	void Update () {
		Movement ();
		
		//Inventory
		if(Input.GetAxis("Inventory") > 0) {
			inventory.SetActive( !inventory.activeSelf );
			Debug.Log("PlayerMovement: Player opens inventory");
		}
		
		//save character position
		SavePos();
		
		
		// DEBUG!! TODO: REMOVE THIS
		if(Input.GetKeyUp(KeyCode.Delete)) {
			transform.position = defaultPosition;
			PlayerPrefs.DeleteAll();
		}
	}

	void Movement ()
	{
			//Report input to the log
		//Debug.Log("V: " + Input.GetAxis("Vertical"));
		//Debug.Log("H: " + Input.GetAxis("Horizontal"));
		
		if(Input.GetAxis("Horizontal") < 0) {
				//Rotate is no longer used on this Axis, but this is the code to do so
			//transform.Rotate(Vector3.up * turnSpeed * Time.deltaTime);
			
			transform.Translate(Vector3.left * speed * Time.deltaTime);
			
		} else if (Input.GetAxis("Horizontal") > 0) {
				//Rotate is no longer used on this Axis, but this is the code to do so
			//transform.Rotate(Vector3.down * turnSpeed * Time.deltaTime);
			
			transform.Translate(Vector3.right * speed * Time.deltaTime);
		}
		
		if(Input.GetAxis("Vertical") < 0) {
			transform.Translate(Vector3.back * speed * Time.deltaTime);			
		} else if (Input.GetAxis("Vertical") > 0) {
			transform.Translate(Vector3.forward * speed * Time.deltaTime);
		}
		
		
			//Rotation
			//We cast a ray from the mouseposition on screen,
			//outwards to see where in the world we are pointing
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if(Physics.Raycast(ray, out hit, 10000, ~LayerMask.NameToLayer("Player"))) {
			if(hit.collider != collider) {
				Vector3 hitPos = hit.point;
				hitPos.y = transform.position.y;
				transform.LookAt(hitPos); 
				Debug.DrawRay(ray.origin, ray.direction * 100, Color.green);
			}
		} else {
			Debug.DrawRay(ray.origin, ray.direction * 100, Color.red);
		}
		
		Debug.DrawRay(transform.position, transform.forward * 100, Color.yellow);
		
	}
	
	void SavePos() {
		//Easy mode, but we no longer use 'PlayerPrefs' due to its limited storage
		//SaveLoadManager.instance.SavePref("PlayerPos_X", transform.position.x);
		//SaveLoadManager.instance.SavePref("PlayerPos_Z", transform.position.z);
				
		//Better mode
		SaveLoadManager.instance.saveFile.playerPosition = transform.position;
		SaveLoadManager.instance.SaveRealSavefile();		
	}
	
	private Ray r;
	private RaycastHit h;
	public void MyMove(Vector3 dir) {
		if(dir.y < 0) {
			Ray ray = new Ray(transform.position, transform.up * -1);			
			if(Physics.Raycast(ray, out h, -dir.y)) {
				Vector3 pos = transform.position;
				pos.x += dir.x;
				pos.y = h.point.y;
				pos.z += dir.z;
				transform.position = pos;
				return;
			}
		}
		
		transform.Translate(dir);
	}
}