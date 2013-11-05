using UnityEngine;
using System.Collections;

public class ScoreDisplay : MonoBehaviour {
	private TextMesh textMesh;
	private int currentScore = 0;
	private Vector3 defaultSize;
	
	public float time;
	public bool up;
	public bool down;

	// Use this for initialization
	void Start () {
		defaultSize = transform.localScale;
		textMesh = GetComponent<TextMesh>();
		textMesh.text = currentScore + "";
	}
	void Update() {
		if(up) {
			up = false;
			currentScore++;
			ChangeDisplay("" + currentScore);
		}
		if(down) {
			down = false;
			currentScore--;
			ChangeDisplay("" + currentScore);
		}
	}
	
	void ChangeDisplay(string text) {
		textMesh.text = text;
		
		Hashtable hash = new Hashtable();
		hash["amount"] = defaultSize * ((currentScore + 1) / 2);
		hash["time"] = time;
		
		iTween.PunchScale(gameObject,hash);
		
		hash["amount"] = Vector3.forward * ((currentScore + 1) * 3);
		hash["time"] = time * 2;
		iTween.PunchRotation(gameObject, hash);		
	}
	
	void OnGUI() {
		if(GUILayout.Button("Win!")) {
			up = false;
			currentScore++;
			ChangeDisplay("" + currentScore);
		}
		if(GUILayout.Button("Fail!")) {
			down = false;
			currentScore--;
			ChangeDisplay("" + currentScore);
		}
	}
}
