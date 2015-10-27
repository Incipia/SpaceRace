using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using AssemblyCSharp;

public class DefaultRoomMatchmaker : Photon.PunBehaviour 
{
	const string GAME_VERSION = "0.0.1";

	public GameObject playerPrefab;
	public CountdownManager countdownManager;

	private List<MovePlayerPhoton> movePlayerScripts = new List<MovePlayerPhoton>();

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
			if (GUI.Button(new Rect(0, 0, 175, 100), "Connect to 4 Person Room"))
			{
				RoomOptions roomOptions = new RoomOptions() {
					maxPlayers = 4,
					isVisible = false
				};
				PhotonNetwork.JoinOrCreateRoom("default4", roomOptions, TypedLobby.Default);
			}
			if (GUI.Button(new Rect(0, 100, 175, 100), "Connect to 3 Person Room"))
			{
				RoomOptions roomOptions = new RoomOptions() {
					maxPlayers = 3,
					isVisible = false
				};
				PhotonNetwork.JoinOrCreateRoom("default3", roomOptions, TypedLobby.Default);
			}
			if (GUI.Button(new Rect(0, 200, 175, 100), "Connect to 2 Person Room"))
			{
				RoomOptions roomOptions = new RoomOptions() {
					maxPlayers = 2,
					isVisible = false
				};
				PhotonNetwork.JoinOrCreateRoom("default2", roomOptions, TypedLobby.Default);
			}
			if (GUI.Button(new Rect(0, 300, 175, 100), "Connect to 1 Person Room"))
			{
				RoomOptions roomOptions = new RoomOptions() {
					maxPlayers = 1,
					isVisible = false
				};
				PhotonNetwork.JoinOrCreateRoom("default1", roomOptions, TypedLobby.Default);
			}
		}
	}

	void OnJoinedRoom()
	{
		Room currentRoom = PhotonNetwork.room;
		List<Vector3> startPositions = PlayerStartPositionProvider.startPositionsForMaxPlayers(currentRoom.maxPlayers);

		Vector3 position = startPositions[currentRoom.playerCount-1];
		GameObject player = PhotonNetwork.Instantiate(playerPrefab.name, position, Quaternion.identity, 0);

		MovePlayerPhoton movePlayer = player.GetComponent<MovePlayerPhoton>();
		if (movePlayer != null)
		{
			movePlayer.enabled = false;
			movePlayerScripts.Add(movePlayer);
		}

		if (currentRoom.maxPlayers == currentRoom.playerCount)
		{
			beginCountdown();
			photonView.RPC("beginCountdown", PhotonTargets.Others);
		}
	}

	[PunRPC]
	void beginCountdown()
	{
		countdownManager.beginCountdownWithSeconds(5, enablePlayers);
	}

	void enablePlayers()
	{
		foreach(MovePlayerPhoton movePlayer in movePlayerScripts)
		{
			movePlayer.enabled = true;
		}
	}
}
