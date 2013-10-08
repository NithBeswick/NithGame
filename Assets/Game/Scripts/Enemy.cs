using UnityEngine;
using System.Collections;

//*********************************************
//	Enemy this script is attached to the enemies
//	and handles their basic AI
//************************
public class Enemy : MonoBehaviour {
	public string displayname;
	public int health;
	public bool canShoot;
	public float moveSpeed = 0;
	public float turnSpeed = 0;
	
	private GameObject playerObject;
	
	// Use this for initialization
	void Start () {
		//get the player now, so we dont need to do this every frame
		playerObject = ((PlayerMovement)Component.FindObjectOfType(typeof(PlayerMovement))).gameObject;			
	}
	
	// Update is called once per frame
	void Update () {
		//wake up the rigidbody, so fireballs can collide with it		
		rigidbody.WakeUp();
		
		if(turnSpeed > 0) {
			transform.LookAt(playerObject.transform); //TODO use turnSpeed
		}
		
		if(moveSpeed > 0) {
			transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
		}
		
		if(canShoot) {
			//TODO by the studens
		}
	}
	
	//
	//	Damages the Enemy, and returns if it is destroyed
	//
	public bool Damage(int amount) {
		health -= amount;
		
		if(health <= 0) {
			//TODO make explosion! with LFs!
			Destroy(gameObject);
			return true;
		} else {
			return false;
		}
	}
}
