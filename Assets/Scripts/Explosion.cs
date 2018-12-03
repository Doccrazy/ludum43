using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {
	public float damage;

	// Use this for initialization
	void Start () {
		StartCoroutine(DisableCollider());
	}

	private IEnumerator DisableCollider() {
		yield return new WaitForSeconds(0.25f);
		GetComponent<Collider2D>().enabled = false;
	}

	// Update is called once per frame
	void Update () {
	}

	private void OnTriggerEnter2D(Collider2D other) {
		var health = other.gameObject.GetComponent<Health>();
		if (health) {
			health.Damage(damage);
		}
	}
}
