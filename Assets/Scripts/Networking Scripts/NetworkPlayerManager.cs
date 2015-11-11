using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using AssemblyCSharp;

public class NetworkPlayerManager : Photon.PunBehaviour
{
	public GameObject playerPrefab;
	private PhotonPlayer _localPlayer { get { return PhotonNetwork.player; }}

	public void createPlayerAtPosition(Vector3 startPos)
	{
		PhotonNetwork.Instantiate(playerPrefab.name, startPos, Quaternion.identity, 0);
	}

	public void setPlayerCrossedFinishLine(bool crossed)
	{
		_localPlayer.setCrossedFinishLine(crossed);
	}

	public void setPlayerReadyToRace(bool ready)
	{
		_localPlayer.setReadyToRace(ready);
	}

	public void enablePlayerMovement()
	{
		setEnablePlayerMovement(true);
	}

	public void disablePlayerMovement()
	{
		setEnablePlayerMovement(false);
	}
	
	private void setEnablePlayerMovement(bool enable)
	{
		_localPlayer.setMovementEnabled(enable);
	}

	public void attachPlayerToCamera()
	{
		_localPlayer.setNeedsToAttachCamera(true);
	}

	public void movePlayerToStart()
	{
		_localPlayer.setNeedsToResetPosition(true);
	}
}
