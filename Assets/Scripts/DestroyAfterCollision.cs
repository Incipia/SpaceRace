using UnityEngine;
using System.Collections;

public class DestroyAfterCollision : MonoBehaviour 
{
	public ParticleSystem feedbackParticleSystem;
	public Collider2D objectCollider;
	public SpriteRenderer spriteRenderer;
	
	public float destroyDelay = 0.1f;
	
	private bool _shouldDestroy;
	private Vector3 _pointOfContact;
	
//	void Update()
//	{
//		if (_shouldDestroy)
//		{
//			_shouldDestroy = false;
//			StartCoroutine(triggerFeedbackAndDestroyObjectAfterDuration(destroyDelay));
//		}
//	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if (!_shouldDestroy)
		{
			_shouldDestroy = true;
			_pointOfContact = other.contacts[0].point;
			
			
			
			StartCoroutine(triggerFeedbackAndDestroyObjectAfterDuration(destroyDelay));
		}
	}
	
	IEnumerator triggerFeedbackAndDestroyObjectAfterDuration(float duration)
	{
		yield return new WaitForSeconds(duration);
		triggerFeedbackAndHideObject();
	}
	
	void triggerFeedbackAndHideObject()
	{
		spriteRenderer.enabled = false;
		objectCollider.isTrigger = true;
		
		feedbackParticleSystem.transform.position = _pointOfContact;
		feedbackParticleSystem.Play();
		
		Destroy(gameObject, feedbackParticleSystem.duration);
	}
}
