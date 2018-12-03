﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LivesLabel : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void FixedUpdate () {
		Text text = GetComponent<Text>();
		text.text = GameState.Lives.ToString();
		if (GameState.Lives == 0) {
			text.color = Color.red;
		}
	}
}
