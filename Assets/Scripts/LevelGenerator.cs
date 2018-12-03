using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using utils;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelGenerator : MonoBehaviour {
	public bool invert;
	public float length = 50f;
	public float height = 4f;
	public float towerProb = 0.05f;
	public GameObject[] towers;
	public int lootAmount;
	public GameObject loot;
	public GameObject winZone;
	private float _length;
	private float _towerProb;

	// Use this for initialization
	void Start () {
		_length = length * (1 + (GameState.Level-1)/3f);
		_towerProb = Math.Min(towerProb * (1 + (GameState.Level-1)/3f), 0.2f);

		CreateMesh();
		PlaceTowers();
		StartCoroutine(PlaceLoot());
		if (winZone) {
			winZone.transform.position = new Vector2(transform.position.x + _length + 3f, 0);
		}
	}

	private void CreateMesh() {
		// Create Vector2 vertices
		List<Vector2> controls = new List<Vector2>();
		controls.Add(new Vector2(0, invert ? 5f : -5f));
		float x = 0;
		float y = invert ? -1f : 1f;
		bool wall = false;
		while (x < _length) {
			float dx;
			if (wall) {
				dx = Random.Range(0.2f, 1f);
				wall = false;
			}
			else {
				dx = Random.value > 0.5f ? Random.Range(0.2f, 1f) : 0;
				wall = dx == 0;
			}
			float dy;
			do {
				dy = (wall ? Random.Range(-1f, 1f) : (Random.Range(-1, 2)) * dx);
			} while (invert ? (y + dy > -0.5f || y + dy < -height) : (y + dy < 0.5f || y + dy > height));

			x += dx;
			y += dy;
			controls.Add(new Vector2(x, y));
		}
		controls.Add(new Vector2(x, invert ? 5f : -5f));

		var vertices2D = controls.ToArray();
		var vertices3D = System.Array.ConvertAll<Vector2, Vector3>(vertices2D, v => v);

		// Use the triangulator to get indices for creating triangles
		var triangulator = new Triangulator(vertices2D);
		var indices = triangulator.Triangulate();
//		if (!invert) {
//			Array.Reverse(indices);
//		}

		// Generate a color for each vertex
		var colors = Enumerable.Range(0, vertices3D.Length)
			.Select(i => Random.ColorHSV())
			.ToArray();

		// Create the mesh
		var mesh = new Mesh {
			vertices = vertices3D,
			triangles = indices,
			colors = colors,
			uv = vertices2D
		};

		mesh.RecalculateNormals();
		mesh.RecalculateBounds();

		GetComponent<MeshFilter>().mesh = mesh;
		GetComponent<PolygonCollider2D>().points = vertices2D;
	}

	private void PlaceTowers() {
		float x = 7f;
		while (x < _length) {
			x += 0.5f;
			if (Random.value < _towerProb * (x/_length) * 2f) {
				var dir = invert ? Vector2.up : Vector2.down;
				var hit = Physics2D.Raycast(
					new Vector2(transform.position.x + x, transform.position.y + (invert ? -height - 1f : height + 1f)),
					dir);
				CreateTower(hit.point, hit.normal);
			}
		}
	}

	private IEnumerator PlaceLoot() {
		yield return new WaitForSeconds(0.1f);
		for (int i = 0; i < lootAmount; i++) {
			Vector2 pos;
			do {
				pos = (Vector2)transform.position + new Vector2(Random.Range(3f, _length), Random.Range(0f, (invert ? -1f : 1f)*height*2f));
			} while (Physics2D.OverlapCircle(pos, 0.5f));

			Instantiate(loot, pos, Quaternion.identity);
		}
	}

	private void CreateTower(Vector2 location, Vector2 normal) {
		var tower = Instantiate(towers[Random.Range(0, towers.Length)], transform);
		tower.transform.position = location;
		tower.transform.rotation = Quaternion.AngleAxis(Vector2.SignedAngle(Vector2.right, normal), Vector3.forward);
	}

	// Update is called once per frame
	void Update () {

	}
}
