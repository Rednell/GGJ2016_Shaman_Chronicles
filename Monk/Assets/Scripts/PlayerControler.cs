using UnityEngine;
using System.Collections;

public class PlayerControler : MonoBehaviour {
	public LayerMask layer;

	// Use this for initialization
	void Start () {
		RaycastHit hit;
		if (Physics.Raycast(new Ray(transform.position, Vector3.down* 0.75f), out hit, 1f, layer.value)) {
			if (hit.collider.GetComponent<MazeFloor>())
				transform.parent = hit.collider.transform;
			else
				throw new System.Exception("Place the Player over some MazeFloor");
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.timeScale < 0.9f) return;
			
		if (Input.GetKeyDown(KeyCode.LeftArrow)) {
			transform.localRotation *= Quaternion.AngleAxis(-90, Vector3.up);
		}

		if (Input.GetKeyDown(KeyCode.RightArrow)) {
			transform.localRotation *= Quaternion.AngleAxis(90, Vector3.up);
		}

		if (Input.GetKeyDown(KeyCode.UpArrow)) {
			MoveIfCan(transform.forward);
		}

		if (Input.GetKeyDown(KeyCode.DownArrow)) {
			MoveIfCan(-transform.forward);
		}
	}

	void MoveIfCan (Vector3 delta) {
		RaycastHit hit;
		// Check if not a wall
		if (Physics.Raycast(new Ray(transform.position, delta), out hit, 1f, layer.value)) {
			if (hit.collider.GetComponent<MazeFloor>())
				return;
		}

		// Check if have floor bellow
		if (Physics.Raycast(new Ray(transform.position + delta, Vector3.down), out hit, 1f, layer.value)) {
			if (!hit.collider.GetComponent<MazeFloor>())
				return;
		} else {
			return; // cant move in float air
		}

		transform.localPosition += delta; 
	}

	void OnTriggerEnter (Collider other) {
		if (other.GetComponent<MazeRune>()) {
			// TODO Play some animation while in the animation this script will be disabled
		}
	}

	void OnDrawGizmos () {
		Gizmos.color = Color.red;
		Gizmos.DrawRay(transform.position, transform.forward);
	}
}
