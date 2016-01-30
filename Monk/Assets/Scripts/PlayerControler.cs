using UnityEngine;
using System.Collections;

public class PlayerControler : MonoBehaviour {
	public LayerMask layer;
    public float timeMovement;
    float TimeStarted;

    Vector3 positionTarget;

    //states of the player
    private enum PlayerState { iddle, moving };
    private PlayerState state;

    //used for movement
    private Vector3 startPosition;
    private Vector3 destinyPosition;

    // Use this for initialization
    void Start () {
        RaycastHit hit;
		if (Physics.Raycast(new Ray(transform.position, Vector3.down* 0.75f), out hit, 1f, layer.value)) {
			if (hit.collider.GetComponent<MazeFloor>())
				transform.parent = hit.collider.transform;
			else
				throw new System.Exception("Place the Player over some MazeFloor");
		}

        positionTarget = transform.localPosition;
    }
	
	// Update is called once per frame
	void Update () {

        //transform.localPosition = Vector3.Lerp(transform.localPosition, positionTarget, moveSpeed * Time.deltaTime);

		if (Time.timeScale < 0.9f) return;
        if (state.Equals(PlayerState.iddle)){
            if (Input.GetKeyDown(KeyCode.LeftArrow)) {
                transform.localRotation *= Quaternion.AngleAxis(-90, Vector3.up);
            }

            if (Input.GetKeyDown(KeyCode.RightArrow)) {
                transform.localRotation *= Quaternion.AngleAxis(90, Vector3.up);
            }

            if (Input.GetKey(KeyCode.UpArrow)) {
                MoveIfCan(transform.forward);
            }

            if (Input.GetKey(KeyCode.DownArrow)) {
                MoveIfCan(-transform.forward);
            }
        } else if (state.Equals(PlayerState.moving)) {
            float actualTime = Time.time;
            float timePassedBy = actualTime - TimeStarted;
            float progress = timePassedBy / timeMovement;
            transform.
            transform.localPosition = Vector3.Lerp(startPosition, destinyPosition, progress);
            if (timePassedBy >= timeMovement)
            {
                state = PlayerState.iddle;
            }
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

        startPosition = transform.localPosition;
        destinyPosition = transform.localPosition + delta;
        this.TimeStarted = Time.time;
        state = PlayerState.moving;
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
