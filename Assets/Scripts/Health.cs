using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Health : MonoBehaviour {
    public float hp = 100f;
    public float initialHealth;
    public GameObject explosion;
    public bool invulnerable;
    public int points;
    public AudioSource hitSound;

    // Use this for initialization
    void Start () {
        initialHealth = hp;
    }

    // Update is called once per frame
    void Update () {
    }

    public void Damage(float amount) {
        if (invulnerable) {
            return;
        }

        if (hitSound) {
            hitSound.Play();
        }
        hp = Math.Max(0f, hp - amount);
        if (hp <= 0) {
            Die();
        }
    }

    void Die() {
        GameState.AddScore(points);
        Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
