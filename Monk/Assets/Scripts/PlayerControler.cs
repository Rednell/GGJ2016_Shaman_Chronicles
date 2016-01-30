using UnityEngine;
using System.Collections;

public class PlayerControler : MonoBehaviour {
	public LayerMask layer;
	public float moveSpeed = 4;
	public float turnSpeed = 10;

	Vector3 targetPosition;
	Quaternion targetRotation;

	// Use this for initialization
	void Start () {
		RaycastHit hit;
		if (Physics.Raycast(new Ray(transform.position, Vector3.down* 0.75f), out hit, 1f, layer.value)) {
			if (hit.collider.GetComponent<MazeFloor>())
				transform.parent = hit.collider.transform;
			else
				throw new System.Exception("Place the Player over some MazeFloor");
		}

		targetPosition = transform.localPosition;
		targetRotation = transform.localRotation;
	}
	
	// Update is called once per frame
	void Update () {
		transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, turnSpeed * Time.deltaTime);
		transform.localPosition = Vector3.Lerp (transform.localPosition, targetPosition, moveSpeed * Time.deltaTime);

		if (Time.timeScale < 0.9f) return;
			
		if (Input.GetKeyDown(KeyCode.LeftArrow)) {
			Turn(-90);
		}

		if (Input.GetKeyDown(KeyCode.RightArrow)) {
			Turn(90);
		}

		if (Input.GetKeyDown(KeyCode.UpArrow)) {
			MoveIfCan(targetRotation * Vector3.forward);
		}

		if (Input.GetKeyDown(KeyCode.DownArrow)) {
			MoveIfCan(-(targetRotation * Vector3.forward));
		}
	}

	void Turn (float angle) {
		transform.localRotation = targetRotation;
		targetRotation *= Quaternion.AngleAxis(angle, Vector3.up);
	}

	void MoveIfCan (Vector3 delta) {
		RaycastHit hit;
		// Check if not a wall
		if (Physics.Raycast(new Ray(transform.TransformPoint(targetPosition), delta), out hit, 1f, layer.value)) {
			if (hit.collider.GetComponent<MazeFloor>())
				return;
		}

		// Check if have floor bellow
		if (Physics.Raycast(new Ray(transform.TransformPoint(targetPosition) + delta, Vector3.down), out hit, 0.75f)) {
			if (!hit.collider.GetComponent<MazeFloor>())
				return;
		} else {
			return; // cant move in float air
		}

		targetPosition += delta; 
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
