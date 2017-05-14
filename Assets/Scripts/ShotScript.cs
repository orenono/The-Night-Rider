using UnityEngine;
using System.Collections;

public class ShotScript : MonoBehaviour {

	public int damage = 1; //damage inflicted
	public bool isEnemyShot = false; // projectile damage player or enemy

	// Use this for initialization
	void Start () {

		Destroy (gameObject, 20); //we destroy the object after 20 seconds to avoid any leak 
	
	}
	
	// Update is called once per frame
	/*
	void Update () {
	
	}
*/
}
