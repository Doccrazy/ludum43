using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using Random = UnityEngine.Random;

public class LaserTower : MonoBehaviour {
	public float offTime = 1f;
	public float prepareTime = 0.25f;
	public float onTime = 0.5f;
	public Laser laser;
	private Laser _laser;

	// Use this for initialization
	void Start () {
		StartCoroutine(fire());
		_laser = Instantiate(laser);
		_laser.gameObject.SetActive(false);
		_laser.spinupTime = prepareTime;
		_laser.transform.position = transform.position;
		_laser.transform.rotation = transform.rotation;
		_laser.transform.Translate(0.1f, 0, 0, Space.Self);

		var hit = Physics2D.Raycast(_laser.transform.position,
			_laser.transform.right, Mathf.Infinity, LayerMask.GetMask("Level"));
		_laser.length = hit.collider ? hit.distance : 2f;
	}

	private void OnDestroy() {
		Destroy(_laser);
	}

	// Update is called once per frame
	void FixedUpdate () {
	}

	private IEnumerator fire() {
		yield return new WaitForSeconds(Random.Range(0f, onTime + offTime + prepareTime));
		while (true) {
			yield return new WaitForSeconds(offTime);
			_laser.gameObject.SetActive(true);
			yield return new WaitForSeconds(prepareTime);
			_laser.SetHot();
			yield return new WaitForSeconds(onTime);
			_laser.gameObject.SetActive(false);
		}
	}
}
