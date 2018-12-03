using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour {
	public GameObject Player;

	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
	}

	private void LateUpdate() {
		if (!Player) {
			return;
		}
		transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y, transform.position.z);
	}
}
