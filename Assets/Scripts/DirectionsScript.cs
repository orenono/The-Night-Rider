using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DirectionsScript : MonoBehaviour {

	public Button backToMenuButton;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	public void BackToMenu() {
		SceneManager.LoadScene ("Menu1");
	}
}
