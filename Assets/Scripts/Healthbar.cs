using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class Healthbar : MonoBehaviour {
    public Health destructible;

    // Use this for initialization
    void Start () {

    }

    // Update is called once per frame
    void Update () {
        Slider healthBarSlider = GetComponent<Slider>();
        healthBarSlider.value = destructible.hp / destructible.initialHealth;
    }
}
