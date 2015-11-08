using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using AssemblyCSharp;

public class LevelSetup : Photon.PunBehaviour 
{
	public CountdownManager countdownManager;
	public LevelComponentsManager levelSetup;
	public NetworkPlayerManager playerManager;
	public bool createPlayerOnStart = false;

	private Room _currentRoom { get { return PhotonNetwork.room; }}
	private bool _allPlayersAreReadyToRace { 
		get {
			bool allPlayersAreReady = true;
			foreach(PhotonPlayer player in PhotonNetwork.playerList)
			{
				if (player.readyToRace() == false)
				{
					allPlayersAreReady = false;
					break;
				}
			}
			return allPlayersAreReady;
		}
	}
	private bool _countdownStarted = false;

	void Start()
	{
		if (PhotonNetwork.connectedAndReady)
		{
			setupCountdownManager();
			if (createPlayerOnStart)
			{
				// Get the start position that corresponds to THIS player
				List<Vector3> startPositions = PlayerStartPositionProvider.startPositionsForRoomSize(_currentRoom.maxPlayers);
				Vector3 startPos = startPositions[PhotonNetwork.player.playerNumber()-1];
				playerManager.createPlayerAtPosition(startPos);
			}
			else
			{
				playerManager.movePlayerToStart();
			}

			// This will trigger OnPhotonPlayerPropertiesChanged()
			playerManager.disablePlayerMovement();
			playerManager.attachPlayerToCamera();
			playerManager.setPlayerReadyToRace(true);
		}
	}

	void OnPhotonPlayerPropertiesChanged(object[] playerAndUpdatedProps)
	{
		Debug.Log("properties changed!");
		if (PhotonNetwork.isMasterClient && _allPlayersAreReadyToRace && !_countdownStarted)
		{
			Debug.Log("players are ready! starting countdown...");

			_countdownStarted = true;
			photonView.RPC("beginCountdown", PhotonTargets.AllViaServer);

			// reset player ready status for next race
			playerManager.setPlayerReadyToRace(false);
		}
	}

	[PunRPC] void beginCountdown()
	{
		if (countdownManager != null)
		{
			countdownManager.showCountdownUI();
			countdownManager.beginCountdown();
		}
	}

	private void setupCountdownManager()
	{
		if (countdownManager != null)
		{
			countdownManager.hideCountdownUI();
			countdownManager.completion += countdownManager.hideCountdownUI;
			countdownManager.completion += levelSetup.activateMovingLevelComponents;
			countdownManager.completion += playerManager.enablePlayerMovement;
		}
	}
}
