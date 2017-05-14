using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


/// <summary>
/// Title screen script
/// </summary>

public class MenuScript : MonoBehaviour {

	public void StartGame(){

		//"stage1" is the name of the first stage


		SceneManager.LoadScene ("Stage1");

	}

	public void Directions() {
		SceneManager.LoadScene ("Directions");
	}

	public void QuitGame() {
		Application.Quit ();
	}



}
