using UnityEngine;
using System.Collections;
[RequireComponent(typeof(LineRenderer))]			// Remember to set all AIpoints to "Ignore Raycast" layer
public class ArcherRaycast : MonoBehaviour 
{
	private LineRenderer line;
	
	private RaycastHit hit;
	
	private bool timerDown;
	private float timer;
	public float Timer
	{
		get { return timer; }
	}
	
	private EnemyAudio enemyAudio;
	private GameObject parent;
	private AnimationController anim;
	private Color materialColor;
	
	
	//Rowan's Addition(component references)
	public GameObject arrowPrefab;
	public GameObject arrowPoint;
	private bool gotArrow = false;
	public ArrowProxyScript proxy;
	// Sam, Delete this if it's causing problems
	
	//Rowan added reference script
	public GameObject bloodPrefab;
	//End

	void Start () 
	{	
		parent = gameObject.transform.parent.gameObject;
		enemyAudio = parent.GetComponent<EnemyAudio>();
		anim = parent.GetComponentInChildren<AnimationController>();
		
		line = gameObject.GetComponent<LineRenderer>();
		materialColor = line.material.color;
		materialColor.a = 0.0f;
		
		line.useWorldSpace = true;
		line.SetWidth(0.1f, 10.0f);
		
		
		//Animation Component _ delete if causing problems
		if (proxy != null)
		{
			proxy.Invisible();
		}
		// end
	}
	
	
	void Update () 
	{
		//Color materialColor = line.material.color;
		if (timerDown)
		{
		
			if (timer > 0)
			{
				timer -= Time.deltaTime;
				line.SetWidth(0.1f, timer*10);
				anim.Action();			// start attack animation
			}
			else
			{
				line.SetWidth(0.1f, 0.1f);
				enemyAudio.Ranged(); // make a noise!!
				Kill ();
			}
			
			materialColor.a += 0.02f;		// make the line render
		}
		else
		{
			timer = 1.0f;
			line.SetWidth(0.1f, 5.0f);
			materialColor.a = 0.0f;			// stop the line render
		}
			
		//materialColor.a = timer;
		
		if (materialColor.a < 0)		// lock the line render
		{
			materialColor.a = 0;
		}
		
		if (materialColor.a > 1.0f)
		{
			materialColor.a = 1.0f;
		}
			
		line.material.color = materialColor;
		
		///////////////////////////////////////////////////////////////
	
		Ray	ray = new Ray(gameObject.transform.position, gameObject.transform.parent.FindChild ("EnemyAnimation").TransformDirection(Vector3.up));
		
		if (Physics.Raycast(ray,out hit, 20))
		{
			//________________________________________DeleteIfCausingTrouble
			if (proxy != null)
			{
				proxy.Visible();
			}
			//______________________________________________END
			
			//if (hit.collider.gameObject.CompareTag("Player"))
			{
				timerDown = true;
			}
		}
		else if (timer < 0.5f && timer > 0.0f)
		{
			//________________________________________DeleteIfCausingTrouble
			if (proxy != null)
			{
				proxy.Visible();
			}
			//______________________________________________END
			
			timerDown = true;
		}
		else
		{
			//____________________________________________Delete if causing Trouble
			if (proxy != null)
			{
				proxy.Invisible();
			}
			gotArrow = false;
			//__________________________________________END
		
			timerDown = false;
		}
		
		line.SetPosition (0, gameObject.transform.position);
	
		if (hit.collider != null)
		{
			line.SetPosition (1, ray.GetPoint(20));
		}
		else
		{
			line.SetPosition (1, ray.GetPoint(20));
		}
	}
	
	void Kill()
	{
		/// Spawn An arrow_______________________________________delete this if it's causing trouble
		if (!gotArrow)
		{
			//Instantiate(arrowPrefab, gameObject.transform.position, Quaternion.Euler(90, gameObject.transform.rotation.eulerAngles.y, gameObject.transform.rotation.eulerAngles.z));
			Instantiate(arrowPrefab, arrowPoint.transform.position, Quaternion.Euler(90, arrowPoint.transform.rotation.eulerAngles.y, arrowPoint.transform.rotation.eulerAngles.z));  // SOMEHOW!!!!! SOMEHOW! SOME-HOW, this seems to work! I don't even know. It just looks 'right'. Bloody animations. This spawns the arrow object FAR infront of the actual ranged enemy object, but it looks appropriate. 
			gotArrow = true;
		}
		////_______________________________________________
		
		
		
		if (hit.collider != null)
		{
			if (hit.collider.gameObject.CompareTag("Player"))
			{		
				if (hit.collider.gameObject.GetComponent<Player>().RollTimeCurrent < 0.0f)
				{
					GameObject.FindGameObjectWithTag("GameController").GetComponent<StateManager>().Results();
				}
			}
			else
			{			
				GameObject.Destroy(hit.collider.gameObject);
				// Rowan's addition - Spawn a bloodPrefab
				Instantiate(bloodPrefab, hit.collider.gameObject.transform.position, Quaternion.Euler(90, hit.collider.gameObject.transform.rotation.eulerAngles.y, hit.collider.gameObject.transform.rotation.eulerAngles.z));
			}
		}
		timerDown = false;
		//timer = 1.0f;
		anim.StopAction();	// stop the attack animation
	}
}
