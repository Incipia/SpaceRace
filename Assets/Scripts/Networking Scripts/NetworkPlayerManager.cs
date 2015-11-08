using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using AssemblyCSharp;

public class NetworkPlayerManager : Photon.PunBehaviour
{
	public GameObject playerPrefab;
	private NetworkPlayerVisibilitySetup visibilitySetup;
	private PhotonPlayer _localPlayer { get { return PhotonNetwork.player; }}

	public void createPlayerAtPosition(Vector3 startPos)
	{
		GameObject player = PhotonNetwork.Instantiate(playerPrefab.name, startPos, Quaternion.identity, 0);
		visibilitySetup = player.GetComponent<NetworkPlayerVisibilitySetup>();
	}
	
	public void setPlayerReadyToRace(bool ready)
	{
		_localPlayer.setReadyToRace(ready);
	}
	
	public void makePlayerVisible()
	{
		visibilitySetup.makeEverythingVisible(true);
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
