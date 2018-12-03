using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Gun : MonoBehaviour {
    public float hp = 100f;
    public float fireRate = 10f;
    public float shotSpeed = 10f;
    public GameObject shot;
    private float lastFire;

    // Use this for initialization
    void Start () {
    }

    // Update is called once per frame
    void Update () {
        if (CrossPlatformInputManager.GetButton("Fire1") && (Time.time - lastFire) > 1.0f/fireRate) {
            Fire();
            lastFire = Time.time;
        }
    }

    private void Fire() {
        var orientation = new Vector2(transform.right.x, transform.right.y);
        var s = Instantiate(shot);
        s.transform.position = transform.position + transform.right * 0.2f;
        s.GetComponent<Rigidbody2D>().velocity = orientation * shotSpeed;
    }

    public void Damage(float amount) {
        hp = Math.Max(0f, hp - amount);
        if (hp <= 0) {
            Die();
        }
    }

    void Die() {
        Destroy(gameObject);
    }
}
