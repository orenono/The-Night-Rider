using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
/// <summary>
/// Start or quit the game
/// </summary>
public class GameOverScript : MonoBehaviour {

	private Button[] buttons;

	void Awake() {

		//Get the buttons
		buttons = GetComponentsInChildren<Button> ();

		//disable them
		HideButtons ();
	}

	public void HideButtons() {

		foreach (var b in buttons) {
			b.gameObject.SetActive (false);
		}
	}

	public void ShowButtons() {
		foreach (var b in buttons) {
			b.gameObject.SetActive (true);
		}
	}

	public void ExitToMenu() {
		//reload the level
		SceneManager.LoadScene ("Menu1");
	}

	public void RestartGame () {

		//reload the level

		SceneManager.LoadScene ("Stage1");
	}

	public void LoadDirections() {
		SceneManager.LoadScene ("Directions");
	}


}
