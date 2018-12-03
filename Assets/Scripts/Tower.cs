using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using utils;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

public class Tower : MonoBehaviour {
	public float rotSpeed = 45f;
	public float fireRate = 1f;
	public float range = 10f;
	public float maxAngle = 25f;
	public float shotSpeed = 10f;
	public bool smartTargeting = true;
	public GameObject shot;

	private const float Delta = 1f;

	// Use this for initialization
	void Start () {
		StartCoroutine(fire());
	}

	// Update is called once per frame
	void FixedUpdate () {
		var target = TargetFinder.FindTarget(transform, range*1.2f);
		if (!target) {
			return;
		}
		var angleDiff = angleToTarget(target);
		if (inRange(target, 1.2f) && Mathf.Abs(angleDiff) > Delta) {
			transform.RotateAround(transform.position, Vector3.forward, Time.fixedDeltaTime * rotSpeed * Mathf.Sign(angleDiff));
		}
	}

	private float angleToTarget(GameObject target) {
		var toTarget = shotTarget(target) - new Vector2(transform.position.x, transform.position.y);
		return Vector2.SignedAngle(new Vector2(transform.right.x, transform.right.y), toTarget);
	}

	private Vector2 shotTarget(GameObject target) {
		var p = (Vector2)target.transform.position;
		if (smartTargeting) {
			p = p + target.GetComponent<Rigidbody2D>().velocity *
			    Vector2.Distance(target.transform.position, new Vector2(transform.position.x, transform.position.y))/shotSpeed;
		}

		return p;
	}

	private IEnumerator fire() {
		while (true) {
			yield return new WaitForSeconds(1f/fireRate);
			var target = TargetFinder.FindTarget(transform, range*1.2f);
			if (inRange(target, 1.0f) && visible(target) && Mathf.Abs(angleToTarget(target)) < maxAngle) {
				doFire(target);
			}
		}
	}

	private bool inRange(GameObject target, float extend) {
		if (!target) {
			return false;
		}
		return Vector2.Distance(target.transform.position, new Vector2(transform.position.x, transform.position.y)) < range*extend;
	}

	private bool visible(GameObject target) {
		var toTarget = (Vector2)target.transform.position - new Vector2(transform.position.x, transform.position.y);
		var hit = Physics2D.Raycast((Vector2)transform.position + toTarget.normalized * 0.1f,
			toTarget.normalized, Mathf.Infinity, LayerMask.GetMask("Level") | (1 << target.gameObject.layer));
		return hit.collider != null && hit.collider.gameObject == target.gameObject;
	}

	private void doFire(GameObject target) {
		var orientation = new Vector2(transform.right.x, transform.right.y);
		var s = Instantiate(shot);
		s.transform.position = transform.position + transform.right * 0.4f;
		s.GetComponent<Rigidbody2D>().velocity = orientation * shotSpeed;
		if (s.GetComponent<Homing>() != null) {
		}
	}
}
