using UnityEngine;
using System.Collections;
[RequireComponent(typeof(ParticleSystem))]
public class EmberParticleEmit : MonoBehaviour // On the Ember Particle script - an Experiment - not necessary
{
	private ParticleSystem myParticles;
	private	int random;
	private float timer;
	
	void Start()
	{
		myParticles = gameObject.GetComponent<ParticleSystem>();
		random = Random.Range(1, 7);
		timer = random;
	}
	
	void Update()
	{
		timer -= Time.deltaTime;
		
		random = Random.Range(2, 7);
		
		if (timer < 0)
		{
			myParticles.Emit (1);
			timer = random;
		}
	}
}
