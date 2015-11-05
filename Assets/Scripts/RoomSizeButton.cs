using UnityEngine;
using System.Collections;

public class RoomSizeButton : MonoBehaviour
{
	public RoomSizeSelector roomSizeSelector;
	public int number;

	void OnMouseDown()
	{
		roomSizeSelector.selectedRoomSize(number);
	}
}
