using UnityEngine;
using System.Collections;

public class PlayerCollision : MonoBehaviour 
{
	public GameObject collisionFeedbackPrefab;
	
	void OnCollisionEnter2D(Collision2D other)
	{
		Vector3 contactPosition = other.contacts[0].point;
		GameObject collisionFeedbackGameObject = Instantiate(collisionFeedbackPrefab, contactPosition, Quaternion.identity) as GameObject;
		PlayerCollisionFeedback collisionFeedback = collisionFeedbackGameObject.GetComponent<PlayerCollisionFeedback>();
		if (collisionFeedback != null)
		{
			collisionFeedback.showFeedback();
		}
	}
	
//	void OnTriggerEnter2D(Collider2D other)
//	{
//		GameObject collisionFeedbackGameObject = Instantiate(collisionFeedbackPrefab, transform.position, Quaternion.identity) as GameObject;
//		PlayerCollisionFeedback collisionFeedback = collisionFeedbackGameObject.GetComponent<PlayerCollisionFeedback>();
//		if (collisionFeedback != null)
//		{
//			collisionFeedback.showFeedback();
//		}
//	}
}
