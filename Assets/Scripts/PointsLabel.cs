using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointsLabel : MonoBehaviour {
	public Text floatingLabelPlus;
	public Text floatingLabelMinus;
	private int _lastScore;

	// Use this for initialization
	void Start () {
		_lastScore = GameState.GetTotalScore();
	}

	// Update is called once per frame
	void Update () {
		Text text = GetComponent<Text>();
		text.text = GameState.GetTotalScore().ToString();
		var diff = GameState.GetTotalScore() - _lastScore;
		if (diff != 0) {
			var l = Instantiate(diff > 0 ? floatingLabelPlus : floatingLabelMinus, transform);
			l.text = (diff > 0 ? "+" : "") + diff;
			l.transform.localPosition = Vector3.zero;
		}
		_lastScore = GameState.GetTotalScore();
	}
}
