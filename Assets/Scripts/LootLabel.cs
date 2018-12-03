using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LootLabel : MonoBehaviour {
	public LootHolder lootHolder;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		if (!lootHolder) {
			return;
		}
		Text text = GetComponent<Text>();
		text.text = GameState.Loot.ToString();
	}
}
