using UnityEngine;
using System.Collections;

public class FlareAnim : MonoBehaviour {
	private Light lightSource;
	public float peak = 1;
	public float flareSpeed = 1;
	
	private bool hasPeaked = false;
	
	// Use this for initialization
	void Start () {
		lightSource = light;
		lightSource.intensity = 0;
		hasPeaked = false;
	}
	
	// Update is called once per frame
	void Update () {
		lightSource.intensity = peak * Mathf.Sin(flareSpeed * Time.time);
		
		if(hasPeaked) {
			if(lightSource.intensity <= 0)
				Destroy(gameObject);
		} else {
			if(lightSource.intensity >= peak/2)
				hasPeaked = true;
		}
	}
}
