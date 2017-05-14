using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameControl : MonoBehaviour {
	//public string playerName;
	public int score;
	public AudioSource music;
	public Toggle toggle;





	public static GameControl Instance;

	// Use this for initialization
	void Start () {
		//playerName
		score = 0;

	}

	void Update() {

	
	}

	void Awake()
	{
		if (Instance == null) {
			DontDestroyOnLoad (gameObject);
			Instance = this;
		} else {
			Destroy (gameObject);
		}
	}

	public void setScore(int s)
	{
		score = s;
	}

	public int getScore()
	{
		return score;
	}

	public void increaseScore (int a)
	{
		score += a;
	}

	//public void setName (string n)
	//{
	//	playerName = n;
	//}

	//public string getName()
	//{
	//	return playerName;
	//}

	public void SoundOnOff() {

		if (toggle.isOn == true)
			music.Play ();
		else if (toggle.isOn == false)
			music.Stop ();
		
	}
}
