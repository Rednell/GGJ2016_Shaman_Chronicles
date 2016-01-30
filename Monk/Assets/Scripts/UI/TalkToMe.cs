using UnityEngine;
using UnityEngine.UI;

public class TalkToMe : MonoBehaviour {

	static TalkToMe instance;

	string[] text;
	int textId;

	public Text saying;
	public Image haveMore;

	// Use this for initialization
	void Awake () {
		instance = this;
		gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.anyKeyDown) {
			textId++;
			if (textId < text.Length) {
				Show ();
			} else {
				Hide ();
			}
		}
	}

	void OnDestroy () {
		instance = null;
	}

	public static void Speech (string[] text) {
		if (!instance)
			throw new System.NullReferenceException("Need at least one TalkToMe script in the scene");

		if (text == null || text.Length == 0)
			throw new System.Exception("Text to speech is null or empty");

		instance.text = text;
		instance.textId = 0;
		instance.Show ();
	}

	void Show () {
		Time.timeScale = 0.002f;
		PlayerCamera.Focus();

		gameObject.SetActive(true);
		saying.text = text[textId];
		haveMore.enabled = (textId + 1) < text.Length;
	}

	void Hide () {
		Time.timeScale = 1;
		PlayerCamera.Unfocus();
		gameObject.SetActive(false);
	}
}
