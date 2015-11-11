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
	private bool _countdownStarted = false;

	void Start()
	{
		setupCountdownManager();
		if (PhotonNetwork.connectedAndReady)
		{
			if (createPlayerOnStart)
			{
				Vector3 startPosition = PlayerStartPositionProvider.startPositionForPlayer(PhotonNetwork.player);
				playerManager.createPlayerAtPosition(startPosition);
			}
			else
			{
				playerManager.movePlayerToStart();
			}

			playerManager.disablePlayerMovement();
			playerManager.attachPlayerToCamera();
			playerManager.setPlayerCrossedFinishLine(false);
			playerManager.setPlayerReadyToRace(true);
		}
	}

	void OnPhotonPlayerPropertiesChanged(object[] playerAndUpdatedProps)
	{
		if (PhotonNetwork.isMasterClient && PhotonNetwork.room.allPlayersAreReadyToRace())
		{
			// reset player ready status for next race
			playerManager.setPlayerReadyToRace(false);

			Debug.Log("players are ready! starting countdown...");
			photonView.RPC("beginCountdown", PhotonTargets.AllViaServer);
		}
	}

	[PunRPC] void beginCountdown()
	{
		if (countdownManager != null)
		{
			countdownManager.beginCountdown();
			countdownManager.showCountdownUI();
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
