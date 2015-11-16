using UnityEngine;

public class StartButton : MonoBehaviour 
{
	public NetworkRoomManager roomManager;
	public RoomSizeSelector cupSelector;

	void OnMouseUpAsButton()
	{
		int cupNumber = cupSelector.currentRoomSize;
		roomManager.startGameForCupNumber(cupNumber);
	}	
}
