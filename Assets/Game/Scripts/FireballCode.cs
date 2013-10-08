using UnityEngine;
using System.Collections;

//************************************
//	FireballCode handles the movement
//	and collision of fireballs
//************************************
public class FireballCode : MonoBehaviour {
	public ParticleSystem particles;
	public ParticleSystem smoke;
	public GameObject explosion;
	public float speed = 1;
	public float lifetime = 2;
	
	private float currentLife = 0;
	
	// Update is called once per frame
	void Update () {
		transform.Translate(Vector3.forward * speed * Time.deltaTime);
		
		if(currentLife < lifetime) {
			currentLife += Time.deltaTime;
		} else {
			SelfDestruct (false);
		}
	}

	public void SelfDestruct() {
		SelfDestruct(false, null);
	}
	
	public void SelfDestruct (bool collide)
	{
		SelfDestruct (false, null);
		
		if(collide)
			Debug.Log("Colliding with object, but no object spesified!!!");
	}
	
	public void SelfDestruct (bool collide, Collider hit)
	{
		//set particle emission to 0, and detach it
		particles.emissionRate = 0;		
		particles.transform.parent = null;
		
		smoke.emissionRate = 0;
		smoke.transform.parent = null;
		
		//if the fireball collides, spawn a collision explosion and damage the colided object
		if(collide) {
			if(explosion != null)
				Instantiate(explosion, transform.position, transform.rotation);
			
			//SendMessage sends a Method call to the object it collides with
			hit.gameObject.SendMessage("Damage", 5, SendMessageOptions.DontRequireReceiver);
		}
		
		Destroy(gameObject);
	}
	
	void OnCollisionEnter(Collision c) {
		SelfDestruct(true, c.collider);
	}
	
	void OnTriggerEnter(Collider c) {
		SelfDestruct(true, c);
	}
}
