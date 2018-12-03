using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.UI;

public class FloatingLabel : MonoBehaviour {
	public float transl = 50f;
	public float scale = 1f;
	private float _tStart;
	private Vector3 _pos;

	// Use this for initialization
	void Start () {
		_tStart = Time.time;
		_pos = transform.position;
	}

	// Update is called once per frame
	void Update () {
		var delta = (Time.time - _tStart) / 1f;
		transform.position = _pos + Vector3.down * delta * transl;
		transform.localScale = Vector3.one * Mathf.Lerp(1.0f, scale, delta);
		if (delta > 1f) {
			Destroy(gameObject);
		}
	}
}
