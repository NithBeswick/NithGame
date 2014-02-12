using UnityEngine;
using System.Collections;

public class MobileMovement : MonoBehaviour {
	public enum InputMethods {Touch, Accel, Keyboard, Mouse};
	public InputMethods inputMethod = InputMethods.Keyboard;

	public float speed = 1;
	public int fingar;

	// Use this for initialization
	void Start () {
//#if EXECUTABLE

//#endif
	}
	
	// Update is called once per frame
	void Update () {
		if(inputMethod == InputMethods.Touch) {
			TouchInput();
		} else if(inputMethod == InputMethods.Accel) {
			AccelInput();
		} else if(inputMethod == InputMethods.Mouse) {
			MouseInput();
		} else {
			KeyInput();
		}
	}

	void TouchInput() {
		//Touch[] touches = Input.touches;
		if(Input.touchCount > fingar) {
			DoOneFingar(fingar);
		}
	}

	void DoOneFingar (int index)
	{
		Vector3 t = Input.GetTouch(index).position;
		t.z = Vector3.Distance(transform.position, Camera.main.transform.position);

		t = Camera.main.ScreenToWorldPoint(t);

		Debug.Log ("Touching at :  " + t);
		if (t.x > transform.position.x) {
			transform.Translate (Vector3.right * Time.deltaTime * speed);
		}
		else if (t.x < transform.position.x) {
			transform.Translate (Vector3.left * Time.deltaTime * speed);
		}
	}

	void MouseInput() {
		//Touch[] touches = Input.touches;
		if(Input.GetMouseButton(fingar)) {
			Vector3 t = Input.mousePosition;
			t.z = Vector3.Distance(transform.position, Camera.main.transform.position);

			Vector3 pos = Camera.main.ScreenToWorldPoint(t);

			Debug.Log("Mousing at :  " + pos);
			
			if(pos.x > transform.position.x) {
				
				transform.Translate(Vector3.right * Time.deltaTime * speed);
				
			} else if(pos.x < transform.position.x) {
				
				transform.Translate(Vector3.left * Time.deltaTime * speed);
				
			}
		}
	}

	void AccelInput() {
		Vector3 currentAccel = Input.acceleration;
		Debug.Log("Tilting by: " + currentAccel);
		transform.Translate(Vector3.right * Time.deltaTime * speed * currentAccel.x);
	}

	void KeyInput() {
		transform.Translate(Vector3.right * Input.GetAxis("Horizontal") * Time.deltaTime * speed);
	}
}
