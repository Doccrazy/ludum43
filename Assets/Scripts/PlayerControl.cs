using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerControl : MonoBehaviour {
	private Rigidbody2D _body;
	public float accel = 10f;
	public float maxSpeed = 10f;
	private bool _limitSpeed = true;

	// Use this for initialization
	void Start () {
		_body = GetComponent<Rigidbody2D>();
	}

	// Update is called once per frame
	void Update () {
		float h = CrossPlatformInputManager.GetAxis("Horizontal");
		float v = CrossPlatformInputManager.GetAxis("Vertical");

		_body.AddForce(new Vector2(h, v) * accel);
		if (_limitSpeed) {
			_body.velocity = new Vector2(Mathf.Clamp(_body.velocity.x, -maxSpeed, maxSpeed),
				Mathf.Clamp(_body.velocity.y, -maxSpeed, maxSpeed));
		}
	}

	public void DisableSpeedLimit() {
		_limitSpeed = false;
	}
}
