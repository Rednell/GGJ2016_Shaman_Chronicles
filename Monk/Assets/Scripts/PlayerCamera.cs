using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class PlayerCamera : MonoBehaviour {

	static PlayerCamera instance;
	public float orthographicSize;
	public float cameraFollowSpeed;
	public float cameraZoom;
	public float cameraZoomSpeed;

	Vector3 offset;
	PlayerControler player;
	Camera thisCamera;

	float zoom = 1, zoomTarget = 1;

	// Use this for initialization
	void Awake () {
		instance = this;
		thisCamera = GetComponent<Camera>();
		player = FindObjectOfType<PlayerControler>();
		offset = transform.forward * -10;
		transform.position = offset + player.transform.position;
	}

	// Update is called once per frame
	void Update () {
		if (Application.isEditor && !Application.isPlaying) {
			Awake ();
		}

		zoom = Mathf.MoveTowards(zoom, zoomTarget, cameraZoomSpeed * Time.unscaledDeltaTime);
		thisCamera.orthographicSize = orthographicSize * zoom;

		transform.position = Vector3.MoveTowards (
			transform.position,
			player.transform.position + offset,
			cameraFollowSpeed * Time.unscaledDeltaTime );
	}

	void OnValidate() {
		GetComponent<Camera>().orthographicSize = orthographicSize;
	}

	void OnDestroy () {
		instance = null;
	}

	public static void Focus () {
		if (!instance)
			throw new System.NullReferenceException("Need at least one PlayerCamera script in the scene");
		
		instance.zoomTarget = 1 / instance.cameraZoom;
	}

	public static void Unfocus () {
		if (!instance)
			throw new System.NullReferenceException("Need at least one PlayerCamera script in the scene");
		
		instance.zoomTarget = 1;
	}
}
