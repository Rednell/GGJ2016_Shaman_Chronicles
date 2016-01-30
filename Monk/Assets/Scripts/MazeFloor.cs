using UnityEngine;
using System.Collections;

public class MazeFloor : MonoBehaviour {

	public MazeRune rune;

	public bool isDown {
		get;
		private set;
	}

	Vector3 targetPosition;

	// Use this for initialization
	void Start () {
		targetPosition = transform.localPosition;
	}
	
	// Update is called once per frame
	void Update () {
		transform.localPosition = Vector3.MoveTowards(transform.localPosition, targetPosition, MonkGamePrefs.MazeFloorSpeed * Time.deltaTime);
	}

	public void MoveDown () {
		if (isDown) return;

		// Last level
		if (transform.parent == null) {
			Debug.Log("<b>Map Completed!</b>");
		} else {
			targetPosition += Vector3.down;
		}

		isDown = true;
	}
}
