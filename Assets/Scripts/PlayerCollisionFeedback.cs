using UnityEngine;
using System.Collections;

public class PlayerCollisionFeedback : MonoBehaviour 
{
	public ParticleSystem _particleSystem;
	
	public void showFeedback()
	{
		if (_particleSystem != null)
		{
			_particleSystem.Play();
			Destroy(gameObject, _particleSystem.duration);
		}
	}
}
