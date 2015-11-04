using UnityEngine;
using System.Collections;

public class BounceEmitter : MonoBehaviour 
{
	public GameObject particleEmitterPrefab;
	private ParticleSystem ps;
	private Vector3 particleDirectionVector;
	private bool shouldUpdateParticleVelocity = true;
	
//	void Update()
//	{
//		if (ps != null && ps.particleCount != 0 && shouldUpdateParticleVelocity)
//		{	 
//			shouldUpdateParticleVelocity = false;
//			ParticleSystem.Particle[] particles = new ParticleSystem.Particle[ps.particleCount];
//			ps.GetParticles(particles);
//			
//			for (int particleIndex = 0; particleIndex < particles.Length; ++particleIndex)
//			{
//				particles[particleIndex].velocity = particleDirectionVector;
//			}
//		}
//	}
	
	void OnCollisionEnter2D(Collision2D collision) 
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			
			
			Vector3 contactPosition = collision.contacts[0].point;
			
			
			//			Vector3 collisionAngle = particleDirectionVector;
			particleDirectionVector = contactPosition - collision.gameObject.transform.position;
			particleDirectionVector.Normalize();
			//			particleDirectionVector *= 100;
			print(contactPosition);
			//			Vector3.Angle (collisionAngle);
		
			GameObject particleEmitter = Instantiate(particleEmitterPrefab, contactPosition, Quaternion.identity) as GameObject;
			particleEmitter.transform.SetParent(transform);
			
//			print("First point that collided: " + collision.contacts[0].point);
			
//			
//			ps = particleEmitter.GetComponent<ParticleSystem>();
		}
	}
}
