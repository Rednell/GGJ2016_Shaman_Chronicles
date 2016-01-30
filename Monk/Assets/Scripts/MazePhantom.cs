using UnityEngine;
using System.Collections;

public class MazePhantom : MonoBehaviour {
	
	public MazePhantonPath path {
		get;
		set;
	}

	public LayerMask layer;
	public float moveSpeed = 4;
	public float turnSpeed = 400;

	int pathId = 0;
	int inc = +1;
	float timer = 0;

	Vector3 targetPosition;
	Quaternion targetRotation;

	// Use this for initialization
	void Start () {
		RaycastHit hit;
		if (Physics.Raycast(new Ray(transform.position, Vector3.down* 0.75f), out hit, 1f, layer.value)) {
			if (hit.collider.GetComponent<MazeFloor>())
				transform.parent = hit.collider.transform;
			else
				throw new System.Exception("Place the first node of MazePhantomPath over some MazeFloor");
		}

		targetPosition = transform.localPosition;
		targetRotation = transform.localRotation;
	}

	void Update () {
		transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, turnSpeed * Time.deltaTime);
		if (Quaternion.Angle(targetRotation, transform.localRotation) > 1e-4f) { // rotation not ended
			return;
		}

		transform.localPosition = Vector3.Lerp (transform.localPosition, targetPosition, moveSpeed * Time.deltaTime);
		if ((transform.localPosition - targetPosition).magnitude > 1e-4f) { // move not ended
			return;
		}

		timer += Time.deltaTime;

		int nextId = pathId + inc;
		if (nextId < 0 || (nextId >= path.path.Length)) {
			// end of path turn around
			Turn ();
			return;
		}

		if (timer < path.wait [nextId])
			return;

		if (MoveIfCan (path.path[nextId] - path.path[pathId])) {
			// cant move reverse and try again next time
			Turn ();
		} else {
			// can move reset timer ant wait
			timer = 0;
			pathId = nextId;
		}
	}

	bool MoveIfCan (Vector3 delta) {
		RaycastHit hit;
		// Check if not a wall
		if (Physics.Raycast(new Ray(transform.position, delta), out hit, 1f, layer.value)) {
			if (hit.collider.GetComponent<MazeFloor>())
				return true;
		}

		// Check if have floor bellow
		if (Physics.Raycast(new Ray(transform.position + delta, Vector3.down), out hit, 1f, layer.value)) {
			if (!hit.collider.GetComponent<MazeFloor>())
				return true;
		} else {
			return true; // cant move in float air
		}

		targetRotation = Quaternion.LookRotation (delta);
		targetPosition += delta;
		return false;
	}

	void Turn () {
		inc *= -1;
		targetRotation *= Quaternion.AngleAxis (180, Vector3.up);
	}

	void OnDrawGizmos () {
		Gizmos.color = Color.red;
		Gizmos.DrawRay(transform.position, transform.forward);
	}
}
