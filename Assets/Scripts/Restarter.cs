using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;

public class Restarter : MonoBehaviour {
	public GameObject player;
	public WinZone winZone;
	private bool _warping;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		if (CrossPlatformInputManager.GetButtonDown("Submit") && !_warping && (!player || winZone.triggered)) {
			if (winZone.triggered) {
				_warping = true;
				StartCoroutine(WarpNextLevel());
			}
			else {
				if (GameState.Lives > 0) {
					GameState.Lives--;
					GameState.ResetLevel();
				}
				else {
					GameState.ResetAll();
				}

				SceneManager.LoadScene(SceneManager.GetSceneAt(0).name);
			}
		}
	}

	private IEnumerator WarpNextLevel() {
		player.GetComponent<PlayerControl>().DisableSpeedLimit();
		var tStart = Time.time;
		while (Time.time - tStart < 4.0f) {
			player.GetComponent<Rigidbody2D>().AddForce(Vector2.right * player.GetComponent<PlayerControl>().accel * 4f);
			yield return new WaitForFixedUpdate();
		}
		GameState.Level++;
		SceneManager.LoadScene(SceneManager.GetSceneAt(0).name);
	}
}
