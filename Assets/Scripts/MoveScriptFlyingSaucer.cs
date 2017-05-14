using UnityEngine;
using System.Collections;

public class MoveScriptFlyingSaucer : MonoBehaviour {

	public Vector2 speed = new Vector2 (10, 10); //object speed
	public Vector2 direction = new Vector2 (-1, 0); //moving direction

	private Vector2 movement; 
	private Rigidbody2D rigidbodyComponent;

	/*
	// Use this for initialization
	void Start () {
	
	}
	*/

	// Update is called once per frame
	void Update () {


		movement = new Vector2 (speed.x * direction.x, speed.y * direction.y);

		if (rigidbodyComponent.transform.position.y < 0)
			direction.y = 1;


		
	
	}

	void FixedUpdate() {

		if (rigidbodyComponent == null)
			rigidbodyComponent = GetComponent<Rigidbody2D>();

		rigidbodyComponent.velocity = movement;
	}

}
