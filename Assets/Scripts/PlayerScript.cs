using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour {

	public Vector2 speed = new Vector2 (50, 50); //speed of ship
	private Vector2 movement; //to store the movement and the component
	private Rigidbody2D rigidbodyComponent;
	public AudioClip shot;
	public Text health;
	private HealthScript healthScript;
	public Text scoreText;
	private WeaponScript weaponScript;
	//public string name;
	public int score;




	// Use this for initialization
	void Start () {

		//name = GameControl.Instance.getName ();
		score = GameControl.Instance.getScore ();
		healthScript = GetComponent<HealthScript> ();
		weaponScript = GetComponent<WeaponScript> ();
		if (score < 12) {
			scoreText.text = "Your score is " + score + "\n" + "you need 12 points in order to advance to the next level!";
		} else if (score > 11 && score < 20) {
			scoreText.text = "Your score is " + score + "\n" + "you need 20 points in order to advance to the next level!";
		} else if (score >= 20) {
			scoreText.text = "Your score is " + score + "\n" + "you need 100 points in order to advance to the next level!";
		}
	}



	// Update is called once per frame
	void Update () {

		score = GameControl.Instance.getScore ();
		if (score < 12) {
			scoreText.text = "Your score is " + score + "\n" + "you need 12 points in order to advance to the next level!";
		} 
		else if (score >= 12 && score < 20) {
			scoreText.text = "Your score is " + score + "\n" + "you need 20 points in order to advance to the next level!";
		} 
		else if (score >= 20) {
			scoreText.text = "Your score is " + score + "\n" + "you need 100 points in order to advance to the next level!";
		}
		float inputX = Input.GetAxis ("Horizontal");   //get axis information
		float inputY = Input.GetAxis ("Vertical");    //get axis information

		movement = new Vector2 (speed.x * inputX, speed.y * inputY); //movement per direction

		// 5 - shooting
		//bool shoot; = Input.GetButtonDown ("Fire1");
		bool shoot = Input.GetKeyDown (KeyCode.Space);

		if(Input.GetKeyDown(KeyCode.Escape)){
			var gameOver = FindObjectOfType<GameOverScript> ();
			gameOver.ShowButtons ();
		}
			

		if (shoot) {
			WeaponScript weapon = GetComponent<WeaponScript> ();
			if (weapon != null) {
				//false because the player is not an enemy)
				weapon.Attack (false);
				AudioSource.PlayClipAtPoint (shot, transform.position);
			}
		}
		//6 - Make sure we are not outside the camera bounds
		var dist = (transform.position - Camera.main.transform.position).z;

		var leftBorder = Camera.main.ViewportToWorldPoint (new Vector3 (0, 0, dist)).x;
		var rightBorder = Camera.main.ViewportToWorldPoint (new Vector3 (1, 0, dist)).x;
		var topBorder = Camera.main.ViewportToWorldPoint (new Vector3 (0, 0, dist)).y;
		var bottomBorder = Camera.main.ViewportToWorldPoint (new Vector3 (0,1,dist)).y;

		transform.position = new Vector3 (Mathf.Clamp (transform.position.x, leftBorder, rightBorder), 
			Mathf.Clamp (transform.position.y, topBorder, bottomBorder),
			transform.position.z);

		// show player's health score
		health.text = "health " + healthScript.hp;
	
		//score.text = "score " + weaponScript.numberOfEnemiesKilled;

		if (score == 12 )
		{
			GameControl.Instance.increaseScore (1);
			SceneManager.LoadScene ("Stage2");
		}
		else if(score == 20) {
			GameControl.Instance.increaseScore (1);
			SceneManager.LoadScene ("Stage3");
		}


		//score = GameControl.Instance.getScore ();
	}
	


	void FixedUpdate() {

		if (rigidbodyComponent == null)
			rigidbodyComponent = GetComponent<Rigidbody2D> (); // Get the component and store the reference

		rigidbodyComponent.velocity = movement; // move the game object


	}
	void OnCollisionEnter2D (Collision2D collision)
	{
		bool damagePlayer = false;

		//collision with enemy
		EnemyScript enemy = collision.gameObject.GetComponent<EnemyScript> ();
		if (enemy != null) {
			//kill the enemy
			HealthScript enemyHealth = enemy.GetComponent<HealthScript> ();
			if (enemyHealth != null)
				enemyHealth.Damage (enemyHealth.hp);

			damagePlayer = true;
		}

		// Damage the player
		if (damagePlayer) {
			HealthScript playerHealth = this.GetComponent<HealthScript> ();
			if (playerHealth != null) {
				playerHealth.Damage (1);
			}
		}
	}
	void OnDestroy(){

		//game over
		var gameOver = FindObjectOfType<GameOverScript> ();
		gameOver.ShowButtons ();
	}
}
