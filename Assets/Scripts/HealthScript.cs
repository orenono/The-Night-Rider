using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HealthScript : MonoBehaviour {

	public int hp = 1; // total hitpoints
	public bool isEnemy = true; //enemy or player
	public bool isExploding = false;
	public ParticleSystem explosion = null;
	public bool isAudio = false;
	public AudioClip boom = null;




	/// <summary>
	/// inflicts damage and check if the object should be destroyed
	/// </summary>
	///<param name="damageCount"></param>


	// Use this for initialization
	void Start () {


	
	}

	// Update is called once per frame
	//void Update () {

	

	//}


	public void Damage (int damageCount) {
		hp -= damageCount;

		if (hp <= 0)
		{
			//explosion
			if (isExploding == true) {
				Instantiate (explosion, transform.position, Quaternion.identity);
			}
			//play audio effect

			if (isAudio == true) {
				AudioSource.PlayClipAtPoint (boom, transform.position);
			}
			GameControl.Instance.increaseScore (1);

			Destroy (gameObject); //dead!

		}
	}

	void OnTriggerEnter2D (Collider2D otherCollider) {

		//Is this a shot?
		ShotScript shot = otherCollider.gameObject.GetComponent<ShotScript> ();
		if (shot != null) {
			//avoid friendly fire
			if (shot.isEnemyShot != isEnemy) {
				Damage (shot.damage);
				//destroy the shot

				Destroy (shot.gameObject); //remember to always target the game object, otherwise you will just remove the script
			}
		}
	}




}
