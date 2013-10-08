using UnityEngine;
using System.Collections;

//*********************************************
//	ParticleDestroyer destroys the particle's
//	gameObject when it is complete, we do this
//	our selves because Unity is stupid.
//************************

//RequireComponent makes sure that this object has the spesified component
[RequireComponent(typeof(ParticleSystem))]
public class ParticleDestroyer : MonoBehaviour {
	private ParticleSystem ps;
	private float lifetime = 0;
	
	// Use this for initialization
	void Start () {
		//get the particle component now, so we dont need to to it every frame
		ps = this.GetComponent<ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update () {	
		//check if emission is off, or the rate is 0
		if(ps.emissionRate <= 0 || ps.enableEmission == false) {
			lifetime += Time.deltaTime;
			
			//if our timer is higher than the particle's lifetime
			if(lifetime >= ps.startLifetime) {
				Destroy(gameObject);
			}			
		} else {
			lifetime = 0;
		}
	}
}
