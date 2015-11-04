using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using AssemblyCSharp;

public class NetworkPlayerManager : Photon.PunBehaviour
{
	public GameObject playerPrefab;
	public bool autoDisablePlayerMovementOnCreate;
	private MovePlayerPhoton movePlayer;
	private NetworkPlayerVisibilitySetup visibilitySetup;

	public void createPlayerAtPosition(Vector3 startPos)
	{
		GameObject player = PhotonNetwork.Instantiate(playerPrefab.name, startPos, Quaternion.identity, 0);

		movePlayer = player.GetComponent<MovePlayerPhoton>();
		if (movePlayer != null)
		{
			movePlayer.enabled = !autoDisablePlayerMovementOnCreate;
		}

		visibilitySetup = player.GetComponent<NetworkPlayerVisibilitySetup>();
	}

	public void enablePlayerMovement()
	{
		if (movePlayer != null)
		{
			movePlayer.enabled = true;
		}
	}

	void OnPhotonPlayerPropertiesChanged (object[] playerAndUpdatedProps)
	{
		foreach (object obj in playerAndUpdatedProps)
		{
			Debug.Log("Player properties changed: " + obj);
		}
	}

	public void makePlayerVisible()
	{
		visibilitySetup.makeEverythingVisible(true);
	}
}
