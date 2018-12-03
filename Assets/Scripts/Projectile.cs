using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Projectile : MonoBehaviour {
    public float damage = 10f;
    public float lifeTime = 10f;
    public GameObject explosion;

    // Use this for initialization
    void Start () {
        Invoke("Expire", lifeTime);
    }

    // Update is called once per frame
    void Update () {
        transform.right = GetComponent<Rigidbody2D>().velocity;
    }

    protected void Expire() {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        var health = other.gameObject.GetComponent<Health>();
        if (health) {
            health.Damage(damage);
        }

        if (explosion) {
            Instantiate(explosion, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }
}
