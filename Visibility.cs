using UnityEngine;
using System.Collections;
[RequireComponent(typeof(MeshRenderer))]
public class Visibility : MonoBehaviour 
{
	void Start()	// Locks the object to the floor/navMesh and makes it invisible
	{
		gameObject.transform.position = new Vector3(gameObject.transform.position.x, 1, gameObject.transform.position.z);
		gameObject.GetComponent<MeshRenderer>().enabled = false;
	}
}
// Hi sam