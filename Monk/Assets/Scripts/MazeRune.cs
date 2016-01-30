using UnityEngine;
using System.Collections;

public class MazeRune : MonoBehaviour {

	MazeFloor floor;

	// Use this for initialization
	void Start () {
		RaycastHit hit;
		if (Physics.Raycast(new Ray(transform.position, Vector3.down* 0.75f), out hit)) {
			floor = hit.collider.GetComponent<MazeFloor>();
			if (floor.rune)
				throw new System.Exception("Only one rune per floor is allowed");

			floor.rune = this;
			transform.parent = floor.transform;
		} else {
			throw new System.NullReferenceException("Place the rune over one MazeFloor");
		}

		foreach (var collider in GetComponents<Collider>()) {
			collider.isTrigger = true;
		}
	}

	void OnTriggerEnter (Collider other) {
		if (other.GetComponent<PlayerControler>()) {
			PickUpRune ();
		}
	}

	void PickUpRune () {
		floor.MoveDown(); // move floor down
		Destroy(gameObject); // destroy rune !
	}

	void OnDrawGizmosSelected () {
		RaycastHit hit;
		if (Physics.Raycast(new Ray(transform.position, Vector3.down* 0.75f), out hit)) {
			MazeFloor floor = hit.collider.GetComponent<MazeFloor>();
			if (floor) {
				Gizmos.color = Color.yellow;
				Gizmos.DrawMesh(floor.GetComponent<MeshFilter>().sharedMesh, floor.transform.position, floor.transform.rotation);
			}
		}
	}
}
