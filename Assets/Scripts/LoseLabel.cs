using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoseLabel : MonoBehaviour {
	public Health health;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		Text text = GetComponent<Text>();
		text.enabled = !health || health.hp <= 0;
		if (!text.enabled) {
			if (GameState.Lives > 0) {
				text.text = "You died!\n\n" + GameState.Lives + " ships remaining.\nPress Return to restart level";
			}
			else {
				text.text = "You blew up your last ship!\n\nLoot crates salvaged: " + GameState.Loot +
				            "\nFinal score: " + GameState.GetTotalScore() + "\n\nPress Return to try again.";
			}
		}
	}
}
