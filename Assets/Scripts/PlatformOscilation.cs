using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlatformOscilation : MonoBehaviour 
{
	public List<Vector3> positions;

	public void reset()
	{
		positions.Clear();
	}
	
	public void storeTransform()
	{
		positions.Add(transform.position);
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
				Gizmos.DrawSphere(previousPosition, .1f);
				previousPosition = position;
			}
       	}
   	}
}
