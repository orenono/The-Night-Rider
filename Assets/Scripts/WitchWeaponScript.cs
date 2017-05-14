using UnityEngine;
using System.Collections;

public class WitchWeaponScript : MonoBehaviour {

	/// <summary>
	/// Projectile prefab for shooting
	/// </summary>
	public Transform shotPrefab;

	/// <summary>
	/// Cooldown in seconds between shots
	/// </summary>
	public float shootingRange = 1.0f;

	public float shootCooldown = 1.0f; //cooldown

	private HealthScriptWitch health;

	private int score;

	// Use this for initialization

	void Start () {

		health = GetComponentInParent<HealthScriptWitch> ();
		score = health.hp;
	
	}

	// Update is called once per frame
	void Update () {
		
		if (shootCooldown > 0) {
			shootCooldown -= Time.deltaTime;
		}

		if (score > health.hp) {

			Attack (true);
			}

		score = health.hp;
	}
	//--------------
	// 3 - shooting from another script
	//

	//create a new projectile if possible
	public void Attack(bool isEnemy) {

		if (CanAttack) {
			shootCooldown = shootingRange;

			//create new shot
			var shotTransform = Instantiate (shotPrefab) as Transform;

			//Assign position
			shotTransform.position = transform.position;

			//the is enemy property
			ShotScript shot = shotTransform.gameObject.GetComponent<ShotScript> ();

			if (shot != null) {
				shot.isEnemyShot = isEnemy;
			}

			//make the weapon shot always towards it
			MoveScript move = shotTransform.gameObject.GetComponent<MoveScript> ();
			if (move != null) {
				move.direction = this.transform.right; // towards in 2d space is the right of the sprite
			}
		}
	}


	//Is the weapon ready to create a new projectile?

	public bool CanAttack {
		get {
			return shootCooldown <= 0f;
		}
	}




}


