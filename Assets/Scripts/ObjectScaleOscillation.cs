using UnityEngine;
using System;
using System.Collections;

public class ObjectScaleOscillation : MonoBehaviour 
{
	public Vector2 targetScale = new Vector2(1, 1);
	public float scaleDuration = 2;

	private void scaleWithDuration(float duration, Action completion)
	{
		LeanTween.scale(gameObject, targetScale, scaleDuration).setLoopPingPong();
	}
}
