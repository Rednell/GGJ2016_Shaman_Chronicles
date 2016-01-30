using UnityEngine;
using System.Collections;

public class MazePhantonPath : MonoBehaviour {

	public Vector3[] path = new Vector3[] { Vector3.zero, Vector3.forward };
	public float[] wait = new float[] { 0, 0 };
	public MazePhantom template;
	public float delay;

	public Color pathColor = Color.yellow;

	MazePhantom phatom;

	void Awake () {
		if (path.Length < 2)
			throw new System.Exception ("The MazePhantonPath needs to be at least 2 poitns");
		
		phatom = (MazePhantom) Instantiate (template, path [0], Quaternion.LookRotation (path [1] - path [0]));
		phatom.path = this;
	}

	void OnValidate () {
		if (path.Length > 0)
			transform.position = path [0];
		
		if (wait.Length != path.Length)
			System.Array.Resize (ref wait, path.Length);
	}

	void OnDrawGizmos () {
		if (path.Length < 1) return;

		Gizmos.color = pathColor;

		Vector3 v0 = path [0];
		Gizmos.DrawCube(v0, Vector3.one * 0.25f);

		for (int i = 1; i < path.Length; i++) {
			Vector3 v1 = path [i];
			Gizmos.DrawLine (v0, v1);
			v0 = v1;
		}
	}
}
