using UnityEngine;
using System;
using AssemblyCSharp;
using System.Collections;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class LevelSetup : Photon.PunBehaviour 
{
	public CountdownManager countdownManager;
	public LevelComponentsManager levelSetup;
	public NetworkPlayerManager playerManager;
	public bool createPlayerOnStart = false;
	
	private PhotonPlayer _localPlayer { get { return PhotonNetwork.player; }}
	private Room _currentRoom { get { return PhotonNetwork.room; }}
	private bool _countdownStarted = false;
	
	void Start()
	{
		if (PhotonNetwork.connectedAndReady)
		{
			setupCountdownManager();
			if (createPlayerOnStart)
			{
				Vector3 startPosition = PlayerStartPositionProvider.startPositionForPlayer(PhotonNetwork.player);
				playerManager.createPlayerAtPosition(startPosition);
			}
			else
			{
				_localPlayer.setNeedsToResetPosition(true);
			}

			_localPlayer.setMovementEnabled(false);
			_localPlayer.setCrossedFinishLine(false);
			_localPlayer.setNeedsToAttachCamera(true);

			// Give it about a second until we say that the player is ready to race -- this should help
			// assure that each client can see everyone else by the time this is called
			StartCoroutine(setPlayerReadyToRaceAfterDuration(1.5f));
		}
	}

	void OnPhotonPlayerPropertiesChanged(object[] playerAndUpdatedProps)
	{
		// don't bother to check whether players are ready to race if that property is
		// not what changed
		Hashtable props = playerAndUpdatedProps[1] as Hashtable;
		if (props.ContainsKey(PlayerConstants.readyToRaceKey))
		{
			if (PhotonNetwork.isMasterClient && PhotonNetwork.room.allPlayersAreReadyToRace())
			{
				// reset player ready status for next race
				_localPlayer.setReadyToRace(false);
				
				Debug.Log("players are ready! starting countdown...");
				photonView.RPC("beginCountdown", PhotonTargets.AllViaServer);
			}
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

	private IEnumerator setPlayerReadyToRaceAfterDuration(float duration)
	{
		yield return new WaitForSeconds(duration);
		_localPlayer.setReadyToRace(true);
	}

	private void setupCountdownManager()
	{
		if (countdownManager != null)
		{
			countdownManager.hideCountdownUI();
			countdownManager.completion += countdownManager.hideCountdownUI;
			countdownManager.completion += levelSetup.activateMovingLevelComponents;
			countdownManager.completion += _localPlayer.enableMovement;
		}
	}
}
