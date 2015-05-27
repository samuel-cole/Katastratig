using UnityEngine;
using System.Collections;

public class CameraRotateScript : MonoBehaviour // For Credits Scene
{
	void Update () 
	{
		gameObject.transform.Rotate(Vector3.forward * Time.deltaTime * 8.0f);
	}
}
