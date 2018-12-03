using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinZone : MonoBehaviour {
	public Health player;
	public bool triggered;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	private void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.GetComponent<Health>() == player && !triggered) {
			player.invulnerable = true;
			GameState.AddScore(5000 * GameState.Level);
			triggered = true;
		}
	}
}
