using UnityEngine;
using System.Collections.Generic;
using System.Linq;
/// <summary>
/// Scrolling script that needs to be assigned to a layer
/// </summary>
public class ScrollingScript : MonoBehaviour {

	public Vector2 speed = new Vector2 (2, 2);// scrolling speed
	public Vector2 direction = new Vector2(-1, 0); //moving direction
	public bool isLinkedToCamera = false;  //if movement should be applied to camera

	//background is infinite
	public bool isLooping = false;

	//list of children with a renderer
	private List<SpriteRenderer> backgroundPart;

	//get all the children
	void Start() {

		//for infinite background only
		if (isLooping)
		{
			//get all the children of the layer with a renderer
			backgroundPart = new List<SpriteRenderer>();

			for (int i =0; i < transform.childCount; i++)
			{
				Transform child = transform.GetChild (i);
				SpriteRenderer r = child.GetComponent<SpriteRenderer>();

				//add only the visible children
				if (r != null)
				{
					backgroundPart.Add(r);
				}
			}

			//sort by position
			//we need to add a few conditions to handle all possible scrolling directions
			backgroundPart = backgroundPart.OrderBy( t => t.transform.position.x).ToList();
		}
	}


	
	// Update is called once per frame
	void Update () {

		//movement
		Vector3 movement = new Vector3(speed.x * direction.x, speed.y * direction.y, 0);
		movement *= Time.deltaTime;
		transform.Translate (movement);

		//move the camera
		if (isLinkedToCamera) {
			Camera.main.transform.Translate (movement);
		}

		// loop
		if (isLooping)
		{
			// Get the first object.
			// The list is ordered from left (x position) to right.
			SpriteRenderer firstChild = backgroundPart.FirstOrDefault();

			if (firstChild != null)
			{
				// Check if the child is already (partly) before the camera.
				// We test the position first because the IsVisibleFrom
				// method is a bit heavier to execute.
				if (firstChild.transform.position.x < Camera.main.transform.position.x)
				{
					// If the child is already on the left of the camera,
					// we test if it's completely outside and needs to be
					// recycled.
					if (firstChild.IsVisibleFrom(Camera.main) == false)
					{
						// Get the last child position.
						SpriteRenderer lastChild = backgroundPart.LastOrDefault();
						Vector3 lastPosition = lastChild.transform.position;
						Vector3 lastSize = (lastChild.bounds.max - lastChild.bounds.min);

						// Set the position of the recyled one to be AFTER
						// the last child.
						// Note: Only work for horizontal scrolling currently.
						firstChild.transform.position = new Vector3(lastPosition.x + lastSize.x, firstChild.transform.position.y, firstChild.transform.position.z);

						// Set the recycled child to the last position
						// of the backgroundPart list.
						backgroundPart.Remove(firstChild);
						backgroundPart.Add(firstChild);
					}
				}
			}
		}
	
	}
}
