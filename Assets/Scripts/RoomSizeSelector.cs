using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoomSizeSelector : MonoBehaviour
{
	public SpriteRenderer one;
	public SpriteRenderer two;
	public SpriteRenderer three;
	public SpriteRenderer four;
	public SpriteRenderer selectedRing;
	public Color selectedColor = Color.magenta;
	public Color unselectedColor = Color.black;

	public int currentRoomSize { get { return _currentRoomSize; }}
	private int _currentRoomSize = 1;

	void Start()
	{
		selectedRing.color = selectedColor;
		resetAllRenderers();
		selectedRoomSize(_currentRoomSize);
	}

	public void selectedRoomSize(int size)
	{
		_currentRoomSize = size;
		resetAllRenderers();

		SpriteRenderer renderer = rendererForRoomSize(size);
		if (renderer != null)
		{
			renderer.color = selectedColor;
			selectedRing.transform.position = renderer.transform.position;
		}
	}

	private SpriteRenderer rendererForRoomSize(int size)
	{
		SpriteRenderer renderer = null;
		if (size == 1)
		{
			renderer = one;
		}
		else if (size == 2)
		{
			renderer = two;
		}
		else if (size == 3)
		{
			renderer = three;
		}
		else if (size == 4)
		{
			renderer = four;
		}
		return renderer;
	}

	private void resetAllRenderers()
	{
		List<SpriteRenderer> rendererArray = new List<SpriteRenderer>(new SpriteRenderer[] { one, two, three, four });
		foreach (SpriteRenderer renderer in rendererArray)
		{
			renderer.color = unselectedColor;
		}
	}
}
