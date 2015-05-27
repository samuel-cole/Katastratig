using UnityEngine;
using System.Collections;
[RequireComponent(typeof(SpriteRenderer))]
public class BloodScript : MonoBehaviour 		//Goes on the blood Prefab
{
	private SpriteRenderer myRenderer;
	public Sprite[] bloodSprites;
	private int random;
	private float randomAngle;
	//private float timeOut = 5.0f;
	//private Color materaialColour;
	private GameObject sceneManager;
	private ScoreManager score;
	
	void Start () 
	{
		gameObject.transform.position = new Vector3(gameObject.transform.position.x, 0.1f, gameObject.transform.position.z);
		randomAngle = Random.Range(0, 360);
		gameObject.transform.rotation = Quaternion.Euler(90, randomAngle, 0);
	
		myRenderer = gameObject.GetComponent<SpriteRenderer>();
		random = Random.Range(0, bloodSprites.Length);
	
		myRenderer.sprite = bloodSprites[random];
		
		
		sceneManager = GameObject.Find("SceneManager");
		
		if (sceneManager != null)
		{	
			score = sceneManager.GetComponent<ScoreManager>();
		}
		
		if (score != null)
		{
			score.DoubleScore();
		}
	}
}
