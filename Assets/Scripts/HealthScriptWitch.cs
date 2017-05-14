using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HealthScriptWitch : MonoBehaviour {

	public int hp = 1; // total hitpoints
	public bool isEnemy = true; //enemy or player
	public bool isExploding = false;
	public ParticleSystem explosion = null;
	public bool isAudio = false;
	public AudioClip boom = null;
	private Animator anim;



	void Start(){

		anim = GetComponent<Animator> ();


	}

	/// <summary>
	/// inflicts damage and check if the object should be destroyed
	/// </summary>
	///<param name="damageCount"></param>
	/// 
	public void Damage (int damageCount) {
		hp -= damageCount;

		if (hp <= 0) {

			//play audio effect

			if (isAudio == true) {
				AudioSource.PlayClipAtPoint (boom, transform.position);
			}
			anim.SetTrigger ("witchAttack");
		}

		if (hp <= -3) {
			if (isAudio == true) {
				AudioSource.PlayClipAtPoint (boom, transform.position);
			}
			if (isExploding == true) {
				Instantiate (explosion, transform.position, Quaternion.identity);
			}
			GameControl.Instance.increaseScore (1);
			Destroy (gameObject);

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

	/*
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	*/
}
