using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour 
{
	public Transform objectToFollow;
	public float followSpeed;
	public bool lockXPosition = true;

	Transform trans;

	// Use this for initialization
	void Start () {
		trans = transform;
	}
	
	// Update is called once per frame
	void Update () 
	{
		Vector3 trackingPosition = objectToFollow.position;
		trackingPosition.z = trans.position.z;

		if(lockXPosition)
		{
			trackingPosition.x = trans.position.x;
		}

		trans.position = Vector3.Lerp(trans.position, trackingPosition, followSpeed);
	}
}
