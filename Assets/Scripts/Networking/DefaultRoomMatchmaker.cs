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

	void Awake() 
	{
		PhotonNetwork.ConnectUsingSettings(GAME_VERSION);

		countdownManager.hideCountdownUI();
		levelSetup.deactivateMovingLevelComponents();
		playersManager.autoDisablePlayerMovementOnCreate = true;
	}

	void OnJoinedRoom()
	{
		List<Vector3> startPositions = PlayerStartPositionProvider.startPositionsForRoomSize(currentRoom.maxPlayers);
		Vector3 startPos = startPositions[currentRoom.playerCount-1];
		playersManager.createAndTrackPlayer(startPos);

		if (roomIsFull)
		{
			photonView.RPC("beginCountdown", PhotonTargets.AllViaServer);
		}
	}

	[PunRPC] void beginCountdown()
	{
		countdownManager.beginCountdownWithSeconds(5, countdownFinished);
		countdownManager.showCountdownUI();
	}

	private void countdownFinished()
	{
		countdownManager.hideCountdownUI();
		levelSetup.activateMovingLevelComponents();
		playersManager.enableTrackedPlayerMovement();
	}
}
