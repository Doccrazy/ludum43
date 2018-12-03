using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Starfield : MonoBehaviour {
	public Camera mainCamera;
	public Rigidbody2D player;

	private ParticleSystem _particleSystem;
	private ParticleSystem.Particle[] _particles;
	private Rect _bounds;

	// Use this for initialization
	void Start () {
		_particleSystem = GetComponent<ParticleSystem>();

		_particles = new ParticleSystem.Particle[_particleSystem.main.maxParticles];
		var min = mainCamera.ScreenToWorldPoint(new Vector3(-50, -50));
		var max = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width + 50, Screen.height + 50));
		_bounds = new Rect(min, max - min);

		for (int i = 0; i < _particleSystem.main.maxParticles; i++) {
			_particles[i].position = new Vector3(Random.Range(min.x, max.x), Random.Range(min.y, max.y), Random.Range(2.0f, 5.0f));
			_particles[i].startSize = Random.Range(0.05f, 0.25f);
			_particles[i].startColor = new Color(1, Random.Range(0.5f, 1f), Random.Range(0.5f, 1f), 1);
		}

		_particleSystem.SetParticles(_particles, _particles.Length);
	}

	// Update is called once per frame
	void Update () {
		if (!player) {
			return;
		}
		for (var i = 0; i < _particles.Length; i++) {
			_particles[i].velocity = -player.velocity / _particles[i].position.z;
			_particles[i].position =
				new Vector3(clamp(_particles[i].position.x, _bounds.x, _bounds.width),
					clamp(_particles[i].position.y, _bounds.y, _bounds.height), _particles[i].position.z) + _particles[i].velocity * Time.deltaTime;
		}
		_particleSystem.SetParticles(_particles, _particles.Length);
	}

	private float clamp(float v, float min, float size) {
		if (v < min) {
			v += size;
		} else if (v > min + size) {
			v -= size;
		}

		return v;
	}
}
