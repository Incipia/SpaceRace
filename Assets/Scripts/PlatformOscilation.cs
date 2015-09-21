using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlatformOscilation : MonoBehaviour 
{
	public float loopTime = 1.0f;
	public List<Vector3> positions;

	void Start()
	{
		setObjectToFirstPoint();
		LeanTween.moveSpline(gameObject, positions.ToArray(), loopTime).setEase(LeanTweenType.easeInOutCirc);
	}

	public void reset()
	{
		positions.Clear();
	}
	
	public void storeTransform()
	{
		positions.Add(transform.position);
	}

	public void closePath()
	{
		if(positions.Count > 1)
		{
			positions.Add(positions[0]);
		}
	}

	public void setObjectToFirstPoint()
	{
		if(positions.Count >= 1)
		{
			transform.position = positions[0];
		}
	}
	
	void OnDrawGizmos() 
	{
		Gizmos.color = Color.yellow;
		
		if (positions.Count >= 1)
		{
			Vector3 previousPosition = positions[0];
			foreach (Vector3 position in positions)
			{
				Gizmos.DrawLine(previousPosition, position);
				Gizmos.DrawSphere(position, 0.5f);
				previousPosition = position;
			}
       	}
   	}
}
