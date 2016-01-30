using UnityEngine;
using System.Collections;

public class PlayerSpiritualGuideTalks : MonoBehaviour {

	public string[] text;

	// Use this for initialization
	void Start () {
		foreach (var collider in GetComponents<Collider>()) {
			collider.isTrigger = true;
		}
	}
	
//	// Update is called once per frame
//	void Update () {
//	
//	}

	void OnTriggerEnter (Collider other) {
		PlayerControler player = other.GetComponent<PlayerControler>();
		if (player) {
			TalkToMe.Speech(text);
			Destroy(gameObject);
		}
	} 
}
