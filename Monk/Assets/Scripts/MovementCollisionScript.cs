using UnityEngine;
using System.Collections;

public class MovementCollisionScript : MonoBehaviour {

    private bool canMove = true;
    public bool CanMove
    {
        get
        {
            return canMove;
        }
    }

    void OnTriggerEnter(Collider Other) {
        canMove = false;
    }

    void OnTriggerExit(Collider Other) {
        canMove = true;
    }
}
