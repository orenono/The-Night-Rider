using UnityEngine;
using System.Collections;
/// <summary>
/// Enemy basic behavior
/// </summary>

public class FlyingSauceScript : MonoBehaviour
{
	private bool hasSpawn;
	private MoveScriptFlyingSaucer moveScript;
	private WeaponScript[] weapons;
	private Collider2D coliderComponent;
	private SpriteRenderer rendererComponent;
	public bool isAudio = false;
	public AudioClip shot= null;

	void Awake()
	{
		// Retrieve the weapon only once
		weapons = GetComponentsInChildren<WeaponScript>();

		// Retrieve scripts to disable when not spawn
		moveScript = GetComponent<MoveScriptFlyingSaucer>();

		coliderComponent = GetComponent<Collider2D>();

		rendererComponent = GetComponent<SpriteRenderer>();
	}

	//  Disable everything
	void Start()
	{
		hasSpawn = false;

		// Disable everything
		// Disable collider
		coliderComponent.enabled = false;
		// Disable Moving
		moveScript.enabled = false;
		// Disable Shooting
		foreach (WeaponScript weapon in weapons)
		{
			weapon.enabled = false;
		}
	}

	void Update()
	{
		//  Check if the enemy has spawned.
		if (hasSpawn == false)
		{
			if (rendererComponent.IsVisibleFrom(Camera.main))
			{
				Spawn();
			}
		}
		else
		{
			// Auto-fire
			foreach (WeaponScript weapon in weapons)
			{
				if (weapon != null && weapon.enabled && weapon.CanAttack)
				{
					weapon.Attack(true);
					if (isAudio == true) {
						AudioSource.PlayClipAtPoint (shot, transform.position);
					}

				}
			}

			// If out of the camera's sight, destroy the game object.
			if (rendererComponent.IsVisibleFrom(Camera.main) == false)
			{
				Destroy(gameObject);
			}
		}
	}

	// Activate itself.
	private void Spawn()
	{
		hasSpawn = true;

		// Enable everything
		// Enable Moving
		moveScript.enabled = true;
		// Enable Collider
		coliderComponent.enabled = true;

		// Enable Shooting
		foreach (WeaponScript weapon in weapons)
		{
			weapon.enabled = true;
		}
	}
}