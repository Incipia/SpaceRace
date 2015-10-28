using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour 
{
	public Transform objectToFollow;
	public float yTrackingSpeed;
	public float xTrackingSpeed;
	public bool lockXPosition = false;
	public float minYPosition = 0;
	public float xLimit = 0.5f;

	Transform trans;
	float lookAheadDistance;

	// Use this for initialization
	void Start () 
	{
		trans = transform;

		// 1/6 of the camera height. We use this to center the player 1/3 of the way up the screen
		lookAheadDistance = Camera.main.orthographicSize / 3.0f;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (objectToFollow != null)
		{
			float newXPosition = trans.position.x;
			if(!lockXPosition)
			{
				newXPosition = Mathf.Lerp(trans.position.x, objectToFollow.position.x, xTrackingSpeed);
				newXPosition = Mathf.Clamp(newXPosition, -xLimit, xLimit);
			}
			float newYPosition = Mathf.Lerp(trans.position.y, objectToFollow.position.y + lookAheadDistance, yTrackingSpeed);

			newYPosition = Mathf.Max(minYPosition, newYPosition);
			trans.position = new Vector3(newXPosition, newYPosition, trans.position.z);
		}
	}
}
