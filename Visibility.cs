// Locks the object to the floor/navMesh and makes it invisible - used on the AI Navigation cubes
// Created by Rowan Donaldson.

using UnityEngine;
using System.Collections;
[RequireComponent(typeof(MeshRenderer))]

public class Visibility : MonoBehaviour 	
{
	void Start()	
	{
		gameObject.transform.position = new Vector3(gameObject.transform.position.x, 1, gameObject.transform.position.z);
		gameObject.GetComponent<MeshRenderer>().enabled = false;
	}
}