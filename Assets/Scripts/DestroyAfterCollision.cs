using UnityEngine;
using System.Collections;

public class DestroyAfterCollision : MonoBehaviour 
{
	public GameObject breakCollisionFeedbackPrefab;

	void OnCollisionExit2D(Collision2D other)
	{
		Vector3 contactPosition = other.contacts[0].point;
		GameObject breakCollisionFeedbackGameObject = Instantiate(breakCollisionFeedbackPrefab, contactPosition, Quaternion.identity) as GameObject;
//		BreakCollisionFeedback collisionFeedback = breakCollisionFeedbackGameObject.GetComponent<BreakCollisionFeedback>();
//		if (collisionFeedback != null)
//		{
//			collisionFeedback.showBreakFeedback();
//			
//					Debug.Log ("Feedback");
//		}
		Destroy(gameObject);
//		Debug.Log ("Destroyed");
	}
}
