using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Laser : MonoBehaviour {
	public float length = 10f;
	public float spinupTime = 0.25f;
	public float damage = 50f;
	public Material material;
	public Material materialHot;
	private float _width;
	private float _tStart;
	private bool _danger;
	private LineRenderer _lineRenderer;

	// Use this for initialization
	void Start () {
		_lineRenderer = GetComponent<LineRenderer>();
		_width = _lineRenderer.widthMultiplier;
		GetComponent<BoxCollider2D>().offset = Vector2.right * length/2f;
		GetComponent<BoxCollider2D>().size = new Vector2(length, 0.1f);
	}

	private void OnEnable() {
		_tStart = Time.time;
		_danger = false;
		if (_lineRenderer) {
			_lineRenderer.widthMultiplier = 0;
			_lineRenderer.material = material;
		}
	}

	// Update is called once per frame
	void FixedUpdate () {
		GetComponent<BoxCollider2D>().enabled = _danger;
		List<Vector2> points = new List<Vector2>();
		points.Add(Vector2.zero);
		for (int i = 0; i < 3; i++) {
			float t = 0;
			while (t < length) {
				t += 0.05f;
				points.Add(Vector2.right * (i%2 == 0 ? t : length - t) + Vector2.up * Random.Range(-0.05f, 0.05f));
			}
		}

		_lineRenderer.positionCount = points.Count;
		_lineRenderer.SetPositions(Array.ConvertAll<Vector2, Vector3>(points.ToArray(), v => v));
	}

	void Update() {
		_lineRenderer.widthMultiplier = (_danger ? _width : _width * 0.5f) * (_danger ? 1f : Mathf.Min((Time.time - _tStart) / spinupTime, 1f));
		_lineRenderer.material = _danger ? materialHot : material;
	}

	public void SetHot() {
		_danger = true;
	}

	private void OnTriggerEnter2D(Collider2D other) {
		var health = other.gameObject.GetComponent<Health>();
		if (health) {
			health.Damage(damage);
		}
	}
}
