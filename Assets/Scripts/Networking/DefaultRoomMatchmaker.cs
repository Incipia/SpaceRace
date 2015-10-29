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
	public NetworkRoomLevelSetup levelSetup;
	public NetworkPlayersManager playersManager;

	private List<MovePlayerPhoton> movePlayerScripts = new List<MovePlayerPhoton>();

	void Start() 
	{
		PhotonNetwork.ConnectUsingSettings(GAME_VERSION);

		countdownManager.hideCountdownUI();
		levelSetup.deactivateMovingLevelComponents();
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

	private bool roomIsFull()
	{
		Room currentRoom = PhotonNetwork.room;
		return currentRoom.maxPlayers == currentRoom.playerCount;
	}

	void OnJoinedRoom()
	{
		playersManager.createNewPlayer();
		if (roomIsFull())
		{
			beginCountdown();
			photonView.RPC("beginCountdown", PhotonTargets.Others);
		}
	}

	[PunRPC] void beginCountdown()
	{
		countdownManager.beginCountdownWithSeconds(5, countdownFinished);
		countdownManager.showCountdownUI();
	}

	void countdownFinished()
	{
		countdownManager.hideCountdownUI();
		levelSetup.activateMovingLevelComponents();
		playersManager.enableTrackedPlayers();
	}
}
