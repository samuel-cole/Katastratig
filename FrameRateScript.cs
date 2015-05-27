using UnityEngine;
using System.Collections;

public class FrameRateScript : MonoBehaviour
{

	void Start()
	{
		Debug.Log (Application.targetFrameRate);
	}
}
