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
	private Room currentRoom {
		get {
			return PhotonNetwork.room;
		}
	}
	private bool roomIsFull {
		get {
			return currentRoom.maxPlayers == currentRoom.playerCount;
		}
	}

	void Start()
	{
		levelSetup.deactivateMovingLevelComponents();

		PhotonNetwork.ConnectUsingSettings(GAME_VERSION);
		playersManager.autoDisablePlayerMovementOnCreate = true;
		setupCountdownManager();
	}

	void OnJoinedRoom()
	{
		List<Vector3> startPositions = PlayerStartPositionProvider.startPositionsForRoomSize(currentRoom.maxPlayers);

		// Get the start position that corresponds to THIS player
		Vector3 startPos = startPositions[currentRoom.playerCount-1];
		playersManager.createAndTrackPlayer(startPos);

		if (roomIsFull)
		{
			photonView.RPC("beginCountdown", PhotonTargets.AllViaServer);
		}
	}

	[PunRPC] void beginCountdown()
	{
		countdownManager.showCountdownUI();
		countdownManager.beginCountdown();
	}

	private void setupCountdownManager()
	{
		countdownManager.hideCountdownUI();
		countdownManager.completion += countdownManager.hideCountdownUI;
		countdownManager.completion += levelSetup.activateMovingLevelComponents;
		countdownManager.completion += playersManager.enableTrackedPlayerMovement;
	}
}
