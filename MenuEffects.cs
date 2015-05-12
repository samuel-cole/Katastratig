using UnityEngine;
using System.Collections;

public class MenuEffects : MonoBehaviour 	// Makes the Enemy at the title menu rotate, for pretty effects!!
{
	
	void Update () 
	{
		gameObject.transform.Rotate(Vector3.down * Time.deltaTime* 60);
	}
}
