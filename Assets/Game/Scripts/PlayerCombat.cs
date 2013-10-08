using UnityEngine;
using System.Collections;

//*********************************************
//	PlayerCombat handles combat controlls and
//	related methods.
//************************
public class PlayerCombat : MonoBehaviour {
	public SpaceGui gui;
	public GameObject Lazer;
	public GameObject muzzle;
	public GameObject fireball;
	public GameObject muzzleFlash;
	public float clickDelay = 0.5f;
	
	public float meleeRange = 1.0f;
	
	private float lastClick = 0;
	
	// Update is called once per frame
	void Update () {
		Shoot();
		Melee();
		
		//if the Lazer gameobject exists
		if(Lazer != null) {		
			//toggle the lazer with Q
			if(Input.GetKeyUp(KeyCode.Q)) {
				Lazer.SetActive(!Lazer.activeSelf);
			}
			
			//if the lazer is on
			if(Lazer.activeSelf) {
				float distance = 10000;
				
				//draw a ray to find out when it collides
				Ray ray = new Ray(transform.position, transform.forward);
				RaycastHit hit;
				if(Physics.Raycast(ray, out hit, distance)) {
					distance = hit.distance;
				}
				
				//the lazer game object is already rotated and has the right
				//origin point, so we just need to scale it to match the ray
				Lazer.transform.localScale = new Vector3(distance, 1, 1);
			}
		}
	}
	
	void Shoot ()
	{
		//lastClick is used to ensure that the player doesnt spam click
		if(lastClick <= 0) {
			//we dont have jump in this game, so i use the Jump axis for fireballs
			if(Input.GetAxis("Jump") != 0) {
				Vector2 mPos = Input.mousePosition;
				//here is the wierd inverting problem with unity
				//caused by the camera and the screen having different axes
				mPos.y = Camera.main.pixelHeight - mPos.y;
				
				//If the mouse is NOT over the gui
				if(!gui.IsOverGui(mPos)) {
					lastClick = clickDelay;
					
					//spawn a fireball
					GameObject f = (GameObject)Instantiate(fireball, muzzle.transform.position, this.transform.rotation);
					//if there is a muzzleFlash object, spawn that too
					if(muzzleFlash != null) {
						Instantiate(muzzleFlash, muzzle.transform.position, this.transform.rotation);
					}
				}
			}
		}
		
		if(lastClick > 0)
			lastClick -= Time.deltaTime;
		
	}
	
	void Melee ()
	{
		if(lastClick <= 0) {
			if(Input.GetAxis("Fire1") != 0) {
				Vector2 mPos = Input.mousePosition;
				mPos.y = Camera.main.pixelHeight - mPos.y;
				
				if(!gui.IsOverGui(mPos)) {
					lastClick = clickDelay;
														
					Ray ray = new Ray(transform.position, transform.forward);
					RaycastHit hit;
					if(Physics.SphereCast(ray, (meleeRange/2), out hit, (meleeRange/2), ~LayerMask.NameToLayer("Player"))) {
						hit.collider.gameObject.SendMessage("Damage", 10, SendMessageOptions.DontRequireReceiver);
						//TODO this should be a different effect :P
						Instantiate(muzzleFlash, muzzle.transform.position, this.transform.rotation);
						Debug.Log("Melee, success!");
					} else {
						Debug.Log("Melee, but not hit");
					}
				}
			}
		}
		
		if(lastClick > 0)
			lastClick -= Time.deltaTime;
	}
	
}
