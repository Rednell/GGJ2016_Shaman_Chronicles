using UnityEngine;
using System.Collections;

public class PlayerControlScript : MonoBehaviour {

    public float movementDistance = 1.0f;
    public float timeMovement = 0.5f;
    float TimeStarted;

    //from here are new elements
    //used to detect if there's a wall in front or in the back of the player
    private MovementCollisionScript front;
    private MovementCollisionScript back;

    //states of the player
    private enum PlayerState {iddle, moving};
    private PlayerState state;

    //used for movement
    private Vector3 startPosition;
    private Vector3 destinyPosition;

	// Use this for initialization
	void Start () {
        state = PlayerState.iddle;
        this.front = transform.FindChild("Front").gameObject.GetComponent<MovementCollisionScript>();
        this.back = transform.FindChild("Back").gameObject.GetComponent<MovementCollisionScript>();
	}
	
	// Update is called once per frame
	void Update () {
        //transform.position += new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * 0.5f;
        if (state.Equals(PlayerState.iddle))
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                transform.localRotation *= Quaternion.AngleAxis(-90, Vector3.up);
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                transform.localRotation *= Quaternion.AngleAxis(90, Vector3.up);
            }

            /*if (Input.GetKeyDown(KeyCode.W) && front.CanMove)
            {
                transform.localPosition += transform.forward;
            }

            if (Input.GetKeyDown(KeyCode.S) && back.CanMove)
            {
                transform.localPosition -= transform.forward;
            }*/
            if (Input.GetKeyDown(KeyCode.W) && front.CanMove)
            {
                setMovement(transform.localPosition, transform.forward);
            }

            if (Input.GetKeyDown(KeyCode.S) && back.CanMove)
            {
                setMovement(transform.localPosition, -transform.forward);
            }
        } else if (state.Equals(PlayerState.moving)){
            float actualTime = Time.time;
            float timePassedBy = actualTime - TimeStarted;
            float progress = timePassedBy / timeMovement;
            transform.localPosition = Vector3.Lerp(startPosition, destinyPosition, progress);
            if (timePassedBy >= timeMovement) {
                state = PlayerState.iddle;
            }
        }
    }

    void setMovement(Vector3 startPoint, Vector3 distance) {
        this.startPosition = startPoint;
        this.destinyPosition = startPoint + distance;
        this.state = PlayerState.moving;
        this.TimeStarted = Time.time;
    }

}
