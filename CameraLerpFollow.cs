using UnityEngine;
using System.Collections;

public class CameraLerpFollow : MonoBehaviour 
{
	public GameObject camTarget;
	
	void FixedUpdate()
	{
		//gameObject.transform.rotation = Quaternion.Slerp(gameObject.transform.rotation, camTarget.transform.rotation, Time.fixedDeltaTime * 2);
		gameObject.transform.rotation = Quaternion.LookRotation(Vector3.down);
		gameObject.transform.position = Vector3.Lerp (gameObject.transform.position, camTarget.transform.position, Time.fixedDeltaTime * 0.5f);
	}
}
