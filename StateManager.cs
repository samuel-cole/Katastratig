using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StateManager : MonoBehaviour 
{
	float gameOverTimer;

	void Start()
	{
		gameObject.transform.position = Vector3.zero;

		if (Application.loadedLevelName == "ResultsScene") 
		{
			Transform hud = GameObject.FindGameObjectWithTag("Finish").transform;
			hud.FindChild("Text 3").GetComponent<Text>().text = PlayerPrefs.GetInt ("score").ToString();
			hud.FindChild("Text 4").GetComponent<Text>().text = PlayerPrefs.GetInt ("highscore").ToString();
		}
	}

	void Update()
	{
		if (Input.GetButtonDown("MenuExit"))
		{
			Application.Quit ();
			Debug.Log ("Quitter");
		}

		if (gameOverTimer > 0.0f)
		{
			gameOverTimer += Time.fixedDeltaTime;
		}
		if (gameOverTimer > 0.3f) 
		{
			Time.timeScale = 1.0f;
			Application.LoadLevel ("ResultsScene");
		}
	}

	public void Menu()
	{
		Application.LoadLevel ("StartScene");
	}


	public void StartGame()
	{
		Application.LoadLevel ("GameScene");
	}

	public void Credits()
	{
		Application.LoadLevel ("CreditsScene");
	}

	public void Results()
	{
		if (Application.loadedLevelName == "GameScene") 
		{
			GameObject.FindGameObjectWithTag ("GameController").GetComponent<ScoreManager> ().GameOver ();
			GameObject player = GameObject.FindGameObjectWithTag ("Player");
			Instantiate(player.GetComponent<Player>().bloodSplatter, player.transform.position, Quaternion.Euler(new Vector3(90, 0, 0)));	// Make a spray of blood on death
			Time.timeScale = 0.00f;
			gameOverTimer = 0.01f;
		}
		else 
		{
			Application.LoadLevel ("ResultsScene");
		}
	}
}
