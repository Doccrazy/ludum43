using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class LootHolder : MonoBehaviour {
    public GameObject droppedLoot;

    // Use this for initialization
    void Start () {
    }

    // Update is called once per frame
    void Update () {
        if (CrossPlatformInputManager.GetButtonDown("Jump")/* && amount > 0*/) {
            GameState.Loot--;
            var l = Instantiate(droppedLoot, transform.position - transform.right * 0.5f, Quaternion.identity);
            var rb = l.GetComponent<Rigidbody2D>();
            rb.angularVelocity = Random.Range(-25f, 25f);
            rb.velocity = GetComponent<Rigidbody2D>().velocity;
            rb.AddForce(-transform.right.normalized * 50f);
            Destroy(l, 3f);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Loot")) {
            GameState.Loot++;
            Destroy(other.gameObject);
        }
    }
}
