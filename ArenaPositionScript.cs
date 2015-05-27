using UnityEngine;
using System.Collections;

public class ArenaPositionScript : MonoBehaviour 	// This just sets the Arena Position - Kinda Messy - Based of a stupid, stupid, STUPID Parent Object
{
	void Start () 
	{
		gameObject.transform.position = new Vector3 (4.134996F, -60.42977F, 34.53922F);
	}

}
