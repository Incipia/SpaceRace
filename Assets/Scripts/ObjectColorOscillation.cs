using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class ObjectColorOscillation : MonoBehaviour
{
	[System.Serializable]
	public class OscillationColor
	{
		public OscillationColor(Color c) {
			color = c;
		}
		public Color color;
		public float waitDuration = 0;
	}

	public Color targetColor = new Color(1, 0, 0.8f, 0.6f);
	public float duration = 2;
	public float initialWaitDuration = 0;
	
	
	public bool loop = true;
	public bool shouldReverse;
	
	public List<OscillationColor> oscillationColors;
	
	private List<OscillationColor> currentOscillationColors;
	private int currentIndex = 0;

	IEnumerator Start()
	{
		// this is a bug -- we need to do this for the time being.
		if (oscillationColors.Count > 1)
		{
			Color firstColor = oscillationColors[0].color;
			OscillationColor oscColor = new OscillationColor(firstColor);
			oscillationColors.Add(oscColor);
		}

		currentOscillationColors = new List<OscillationColor>(oscillationColors);

		yield return new WaitForSeconds(initialWaitDuration);
		if (oscillationColors.Count == 0)
		{
			LeanTween.color(gameObject, targetColor, duration).setLoopPingPong();
		}
		else
		{
			advanceToNextColor();
		}
	}
	
	private void advanceToNextColor()
	{
		++currentIndex;
		if (currentOscillationColors.Count > currentIndex)
		{
			OscillationColor targetOscillationColor = currentOscillationColors[currentIndex];
			float waitDuration = currentOscillationColors[currentIndex-1].waitDuration;
			
			animateToColor(targetOscillationColor.color, waitDuration, advanceToNextColor);
			if (currentIndexIsAtLastObject() && loop)
			{
				if (shouldReverse)
				{
					currentOscillationColors.Reverse();
				}
				currentIndex = 0;
			}
		}
	}
	
	private void animateToColor(Color color, float waitDuration, Action completion)
	{
		LeanTween.
			color(gameObject, color, duration).
				setEase(LeanTweenType.easeInOutQuad).
				setOnComplete(completion).setDelay(waitDuration);
	}
	
	private bool currentIndexIsAtLastObject()
	{
		return currentIndex == currentOscillationColors.Count - 1;
	}
}
