using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	int score = 0;

	float addScoreTime = 0;
	public int nextScoreIncrement = 1;
	public int timeScoreValue = 1;

	int multiplier = 1;
	float multiplierDecreaseTime = 0;
	public float multiplierDecreaseTimeReset = 3;

	void Update () {

		addScoreTime += Time.deltaTime;
		multiplierDecreaseTime += Time.deltaTime;

		if (multiplierDecreaseTime >= multiplierDecreaseTimeReset) {
			DecreaseMultiplier();
			multiplierDecreaseTime = 0;
		}

		if (addScoreTime >= nextScoreIncrement) {
			AddScore(timeScoreValue * multiplier);
			nextScoreIncrement += 1;
		}
	}
	void AddScore(int a_scoreValue)
	{
		score += a_scoreValue;
	}
	public void IncreaseMultiplier()
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
	public void GameOver()
	{
		PlayerPrefs.SetInt("score", score);
		int highScore = PlayerPrefs.GetInt("highscore");
		if (score > highScore)
		{
			PlayerPrefs.SetInt("highscore", score);
		}
		PlayerPrefs.Save();
	}
}