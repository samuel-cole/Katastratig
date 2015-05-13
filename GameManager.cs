using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	int score;

	float addScoreTime;
	int nextScoreIncrement;
	int timeScoreValue;

	int multiplier;
	float multiplierDecreaseTime;
	float multiplierDecreaseTimeReset;

	void Start () {
		score = 0;

		addScoreTime = 0;
		nextScoreIncrement = 1;
		timeScoreValue = 1;

		multiplier = 1;
		multiplierDecreaseTime = 0;
		multiplierDecreaseTimeReset = 3;
	}
	void Update () {

		addScoreTime += Time.deltaTime;
		multiplierDecreaseTime += Time.deltaTime;

		if (multiplierDecreaseTime >= multiplierDecreaseTimeReset) {
			DecreaseMultiplier();
			multiplierDecreaseTime = 0;
		}

		if (addScoreTime >= nextScoreIncrement) {
			AddScore((int)(timeScoreValue * multiplier));
			nextScoreIncrement += 1;
		}
	}
	void AddScore(int a_scoreValue)
	{
		score += scoreValue;
	}
	void IncreaseMultiplier()
	{
		multiplier += 1;
	}
	void DecreaseMultiplier()
	{
		if (multiplier == 1) {
			return;
		}
		multiplier -= 1;
	}
}