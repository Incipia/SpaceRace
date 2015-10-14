﻿using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class PlatformOscillation : MonoBehaviour 
{
	[System.Serializable]
	public class OscillationPoint
	{
		public OscillationPoint(Vector3 pos) {
			position = pos;
		}
		public Vector3 position;
		public float waitDuration = 0;
	}

	public bool loop = true;
	public bool shouldReverse;
	public float movementDuration = 1.0f;
	public List<OscillationPoint> oscillationPoints;

	private List<OscillationPoint> currentOscillationPoints;
	private List<Vector3> worldPoints = new List<Vector3>();
	private int currentIndex = 0;

	void Start()
	{
		currentOscillationPoints = new List<OscillationPoint>(oscillationPoints);

		setObjectToFirstPoint();
		advanceToNextPosition();
	}

	private void advanceToNextPosition()
	{
		++currentIndex;
		if (currentOscillationPoints.Count > currentIndex)
		{
			OscillationPoint targetOscillationPos = currentOscillationPoints[currentIndex];
			float waitDuration = currentOscillationPoints[currentIndex-1].waitDuration;

			animateToPosition(targetOscillationPos.position, waitDuration, advanceToNextPosition);
			if (currentIndexIsAtLastObject() && loop)
			{
				if (shouldReverse)
				{
					currentOscillationPoints.Reverse();
				}
				currentIndex = 0;
			}
		}
	}

	private void animateToPosition(Vector3 position, float waitDuration, Action completion)
	{
		LeanTween.
			moveLocal(gameObject, position, movementDuration).
			setEase(LeanTweenType.easeInOutQuad).
				setOnComplete(completion).setDelay(waitDuration);
	}

	private bool currentIndexIsAtLastObject()
	{
		return currentIndex == currentOscillationPoints.Count - 1;
	}

	public void reset()
	{
		oscillationPoints.Clear();
		worldPoints.Clear();
	}
	
	public void storeTransform()
	{
		OscillationPoint oscPoint = new OscillationPoint(transform.localPosition);
		oscillationPoints.Add(oscPoint);

		worldPoints.Add(transform.position);
	}

	public void closePath()
	{
		if (oscillationPoints.Count > 1)
		{
			Vector3 firstPosition = oscillationPoints[0].position;
			OscillationPoint oscPoint = new OscillationPoint(firstPosition);
			oscillationPoints.Add(oscPoint);

			worldPoints.Add(worldPoints[0]);
		}
	}

	public void setObjectToFirstPoint()
	{
		if (oscillationPoints.Count >= 1)
		{
			transform.localPosition = oscillationPoints[0].position;
		}
	}
	
//	void OnDrawGizmos() 
//	{
//		Gizmos.color = Color.yellow;
//		
//		if (worldPoints.Count > 1)
//		{
//			Vector3 previousPosition = worldPoints[0];
//			foreach (Vector3 position in worldPoints)
//			{
//				Gizmos.DrawLine(previousPosition, position);
//				Gizmos.DrawSphere(position, 0.5f);
//				previousPosition = position;
//			}
//       	}
//   	}
}
