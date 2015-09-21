using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class PlatformOscillation : MonoBehaviour 
{
	public bool loop = true;
	public bool shouldReverse;
	public float movementDuration = 1.0f;
	public List<Vector3> positions;

	private List<Vector3> currentPositions;
	private int currentIndex = 0;

	void Start()
	{
		currentPositions = new List<Vector3>(positions);

		setObjectToFirstPoint();
		advanceToNextPosition();
	}

	private void advanceToNextPosition()
	{
		++currentIndex;
		if (currentPositions.Count > currentIndex)
		{
			Vector3 targetPosition = currentPositions[currentIndex];
			animateToPosition(targetPosition, advanceToNextPosition);

			if (currentIndexIsAtLastObject() && loop)
			{
				if (shouldReverse)
				{
					currentPositions.Reverse();
					currentIndex = 0;
				}
				else
				{
					currentIndex = -1;
				}
			}
		}
	}

	private void animateToPosition(Vector3 position, Action completion)
	{
		LeanTween.move(gameObject, position, movementDuration).setEase(LeanTweenType.easeOutQuad).setOnComplete(completion);
	}

	private bool currentIndexIsAtLastObject()
	{
		return currentIndex == currentPositions.Count - 1;
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
		if (positions.Count > 1)
		{
			positions.Add(positions[0]);
		}
	}

	public void setObjectToFirstPoint()
	{
		if (positions.Count >= 1)
		{
			transform.position = positions[0];
		}
	}
	
	void OnDrawGizmos() 
	{
		Gizmos.color = Color.yellow;
		
		if (positions.Count > 1)
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
