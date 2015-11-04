using UnityEngine;
using System.Collections;

public class TempJoinRoomGUI : MonoBehaviour 
{	
	const string GAME_VERSION = "0.0.1";

	void Start()
	{
		PhotonNetwork.ConnectUsingSettings(GAME_VERSION);
	}

	void OnGUI()
	{
		if (!PhotonNetwork.connected)
		{
			GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
		}
		else if (PhotonNetwork.room == null)
		{
			// Create Room
			if (GUI.Button(new Rect(0, 0, 175, 150), "Connect to 1 Person Room"))
			{
				RoomOptions roomOptions = new RoomOptions() {
					maxPlayers = 1,
					isVisible = false
				};
				PhotonNetwork.JoinOrCreateRoom("default4", roomOptions, TypedLobby.Default);
			}
			if (GUI.Button(new Rect(0, 175, 175, 150), "Connect to 3 Person Room"))
			{
				RoomOptions roomOptions = new RoomOptions() {
					maxPlayers = 3,
					isVisible = false
				};
				PhotonNetwork.JoinOrCreateRoom("default3", roomOptions, TypedLobby.Default);
			}
			if (GUI.Button(new Rect(200, 0, 175, 150), "Connect to 2 Person Room"))
			{			
				RoomOptions roomOptions = new RoomOptions() {
					maxPlayers = 2,
					isVisible = false
				};
				PhotonNetwork.JoinOrCreateRoom("default2", roomOptions, TypedLobby.Default);
			}
			if (GUI.Button(new Rect(200, 175, 175, 150), "Connect to 4 Person Room"))
			{
				RoomOptions roomOptions = new RoomOptions() {
					maxPlayers = 4,
					isVisible = false
				};
				PhotonNetwork.JoinOrCreateRoom("default1", roomOptions, TypedLobby.Default);
			}
		}
	}
}
