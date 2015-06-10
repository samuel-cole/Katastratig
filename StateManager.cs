// Allows changes between game scenes. Should be present in every scene.
// Created by Samuel Cole and Rowan Donaldson.

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StateManager : MonoBehaviour 	
{
	//Timer that counts upwards after the game ends- ends the game at 0.3f.
	float gameOverTimer;

	void Start()
	{
		gameObject.transform.position = Vector3.zero;

		if (Application.loadedLevelName == "ResultsScene") 
		{
			Transform hud = GameObject.FindGameObjectWithTag("Finish").transform;
			hud.FindChild("Text 3").GetComponent<Text>().text = PlayerPrefs.GetInt ("score").ToString();
			hud.FindChild("Text 4").GetComponent<Text>().text = PlayerPrefs.GetInt ("highscore").ToString();
			hud.FindChild ("Text 5").GetComponent<Text>().text = PlayerPrefs.GetFloat ("time").ToString("F2");
		}

		//Locks the cursor.
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
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
	
	public void Intro()
	{
		Application.LoadLevel ("Intro");
	}

	public void Results()
	{
		if (Application.loadedLevelName == "GameScene") 
		{
			if(gameOverTimer < 0.01f)
			{
				GameObject.FindGameObjectWithTag ("GameController").GetComponent<ScoreManager> ().GameOver ();
				GameObject player = GameObject.FindGameObjectWithTag ("Player");
				player.GetComponent<Player>().alive = false;
				// Make a spray of blood and a dead body on death.
				Instantiate(player.GetComponent<Player>().playerBodyPrefab, player.transform.position, Quaternion.Euler(new Vector3(90, 0, 0)));
				Instantiate(player.GetComponent<Player>().bloodSplatter, player.transform.position, Quaternion.Euler(new Vector3(90, 0, 0)));	
				Time.timeScale = 0.00f;
				gameOverTimer = 0.01f;
			}
		}
		else 
		{
			Application.LoadLevel ("ResultsScene");
		}
	}
}
