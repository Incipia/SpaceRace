using UnityEngine;
using System.Collections;

public class CounterUI : MonoBehaviour
{
	public Sprite zero;
	public Sprite one;
	public Sprite two;
	public Sprite three;
	public Sprite four;
	public Sprite five;
	public Sprite six;
	public Sprite seven;
	public Sprite eight;
	public Sprite nine;

	public GameObject onesPlace;
	public GameObject tensPlace;
	public GameObject hundredsPlace;
	public GameObject thousandsPlace;

	private SpriteRenderer onesPlaceRenderer;
	private SpriteRenderer tensPlaceRenderer;
	private SpriteRenderer hundredsPlaceRenderer;
	private SpriteRenderer thousandsPlaceRenderer;
	
	public void reset()
	{
		disableRenderers();
	}

	public void updateSpritesWithNumber(int number)
	{
		int numberOfDigits = numberOfDigitsInNumber(number);
		for (int i = 0; i < numberOfDigits; ++i)
		{
			SpriteRenderer sr = rendererForIndex(i);
			int spriteNumber = digitForIndexInNumber(i, number);
			sr.sprite = spriteForNumber(spriteNumber);
			sr.enabled = true;
		}
	}

	// index 0 is the least significant place
	private int digitForIndexInNumber(int index, int number)
	{
		// ex: 4321

		// 4321 % 10 / 1 = 1
		// 4321 % 100 / 10 = 2
		// 4321 % 1000 / 100  = 3
		// 4321 % 10000 / 1000 = 4

		float modValue = Mathf.Pow(10f, (float)index+1);
		float divValue = Mathf.Pow(10f, (float)index);

		return (int)(number % modValue / divValue);
	}

	private void disableRenderers()
	{
		for (int i = 0; i < 4; ++i)
		{
			rendererForIndex(i).enabled = false;
		}
	}

	private SpriteRenderer rendererForIndex(int index)
	{
		switch (index)
		{
		case 0:
			return rendererForObject(onesPlaceRenderer, onesPlace);
			break;
		case 1:
			return rendererForObject(tensPlaceRenderer, tensPlace);
			break;
		case 2:
			return rendererForObject(hundredsPlaceRenderer, hundredsPlace);
		case 3:
		default:
			return rendererForObject(thousandsPlaceRenderer, thousandsPlace);
			break;
		}
	}

	private SpriteRenderer rendererForObject(SpriteRenderer spriteRenderer, GameObject gameObj)
	{
		if (spriteRenderer == null)
		{
			spriteRenderer = gameObj.GetComponent<SpriteRenderer>();
		}
		return spriteRenderer;
	}

	private int numberOfDigitsInNumber(int number)
	{
		if (number == 0)
		{
			return 1;
		}
		int digits = 0;
		if (number <= 0)
		{
			digits = 1;
		}
		while (number > 0)
		{
			number /= 10;
			digits++;
		}

		return digits;
	}

	private Sprite spriteForNumber(int number)
	{
		Sprite retVal;
		switch (number)
		{
		case 0:
			retVal = zero;
			break;
		case 1: 
			retVal = one;
			break;
		case 2:
			retVal = two;
			break;
		case 3: 
			retVal = three;
			break;
		case 4:
			retVal = four;
			break;
		case 5:
			retVal = five;
			break;
		case 6:
			retVal = six;
			break;
		case 7:
			retVal = seven;
			break;
		case 8:
			retVal = eight;
			break;
		case 9:
		default:
			retVal = nine;
			break;
		}
		return retVal;
	}

	private void setSpriteRendererAlphaValue(SpriteRenderer spriteRenderer, float alphaValue)
	{
		Color newColor = new Color(1f, 1f, 1f, alphaValue);
		spriteRenderer.color = newColor;
	}

	public void setDigitAlpha(float alphaValue)
	{
		for (int i = 0; i < 4; ++i)
		{
			setSpriteRendererAlphaValue(rendererForIndex(i), alphaValue);
		}
	}

	public void updateColor(Color color)
	{
		for (int i = 0; i < 4; ++i)
		{
			rendererForIndex(i).color = color;
		}
	}
}
