using UnityEngine;
using System.Collections;

public class Spawn : MonoBehaviour {

	public GameObject[] prefabs;

	// Use this for initialization
	void Awake () {
		foreach(var prefab in prefabs) {
			GameObject.Instantiate(prefab);
		}
	}
}
