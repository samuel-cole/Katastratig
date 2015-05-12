using UnityEngine;
using System.Collections;

public class StateManager : MonoBehaviour 
{
	void Start()
	{
		gameObject.transform.position = Vector3.zero;
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
		Application.LoadLevel ("ResultsScene");
	}
}
