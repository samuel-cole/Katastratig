using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour 
{
	private Transform myTransform;
	private Vector3 startPos;
	
	private float shake = 0.0f;
	public float shakeAmount = 0.5f;

	// Use this for initialization
	void Start () 
	{
		myTransform = gameObject.GetComponent<Transform>() as Transform;
	}
	
	// Update is called once per frame
	void Update () 
	{
		startPos = myTransform.localPosition;
		
		shake -= Time.deltaTime;
		
		if (shake < 0)
		{
			shake = 0;
		}
		
		if (shake > 0)
		{
			myTransform.localPosition = startPos + Random.insideUnitSphere * shakeAmount;	
		}
		else
		{
			myTransform.localScale = startPos;
		}
	}
	
	public void CamShake()
	{
		shake = 0.4f;
	}
}
