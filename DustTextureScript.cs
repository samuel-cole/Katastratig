using UnityEngine;
using System.Collections;
public class DustTextureScript : MonoBehaviour // This goes on the DustPrefab object
{	
	private float randomAngle;
	
	private float timeOut = 0.5f;
	
	void Start()
	{
		randomAngle = Random.Range(0, 360);
		
		gameObject.transform.rotation = Quaternion.Euler(90,randomAngle,0);
	}
	
	void Update()
	{
		timeOut -= Time.deltaTime;
		
		if (timeOut < 0)
		{
			Destroy(gameObject);
		}
	}
}
