using UnityEngine;

public class MainMenuButton : MonoBehaviour 
{
	public string mainMenuLevelName = "StartScreen";
	bool _isLeavingRoom;

	void OnMouseUpAsButton()
	{
		if (!_isLeavingRoom)
		{
			_isLeavingRoom = true;
			PhotonNetwork.LeaveRoom();
			PhotonNetwork.LoadLevel(mainMenuLevelName);
		}
	}
}
