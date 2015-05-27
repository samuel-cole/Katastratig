using UnityEngine;
using System.Collections;

public class ArrowProxyScript : MonoBehaviour 
{
	private SpriteRenderer myRenderer;

	void Awake()
	{
		myRenderer = gameObject.GetComponent<SpriteRenderer>();
	}

	void Start () 
	{
		myRenderer.enabled = false;
	}
	
	public void Visible()
	{
		myRenderer.enabled = true;
	}
	
	public void Invisible()
	{
		myRenderer.enabled = false;
	}
}
