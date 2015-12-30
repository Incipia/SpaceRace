using UnityEngine;
using System.Collections;

public class PlayerCollision : MonoBehaviour
{
	public GameObject collisionFeedbackPrefab;
	public bool useOnCollisionExit;
	public bool shouldDestroyGameObject;

	void OnCollisionEnter2D(Collision2D other)
	{
		if (!useOnCollisionExit)
		{
			Vector3 contactPosition = other.contacts[0].point;
			GameObject collisionFeedbackGameObject = Instantiate(collisionFeedbackPrefab, contactPosition, Quaternion.identity) as GameObject;
			PlayerCollisionFeedback collisionFeedback = collisionFeedbackGameObject.GetComponent<PlayerCollisionFeedback>();
			if (collisionFeedback != null)
			{
				collisionFeedback.showFeedback();
			}
			
			if (shouldDestroyGameObject)
			{
				Destroy(gameObject);
			}
		}
	}
	
	void OnCollisionExit2D(Collision2D other)
	{
		if (useOnCollisionExit)
		{
			Vector3 contactPosition = other.contacts[0].point;
			GameObject collisionFeedbackGameObject = Instantiate(collisionFeedbackPrefab, contactPosition, Quaternion.identity) as GameObject;
			PlayerCollisionFeedback collisionFeedback = collisionFeedbackGameObject.GetComponent<PlayerCollisionFeedback>();
			if (collisionFeedback != null)
			{
				collisionFeedback.showFeedback();
			}
			
			if (shouldDestroyGameObject)
			{
				Destroy(gameObject);
			}
		}
	}
}
