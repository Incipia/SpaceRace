using UnityEngine;

public class PlatformRotator : MonoBehaviour 
{	
	public float rotationSpeed = 0.5f;
	public bool pingpong = false;
	public float pingpongStartAngle = 0.0f;
	public float pingpongStopAngle = 90.0f;
	public float duration;

	private Vector3 rotation;
	private float direction;
	
	void Start() 
	{
		transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, pingpongStartAngle);
		direction = 1.0f;
		if(pingpong && pingpongStartAngle > pingpongStopAngle)
		{
			float swap = pingpongStartAngle;
			pingpongStartAngle = pingpongStopAngle;
			pingpongStopAngle = swap;
		}

		if (pingpong)
		{
			LeanTween.rotateLocal(gameObject, new Vector3(0, 0, pingpongStopAngle), duration).setLoopPingPong();
		}
	}

	void FixedUpdate() 
	{
		if(pingpong)
		{
			return;
			if(transform.eulerAngles.z > pingpongStopAngle || transform.eulerAngles.z < pingpongStartAngle)
			{
				direction *= -1.0f;
			}
		}

		rotation = transform.eulerAngles;
		rotation.z += rotationSpeed * direction;
		transform.eulerAngles = rotation;
	}

	void OnDrawGizmos()
	{
		if(pingpong)
		{
			Gizmos.color = Color.yellow;
			Vector3 minAngle = new Vector3(  Mathf.Cos(Mathf.Deg2Rad * pingpongStartAngle), Mathf.Sin(Mathf.Deg2Rad * pingpongStartAngle), 0);
			Vector3 maxAngle = new Vector3(  Mathf.Cos(Mathf.Deg2Rad * pingpongStopAngle), Mathf.Sin(Mathf.Deg2Rad * pingpongStopAngle), 0);
			Gizmos.DrawSphere(transform.position, 0.25f);
			Gizmos.DrawLine(transform.position, transform.position + minAngle);
			Gizmos.DrawLine(transform.position, transform.position + maxAngle);
		}
	}
}
