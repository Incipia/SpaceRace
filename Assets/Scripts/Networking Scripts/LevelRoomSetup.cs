using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using AssemblyCSharp;

public class LevelRoomSetup : Photon.PunBehaviour 
{
	public CountdownManager countdownManager;
	public LevelComponentsManager levelSetup;
	public NetworkPlayerManager playerManager;

	private PhotonPlayer _localPlayer { get { return PhotonNetwork.player; }}
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

	void Start()
	{
		if (PhotonNetwork.connectedAndReady)
		{
			List<Vector3> startPositions = PlayerStartPositionProvider.startPositionsForRoomSize(_currentRoom.maxPlayers);
			
			// Get the start position that corresponds to THIS player
			Vector3 startPos = startPositions[PhotonNetwork.player.playerNumber()-1];
			playerManager.createPlayerAtPosition(startPos);

			setupCountdownManager();

			// This will trigger OnPhotonPlayerPropertiesChanged()
			_localPlayer.setReadyToRace(true);
		}
	}

	void OnPhotonPlayerPropertiesChanged(object[] playerAndUpdatedProps)
	{
		if (PhotonNetwork.isMasterClient && _allPlayersAreReadyToRace)
		{
			Debug.Log("players are ready! starting countdown...");
			photonView.RPC("beginCountdown", PhotonTargets.AllViaServer);
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
