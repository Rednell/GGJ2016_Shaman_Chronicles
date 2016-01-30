using UnityEngine;
using System.Collections;

public class PauseGame : MonoBehaviour {

	public GameObject ui;
	public GameObject quit;

	// Use this for initialization
	void Start () {
		ui.SetActive(false);
		quit.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape)) {
			if (quit.activeSelf) {
				DontQuit ();
				return;
			}

			TogglePause();
		}
	}

	void Show () {
		ui.SetActive(true);
		quit.SetActive(false);
	}

	void Hide () {
		ui.SetActive(false);
	}

	public void TogglePause() {
		if (Time.timeScale == 1) { // if the game time is 1 this script can pause the game !!!
			Time.timeScale = 0;
			Show ();
		} else if (Time.timeScale == 0) { // and if is zero this script can unpause the game
			Time.timeScale = 1;
			Hide ();
		}
	}

	public void QuitGameLikeAChamp () {
		quit.SetActive(true);
	}

	public void QuitAlredy () {
		Application.Quit();
	}

	public void DontQuit () {
		quit.SetActive(false);
	}
}
