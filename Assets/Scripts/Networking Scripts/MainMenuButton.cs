using UnityEngine;
using System.Collections;

public class MainMenuButton : MonoBehaviour 
{
	public string mainMenuLevelName = "StartScreen";
	bool _isLeavingRoom;

	void OnMouseDown()
	{
		if (!_isLeavingRoom)
		{
			_isLeavingRoom = true;
			PhotonNetwork.LeaveRoom();
			PhotonNetwork.LoadLevel(mainMenuLevelName);
		}
	}
}
