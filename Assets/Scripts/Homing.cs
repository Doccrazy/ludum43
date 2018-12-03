using System;
using System.Collections;
using System.Collections.Generic;
using utils;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Homing : MonoBehaviour {
    public float accelForce = 10f;
    public float steerForce = 10f;
    private Rigidbody2D rb;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate() {
        rb.AddRelativeForce(Vector2.right * accelForce);
        var target = TargetFinder.FindTarget(transform);
        if (target != null) {
            Vector2 to = ((Vector2)target.transform.position - (Vector2)transform.position).normalized;
            //Debug.Log(Vector3.Angle(to, rb.velocity));
            if (Vector2.Angle(to, rb.velocity) > 5) {
                rb.AddForce(Vector3.Project(to, Vector2.Perpendicular(rb.velocity)).normalized * steerForce);
            }
        }
    }

}
