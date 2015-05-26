using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StateManager : MonoBehaviour 
{
	void Start()
	{
		gameObject.transform.position = Vector3.zero;

		if (Application.loadedLevelName == "ResultsScene")
		{
			Transform hud = GameObject.FindGameObjectWithTag("Finish").transform;
			hud.FindChild("Text 3").GetComponent<Text>().text = PlayerPrefs.GetInt("score").ToString();
			hud.FindChild("Text 4").GetComponent<Text>().text = PlayerPrefs.GetInt("highscore").ToString();
		}
	}

	void Update()
	{
		if (Application.loadedLevelName == "GameScene")
		{
			if (Input.GetKeyDown(KeyCode.Alpha7))
			{
				//Debug.Log ("Go_To_Results_Screen");
				Results();
			}
		}
		
		if (Input.GetButtonDown("MenuExit"))
		{
			Application.Quit ();
			Debug.Log ("Quitter");
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
			GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().GameOver();
		}
		Application.LoadLevel ("ResultsScene");
	}
}
